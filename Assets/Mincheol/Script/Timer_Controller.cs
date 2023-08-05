using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer_Controller : MonoBehaviour
{
    [SerializeField] float startingTime;
    [SerializeField] float remainTime;
    [SerializeField] Image clock;
    [SerializeField] GameOverPanel _gameOverPanel;
	bool _flowTime = true;

    void Start()
    {
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
				_gameOverPanel.gameObject.SetActive(true);
				_gameOverPanel.StartFaceIn(()=> {
					SceneChanger.Instance.ChangeScene(SceneName.Recycle);
				});
				_flowTime = false;
			}
		}
    }
}
