using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AfterGame_Controller : MonoBehaviour
{
    public Text pressToRetry;
	public bool clear;

    void Start()
    {
        StartCoroutine(FadeTextToFullAlpha());
		if (clear) {
			Audio_Controller.instance.BGMPlay_EndingWin();
		}
		else {
			Audio_Controller.instance.BGMPlay_EndingLose();
		}
	}

    public IEnumerator FadeTextToFullAlpha() // 알파값 0에서 1로 전환
    {
        pressToRetry.color = new Color(pressToRetry.color.r, pressToRetry.color.g, pressToRetry.color.b, 0);
        while (pressToRetry.color.a < 1.0f)
        {
            pressToRetry.color = new Color(pressToRetry.color.r, pressToRetry.color.g, pressToRetry.color.b, pressToRetry.color.a + (Time.deltaTime));
            yield return null;
        }
        StartCoroutine(FadeTextToZero());
    }

    public IEnumerator FadeTextToZero()  // 알파값 1에서 0으로 전환
    {
        pressToRetry.color = new Color(pressToRetry.color.r, pressToRetry.color.g, pressToRetry.color.b, 1);
        while (pressToRetry.color.a > 0.0f)
        {
            pressToRetry.color = new Color(pressToRetry.color.r, pressToRetry.color.g, pressToRetry.color.b, pressToRetry.color.a - (Time.deltaTime));
            yield return null;
        }
        StartCoroutine(FadeTextToFullAlpha());
    }

    public void Retry()
    {
        SceneManager.LoadScene("StartScene");
    }
}
