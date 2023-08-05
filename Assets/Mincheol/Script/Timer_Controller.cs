using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer_Controller : MonoBehaviour
{
    [SerializeField] float startingTime;
    [SerializeField] float remainTime;
    [SerializeField] Image clock;

    void Start()
    {
        remainTime = 0;
    }

    void Update()
    {
        if(startingTime > remainTime)
        {
            remainTime = remainTime + Time.deltaTime;
            clock.fillAmount = (float)remainTime/startingTime; 
        }

    }
}
