using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Object
{
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

		private void Start()
		{
			_spriteRenderer = GetComponent<SpriteRenderer>();
			_spriteRenderer.sprite = _itemData.ItemSprite;
		}


		public void PickByTouch()
		{
			PickAim();
		}

		public void RemoveInRecycle()
		{
			RecycleAnim();
		}


		private void SpawnAnim()
		{
		
		}

		private void PickAim()
		{
			StartCoroutine(RecycleAimation());
		}

		private void RecycleAnim()
		{
			StartCoroutine(RecycleAimation());
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
}