using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecycleManager : MonoBehaviour
{

	[SerializeField]
	private Image _fillImage;
	[SerializeField]
	private Button _startButton;
	[SerializeField]
	private GameObject _startPanel;

	[SerializeField]
	private RecyclePickupReceiver _receiver;
	[SerializeField]
	private TrashSpawner _spawner;
	RecycleTimer _timer;


	public int itemCount = 0;
	public int removeCount = 0;
	public int successCount = 0;
	public int failCount = 0;

	private void Start()
	{
		_timer = new RecycleTimer(10);
		_timer.OnTimerStart += TimerStart;
		_timer.OnTimerUpdate += TimerUpdated;
		_timer.OnTimerEnd += TimerEnd;

		_startButton.onClick.AddListener(OnTouchStartButton);
		_receiver.OnSuccess += () => {
			removeCount++;
			successCount++;
			ClearRecycle();
		};
		_receiver.OnFail += () => {
			removeCount++;
			failCount++;
			ClearRecycle();
		};
	}

	private void Update()
	{
		_timer.Update();
	}

	public void ClearRecycle()
	{
		if (removeCount == itemCount) {
			_timer.Stop();
			Debug.Log("클리어 성공");

			//TODO: 패널티 받을것

			//SceneChanger.Instance.ChangeScene(SceneName.End_Success);
		}
	}

	public void FailRecycle()
	{
		int panelty = itemCount - successCount;
		Debug.Log($"클리어 실패: {panelty}");
		//SceneChanger.Instance.ChangeScene(SceneName.End_Fail);
	}


	private void TimerUpdated(float max, float time)
	{
		_fillImage.fillAmount = time / max;
	}

	private void TimerStart(float max, float time)
	{
		_fillImage.fillAmount = 1f;
	}

	private void TimerEnd(float max, float time)
	{
		_fillImage.fillAmount = 0f;
		FailRecycle();
	}

	public void OnTouchStartButton()
	{
		_startPanel.SetActive(false);
		_timer.TimerStart();

		var itemList = DataManager.Instance.AllItem;
		List<TrashSpawner.SpawnArguments> list = itemList.Select(s => new TrashSpawner.SpawnArguments(s.Data)).ToList();

		itemCount = list.Count;
		_spawner.SpawnItems(list);
	}
}
