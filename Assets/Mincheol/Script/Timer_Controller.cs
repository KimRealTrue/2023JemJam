using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer_Controller : MonoBehaviour
{
    float startingTime;
    float remainTime;
    [SerializeField] Image clock;
    [SerializeField] GameOverPanel _gameOverPanel;
	public bool IsGameOverPanel => _gameOverPanel.gameObject.activeSelf;
	bool _flowTime = true;

	public static Timer_Controller Instance {
		get;private set;
	}

    void Start()
    {
		startingTime = DataManager.Instance.GameTime;
		Instance = this;
		remainTime = 0;
    }

    void Update()
    {
		if (_flowTime) {
			if (startingTime > remainTime) {
				remainTime = remainTime + Time.deltaTime;
				clock.fillAmount = (float)remainTime / startingTime;
			}
			else {
				if (Timer_Controller.Instance.IsGameOverPanel == false) {
					OpenGameOverPanel(() => {
						SceneChanger.Instance.ChangeScene(SceneName.Recycle);
					});
				}
			}
		}
    }

	public void OpenGameOverPanel(System.Action callback)
	{
		_flowTime = false;
		_gameOverPanel.gameObject.SetActive(true);
		_gameOverPanel.StartFaceIn(callback);
	}
}
