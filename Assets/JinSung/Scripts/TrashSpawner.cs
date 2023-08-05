using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashSpawner : MonoBehaviour
{
	[SerializeField]
	private List<ItemObject> _prefabList = new List<ItemObject>();
	[SerializeField]
	private SpriteRenderer _spawnArea;
	private Vector2 _spawnRange;

	[SerializeField]
	private Transform _trashParent;


	public delegate void OnTrashDelegate();


	private void Start()
	{
		_spawnRange = (_spawnArea.transform.localScale * 0.5f);
	}

	public void SpawnItems(List<SpawnArguments> trashList)
	{
		int idx = 0;
		foreach (var t in trashList) {
			//ItemObject itemPrefab = _prefabList.Find(p => p.Data.Name == t.name && p.Data.TrashType == t.trashType);
			ItemObject itemPrefab = _prefabList.Find(p => p.Data.TrashType == t.trashType);
			ItemObject item = GameObject.Instantiate(itemPrefab);
			item.transform.SetParent(_trashParent);
			item.transform.position = new Vector3(Random.Range(-_spawnRange.x, _spawnRange.x),
				Random.Range(-_spawnRange.y, _spawnRange.y),
				idx * 0.01f
				) + _spawnArea.transform.position;

			Vector3 r = new Vector3(0, 0, Random.Range(0, 360));
			item.transform.rotation = Quaternion.Euler(r);
			idx++;
		}
	}

	public struct SpawnArguments
	{
		public Global.TrashType trashType;
		public string name;

		public SpawnArguments(int idx)
		{
			trashType = (Global.TrashType)idx;
			name = "";
		}

		public SpawnArguments(Data.ItemData itemData)
		{
			trashType = itemData.TrashType;
			name = itemData.Name;
		}
	}

}