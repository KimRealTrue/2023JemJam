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
	private GameObject _tutorialPanel;

	[SerializeField]
	private RecyclePickupReceiver _receiver;
	[SerializeField]
	private GameOverPanel _gameOverPanel;
	[SerializeField]
	private TrashSpawner _spawner;
	RecycleTimer _timer;


	public int itemCount = 0;
	public int removeCount = 0;
	public int successCount = 0;
	public int failCount = 0;

	private void Start()
	{
		_timer = new RecycleTimer(DataManager.Instance.RecycleTime);
		_timer.OnTimerStart += TimerStart;
		_timer.OnTimerUpdate += TimerUpdated;
		_timer.OnTimerEnd += TimerEnd;

		_startButton.onClick.AddListener(OnTouchStartButton);
		_receiver.OnSuccess += OnSuccess;
		_receiver.OnFail += OnFail;

		Audio_Controller.instance.BGMPlay_PlayingRecycle();
	}

	private void Update()
	{
		_timer.Update();
	}


	void OnSuccess()
	{
		Audio_Controller.instance.EffectPlay_RecycleSuccess();
		removeCount++;
		successCount++;
		CheckClearRecycle(End);
	}

	private void OnFail()
	{
		Audio_Controller.instance.EffectPlay_RecycleFail();
		removeCount++;
		failCount++;
		DataManager.Instance.GetRecycleEcoDamage(1);
		CheckClearRecycle(FailRecycle);
	}
	

	public void CheckClearRecycle(System.Action callback)
	{
		if (removeCount == itemCount) {
			_timer.Stop();
			//Debug.Log("클리어");

			_gameOverPanel.gameObject.SetActive(true);
			_gameOverPanel.StartFaceIn(callback);
		}
	}

	void End()
	{
		if (DataManager.Instance.IsEcoFail()) {
			FailRecycle();
		}
		else {
			SceneChanger.Instance.ChangeScene(SceneName.End_Success);
		}
	}

	public void FailRecycle()
	{
		SceneChanger.Instance.ChangeScene(SceneName.End_Fail);
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
		int panelty = itemCount - successCount;
		Debug.Log($"클리어 실패: {panelty}");

		DataManager.Instance.GetRecycleEcoDamage(panelty);
		_gameOverPanel.gameObject.SetActive(true);
		_gameOverPanel.StartFaceIn(FailRecycle);
	}

	public void OnTouchStartButton()
	{
		_startPanel.SetActive(false);
		_tutorialPanel.SetActive(false);
		_timer.TimerStart();

		var itemList = DataManager.Instance.AllItem;
		List<TrashSpawner.SpawnArguments> list = itemList.Select(s => new TrashSpawner.SpawnArguments(s.Data)).ToList();

		itemCount = list.Count;
		_spawner.SpawnItems(list);
	}
}
