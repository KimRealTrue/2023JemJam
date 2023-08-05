using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSystem_Controller : MonoBehaviour
{
    public GameObject tutorial;
	public int stageNumber;
    public int spawned;

	public bool gameStart = false;

    public static GameSystem_Controller instance = null;

    void Awake()
    {
		if (null == instance)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }


	public void PlayGame()
	{
		Audio_Controller.instance.BGMPlay_PlayingGame();
		ShowTutorial();
	}

    void ShowTutorial()
    {
		gameStart = false;
    }

    public void CloseTutorial()
    {
        tutorial.SetActive(false);
		gameStart = true;
    }
}
