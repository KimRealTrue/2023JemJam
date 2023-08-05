using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
	public GlobalData globalEasyData;
	public GlobalData globalNormalData;
	public GlobalData globalHardData;


	public int stage;

	public static DataManager Instance {
		get {
			return _instance;
		}
	}

	private static DataManager _instance;

	private List<ItemObject> _itemDataList = new List<ItemObject>();
	public List<ItemObject> AllItem {
		get {
			return _itemDataList;
		}
	}

	public int ecoDamage = 0;


	private void Awake()
	{
		if (_instance == null) {
			_instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else {
			Destroy(gameObject);
		}
	}

	private void Start()
	{
		ClearItem();
	}

	public bool IsEcoFail()
	{
		return ecoDamage > EcoHp;
	}

	public void GetRecycleEcoDamage(int count)
	{
		if (stage == 1) {
			ecoDamage += globalEasyData.recycleEcoDamage * count;
		}
		else if (stage == 2) {
			ecoDamage += globalNormalData.recycleEcoDamage * count;
		}
		else if (stage == 3) {
			ecoDamage += globalHardData.recycleEcoDamage * count;
		}
	}


	public void GetEcoDamage()
	{
		if (stage == 1) {
		//Debug.Log($"환경 데미지: {globalEasyData.ecoDamage}");
			ecoDamage += globalEasyData.ecoDamage;
		}
		else if (stage == 2) {
		//Debug.Log($"환경 데미지: {globalNormalData.ecoDamage}");
			ecoDamage += globalNormalData.ecoDamage;
		}
		else if (stage == 3) {
		//Debug.Log($"환경 데미지: {globalHardData.ecoDamage}");
			ecoDamage += globalHardData.ecoDamage;
		}

		if (ecoDamage > EcoHp) {
			if (Timer_Controller.Instance.IsGameOverPanel == false) {
				//Debug.Log("EEE");
				Timer_Controller.Instance.OpenGameOverPanel(() => {
					SceneChanger.Instance.ChangeScene(SceneName.End_Fail);
				});
			}
		}
	}



	public void GetItem(ItemObject item)
	{
		//Debug.Log($"아이템 획득: {item}");
		switch (item.Data.ItemType) {
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


	public int EcoHp {
		get {
			if (stage == 1) {
				return globalEasyData.ecoHP;
			}
			else if (stage == 2) {
				return globalNormalData.ecoHP;
			}
			else {
				return globalHardData.ecoHP;
			}
		}
	}

	public float RecycleTime {
		get {
			if (stage == 1) {
				return globalEasyData.recycleTime;
			}
			else if (stage == 2) {
				return globalNormalData.recycleTime;
			}
			else {
				return globalHardData.recycleTime;
			}
		}
	}

	public float GameTime {
		get {
			if (stage == 1) {
				return globalEasyData.gameTime;
			}
			else if (stage == 2) {
				return globalNormalData.gameTime;
			}
			else {
				return globalHardData.gameTime;
			}
		}
	}

	public Vector2 TrashDropTime {
		get {
			if (stage == 1) {
				return globalEasyData.dropTime;
			}
			else if (stage == 2) {
				return globalNormalData.dropTime;
			}
			else {
				return globalHardData.dropTime;
			}
		}
	}
}