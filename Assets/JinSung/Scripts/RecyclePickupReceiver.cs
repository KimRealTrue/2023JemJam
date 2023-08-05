using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecyclePickupReceiver : MonoBehaviour
{
	public float size = 300;
	[SerializeField]
	private Camera _uiCam;
	private GameObject _draggedItem;
	private Vector3 _pos;
	private Vector3 _uiPos;

	public GameObject successParticle;
	public GameObject failParticle;

	[SerializeField]
	private List<RectTransform> _trashCanList = new List<RectTransform>();


	public delegate void OnRecycleDelegate();
	public event OnRecycleDelegate OnSuccess;
	public event OnRecycleDelegate OnFail;


	private void Update()
	{
		if (Input.GetMouseButtonDown(0)) {
			SearchItem();
		}

		if (Input.GetMouseButton(0) && _draggedItem != null) {
			DragItem();
		}

		if (Input.GetMouseButtonUp(0)) {
			RectTransform trashCan = SearchUI();
			DropItem(trashCan);

			_draggedItem = null;
		}
	}

	private void SearchItem()
	{
		GameObject item = RaycastObject();
		if (item != null) {
			//Debug.Log(item);
			_draggedItem = item.gameObject;
		}
	}

	private void DragItem()
	{
		_pos = _uiCam.ScreenToViewportPoint(Input.mousePosition);
		_pos = new Vector2(Mathf.Lerp(-19.20f / 2f, 19.20f / 2f, _pos.x), Mathf.Lerp(-10.80f / 2f, 10.80f / 2f, _pos.y));
		_draggedItem.transform.position = _pos;
	}

	private Vector2 MouseToAnchoredPosition()
	{
		Vector2 mousePosition = Input.mousePosition;
		Vector2 pos = _uiCam.ScreenToViewportPoint(Input.mousePosition);
		pos = new Vector2(Mathf.Lerp(0, 1920f, pos.x), Mathf.Lerp(0, -1080f, 1 - pos.y));

		return pos;
	}

	private RectTransform SearchUI()
	{
		_uiPos = MouseToAnchoredPosition();

		float minDist = float.MaxValue;
		RectTransform nearRt = null;
		foreach (RectTransform rt in _trashCanList) {
			float dist = Vector2.Distance(_uiPos, rt.anchoredPosition);
			//Debug.Log($"Object: {_uiPos}, UI: {rt.anchoredPosition} => {dist}");
			if (minDist > dist) {
				minDist = dist;
				nearRt = rt;
			}
		}

		//Debug.Log($"{nearRt} => {minDist}");
		if (minDist > size) {
			nearRt = null;
		}

		//Debug.Log(nearRt);

		return nearRt;
	}

	private void DropItem(RectTransform trashCan)
	{
		if (trashCan != null) {
			GarbageBox tc = trashCan.gameObject.GetComponent<GarbageBox>();
			if (_draggedItem != null) {
				ItemObject item = _draggedItem.GetComponent<ItemObject>();
				//Debug.Log(tc);
				//Debug.Log(item);
				if (tc.trashType == item.Data.TrashType) {
					item.RemoveInRecycle();
					Instantiate(successParticle, item.transform.position, Quaternion.identity, item.transform.parent);

					//Debug.Log($"성공" +
					//	$"\n쓰레기통: {tc.trashType}, 아이템: {item.Data.TrashType}");
					OnSuccess?.Invoke();
				}
				else {
					item.RemoveInRecycle();
					Instantiate(failParticle, item.transform.position, Quaternion.identity, item.transform.parent);
					//Debug.Log($"실패!!" +
					//	$"\n쓰레기통: {tc.trashType}, 아이템: {item.Data.TrashType} => {DataManager.Instance.stage}");
					OnFail?.Invoke();
				}
			}
		}
	}



	private GameObject RaycastObject()
	{
		if (Input.GetMouseButtonDown(0)) {
			GameObject castedObject = GetObject();
			return castedObject;
		}
		return null;
	}

	private GameObject GetObject()
	{
		Vector3 clickWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		clickWorldPosition.z = -10f;

		RaycastHit2D[] hits = Physics2D.RaycastAll(clickWorldPosition, Vector2.zero, 100, LayerMask.GetMask("Item"));
		GameObject forwardObject = null;

		//Debug.Log(hits.Length);

		if (hits != null && hits.Length > 0) {
			float zValue = float.MaxValue;
			foreach (RaycastHit2D hit in hits) {
				GameObject go = hit.collider.gameObject;
				if (zValue > go.transform.position.z) {
					zValue = go.transform.position.z;
					forwardObject = go;
				}
			}
		}

		return forwardObject;
	}
}
