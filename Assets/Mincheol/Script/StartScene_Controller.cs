using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartScene_Controller : MonoBehaviour
{
    public Text pressToStart;
    public GameObject stageUI;
   
    void Start()
    {
		SceneChanger.Create();
        StartCoroutine(FadeTextToFullAlpha());
        stageUI.SetActive(false);


		Audio_Controller.instance.BGMPlay_GameStart();
    }

    public IEnumerator FadeTextToFullAlpha() // 알파값 0에서 1로 전환
    {
        pressToStart.color = new Color(pressToStart.color.r, pressToStart.color.g, pressToStart.color.b, 0);
        while (pressToStart.color.a < 1.0f)
        {
            pressToStart.color = new Color(pressToStart.color.r, pressToStart.color.g, pressToStart.color.b, pressToStart.color.a + (Time.deltaTime));
            yield return null;
        }
        StartCoroutine(FadeTextToZero());
    }

    public IEnumerator FadeTextToZero()  // 알파값 1에서 0으로 전환
    {
        pressToStart.color = new Color(pressToStart.color.r, pressToStart.color.g, pressToStart.color.b, 1);
        while (pressToStart.color.a > 0.0f)
        {
            pressToStart.color = new Color(pressToStart.color.r, pressToStart.color.g, pressToStart.color.b, pressToStart.color.a - (Time.deltaTime));
            yield return null;
        }
        StartCoroutine(FadeTextToFullAlpha());
    }

    public void GameStart()
    {
        pressToStart.gameObject.SetActive(false);
        stageUI.SetActive(true);
    }

    
    public void StartEasy()
    {
        SceneManager.LoadScene("Easy");
    }

    public void StartNormal()
    {
        SceneManager.LoadScene("Normal");
    }

    public void StartHard()
    {
        SceneManager.LoadScene("Hard");
    }   
}

