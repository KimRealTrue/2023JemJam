using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//유지 시간 10초
public class ItemObject : MonoBehaviour
{
	public Data.ItemData Data {
		get {
			return _itemData;
		}
	}


	[SerializeField]
	private Data.ItemData _itemData;
	private SpriteRenderer _spriteRenderer;
	public float removeTime = 0.5f;

	float _lifeTime = 0;
	bool _isAlive = false;

	public bool autoRemove = false;


	public bool IsAlive => _isAlive;

	private void Start()
	{
		_spriteRenderer = GetComponent<SpriteRenderer>();
		_spriteRenderer.sprite = _itemData.ItemSprite;
		_lifeTime = _itemData.RemoveTime;
		_isAlive = true;
	}


	private void Update()
	{
		if (_isAlive == true && autoRemove && Timer_Controller.Instance.IsGameOverPanel == false) {
			_lifeTime -= Time.deltaTime;

			if (_lifeTime <= 0) {
				_isAlive = false;

				Audio_Controller.instance.EffectPlay_TrashFail();
				DataManager.Instance.GetEcoDamage();
				Destroy(gameObject);
			}
		}
	}


	public void PickByTouch()
	{
		_isAlive = false;
		Audio_Controller.instance.EffectPlay_TrashPickup();
		PickAim();
	}

	public void RemoveInRecycle()
	{
		RecycleAnim();
	}

	private void PickAim()
	{
		StartCoroutine(RecycleAimation());
	}

	private void RecycleAnim()
	{
		try {
			StartCoroutine(RecycleAimation());
		}
		catch (System.Exception ex) {
			
		}
	}

	IEnumerator RecycleAimation()
	{
		float time = removeTime;
		float dt = 1 / time;
		Vector3 originScale = transform.localScale;

		while (time > 0) {
			time -= Time.deltaTime * dt;
			transform.localScale = Vector3.Lerp(Vector3.zero, originScale, time);
			transform.Rotate(Vector3.forward, 180 * Time.deltaTime * dt);

			if (time <= 0) {
				time = 0;
				break;
			}
			yield return null;
		}

		transform.localScale = Vector2.zero;

		Destroy(gameObject);
	}


	public override string ToString()
	{
		return _itemData.ToString();
	}
}