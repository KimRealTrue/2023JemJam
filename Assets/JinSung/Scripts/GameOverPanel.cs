using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverPanel : MonoBehaviour
{
	public Graphic[] fadeTargets;
	float _removeTime;

	Color[] _originColor;


	private void Awake()
	{
		_removeTime = 0.5f;
		_originColor = new Color[fadeTargets.Length];

		for (int i = 0; i < fadeTargets.Length; ++i) {
			_originColor[i] = fadeTargets[i].color;
		}
	}


	public void StartFaceIn(System.Action callback)
	{
		StartCoroutine(FadeIn(callback));
	}


	IEnumerator FadeIn(System.Action callback)
	{
		float time = 1;
		float dt = 1 / _removeTime;
		Vector3 originScale = transform.localScale;

		for (int i = 0; i < fadeTargets.Length; ++i) {
			Color c = _originColor[i];
			fadeTargets[i].color = new Color(c.r, c.g, c.b, 0);
		}

		while (time > 0) {
			time -= Time.deltaTime * dt;
			for (int i = 0; i < fadeTargets.Length; ++i) {
				Color c = _originColor[i];
				fadeTargets[i].color = new Color(c.r, c.g, c.b, (1-time));
			}

			if (time <= 0) {
				time = 0;
				break;
			}
			yield return null;
		}

		for (int i = 0; i < fadeTargets.Length; ++i) {
			Color c = _originColor[i];
			fadeTargets[i].color = new Color(c.r, c.g, c.b, 1);
		}


		yield return new WaitForSeconds(1f);

		callback?.Invoke();
	}
}
