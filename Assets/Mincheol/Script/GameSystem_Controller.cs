using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSystem_Controller : MonoBehaviour
{
    public GameObject tutorial;
    public Button lButton;
	public Button rButton;
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
		DataManager.Instance.ecoDamage = 0;
		DataManager.Instance.stage = stageNumber;

		Audio_Controller.instance.BGMPlay_PlayingGame();
		ShowTutorial();
	}

    void ShowTutorial()
    {
        tutorial.SetActive(true);
		gameStart = false;
		lButton.interactable = false;
        rButton.interactable = false;
    }

    public void CloseTutorial()
    {
        tutorial.SetActive(false);
		gameStart = true;

		lButton.interactable = true;
        rButton.interactable = true;
    }
}
