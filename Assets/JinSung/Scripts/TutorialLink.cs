using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialLink : MonoBehaviour
{
	[SerializeField] Button _playButton;


	private void Start()
	{
		StartCoroutine(LateStart());
	}

	IEnumerator LateStart()
	{

		yield return null;
		GameSystem_Controller.instance.tutorial = gameObject;
		_playButton.onClick.AddListener(GameSystem_Controller.instance.CloseTutorial);
	}

}
