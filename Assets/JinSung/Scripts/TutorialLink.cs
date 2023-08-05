using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialLink : MonoBehaviour
{
	[SerializeField] Button _playButton;
	[SerializeField] Button _lButton;
	[SerializeField] Button _rButton;


	private void Start()
	{
		StartCoroutine(LateStart());
	}

	IEnumerator LateStart()
	{

		yield return null;
		GameSystem_Controller.instance.tutorial = gameObject;
		GameSystem_Controller.instance.rButton = _rButton;
		GameSystem_Controller.instance.lButton = _lButton;
		_playButton.onClick.AddListener(GameSystem_Controller.instance.CloseTutorial);
	}

}
