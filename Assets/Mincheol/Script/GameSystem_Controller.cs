using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSystem_Controller : MonoBehaviour
{
    [SerializeField] GameObject tutorial;
    [SerializeField] Button lButton;
    [SerializeField] Button rButton;

    public int spawned;

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

    void Start()
    {
        ShowTutorial();
    }

    void ShowTutorial()
    {
        tutorial.SetActive(true);
        Time.timeScale = 0;

        lButton.interactable = false;
        rButton.interactable = false;
    }

    public void CloseTutorial()
    {
        tutorial.SetActive(false);
        Time.timeScale = 1;

        lButton.interactable = true;
        rButton.interactable = true;
    }
}
