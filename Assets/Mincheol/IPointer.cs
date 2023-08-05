using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class IPointer : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] RectTransform[] stars;
    [SerializeField] bool starSpin;

    public void OnPointerEnter(PointerEventData eventData)
    {
        starSpin = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        starSpin = false;

        for(int i = 0; i<stars.Length; i++)
            {
                 stars[i].localRotation = Quaternion.identity;
            }
    }

    void Update()
    {
        if (starSpin)
        {
            float rotationSpeed = 100f;

            for (int i = 0; i < stars.Length; i++)
            {
                Vector3 rotationAmount = new Vector3(0, rotationSpeed * Time.deltaTime, 0);
                stars[i].Rotate(rotationAmount, Space.Self);
            }
        }
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
