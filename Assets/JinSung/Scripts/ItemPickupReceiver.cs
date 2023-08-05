using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ItemPickupReceiver : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {


	}

    // Update is called once per frame
    private void Update()
    {
		if (EventSystem.current.IsPointerOverGameObject() == false) {
			GameObject contactedObject = RaycastObject();
			if (contactedObject != null) {
				Object.ItemObject item = contactedObject.transform.parent.GetComponent<Object.ItemObject>();
				if (item != null) {
					item.PickByTouch();
					DataManager.Instance.GetItem(item);
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

		Debug.Log(hits.Length);

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
