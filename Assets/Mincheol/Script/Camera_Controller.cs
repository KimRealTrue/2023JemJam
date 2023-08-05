using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Controller : MonoBehaviour
{
    [SerializeField] Camera[] allCameras;
    [SerializeField] int currentCameraIndex;
    [SerializeField] Camera UICamera;

    public void Start()
    {
        for(int i = 1; i<allCameras.Length; i++)
        {
            allCameras[i].gameObject.SetActive(false);
        }
        allCameras[0].gameObject.SetActive(true);
        currentCameraIndex = 0;
    }


	public Camera CurrentCamera => allCameras[currentCameraIndex];




	public void OnClickRight()
    {

		int stage = DataManager.Instance.stage;

		int max = 0;
		if (stage == 1) {
			max = 3;
		}
		else if (stage == 1) {
			max = 4;
		}
		else {
			max = 6;
		}

		if (currentCameraIndex + 1 >= max) {
			return;
		}

		Audio_Controller.instance.EffectPlay_SceneMove();


		switch (currentCameraIndex)
        {
            case 0 : allCameras[0].gameObject.SetActive(false); allCameras[1].gameObject.SetActive(true);  currentCameraIndex++; break;
            case 1 : allCameras[1].gameObject.SetActive(false); allCameras[2].gameObject.SetActive(true);  currentCameraIndex++; break;
            case 2 : allCameras[2].gameObject.SetActive(false); allCameras[3].gameObject.SetActive(true);  currentCameraIndex++; break;
            case 3 : allCameras[3].gameObject.SetActive(false); allCameras[4].gameObject.SetActive(true);  currentCameraIndex++; break;
            case 4 : allCameras[4].gameObject.SetActive(false); allCameras[5].gameObject.SetActive(true);  currentCameraIndex++; break;
            //case 5 : allCameras[5].gameObject.SetActive(false); allCameras[6].gameObject.SetActive(true);  currentCameraIndex++; break;
            default : break;
        }
    }

    public void OnClickLeft()
    {
        switch(currentCameraIndex)
        {
            case 1 : allCameras[1].gameObject.SetActive(false); allCameras[0].gameObject.SetActive(true);  currentCameraIndex--; break;
            case 2 : allCameras[2].gameObject.SetActive(false); allCameras[1].gameObject.SetActive(true);  currentCameraIndex--;  break;
            case 3 : allCameras[3].gameObject.SetActive(false); allCameras[2].gameObject.SetActive(true);  currentCameraIndex--;  break;
            case 4 : allCameras[4].gameObject.SetActive(false); allCameras[3].gameObject.SetActive(true);  currentCameraIndex--;  break;
            case 5 : allCameras[5].gameObject.SetActive(false); allCameras[4].gameObject.SetActive(true);  currentCameraIndex--;  break;
            //case 6 : allCameras[6].gameObject.SetActive(false); allCameras[5].gameObject.SetActive(true);  currentCameraIndex--; break;
            default : break;
        }
    }
}

