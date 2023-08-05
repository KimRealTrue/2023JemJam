using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{

	public int stage;

	public static DataManager Instance {
		get {
			if (_instance == null) {
				Create();
			}
			return _instance;
		}
	}

	private static DataManager _instance;
	public static void Create()
	{
		GameObject go = new GameObject("DataManager");
		_instance = go.AddComponent<DataManager>();
	}


	private List<Object.ItemObject> _itemDataList = new List<Object.ItemObject>();
	public List<Object.ItemObject> AllItem {
		get {
			return _itemDataList;
		}
	}



	private void Start()
	{
		ClearItem();
	}



	public void GetItem(Object.ItemObject item)
	{
		Debug.Log($"아이템 획득: {item}");

		switch (item.Data.ItemType) {
			case Global.ItemType.Positive: {
				Debug.Log("긍정 효과 발휘");
				break;
			}
			case Global.ItemType.Negative: {
				Debug.Log("부정 효과 발휘");
				break;
			}
			case Global.ItemType.Trash: {
				_itemDataList.Add(item);
				break;
			}
			default: {
				break;
			}
		}
	}

	public void ClearItem()
	{
		_itemDataList.Clear();
	}

}