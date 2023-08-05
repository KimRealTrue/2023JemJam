using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;




namespace Data
{
	[Serializable]
	public class ItemData
	{
		public string Name {
			get {
				return _name;
			}
		}
		[Header("Data"), SerializeField]
		private string _name;


		public Global.ItemType ItemType {
			get {
				return _itemType;
			}
		}
		[SerializeField]
		private Global.ItemType _itemType;

		public Global.TrashType TrashType {
			get {
				return _trashType;
			}
		}
		[SerializeField]
		private Global.TrashType _trashType;

		public Sprite ItemSprite {
			get {
				return _sprite;
			}
		}
		[SerializeField]
		private Sprite _sprite;

		//public float EcoDamage {
		//	get {
		//		return _ecoDamage;
		//	}
		//}
		//[SerializeField]
		//private float _ecoDamage;
		//
		//
		//public float Score {
		//	get {
		//		return _score;
		//	}
		//}
		//private float _score;


		public float RemoveTime {
			get {
				return _removeTime;
			}		
		}
		[SerializeField]
		private float _removeTime;



		public override string ToString()
		{
			return $"이름: {_name}, 아이템타입: {_itemType}, 쓰레기타입: {_trashType}, 제거 대기 시간: {_removeTime}";
		}
	}
}