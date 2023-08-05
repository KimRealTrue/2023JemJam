using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EcoDamageBar : MonoBehaviour
{
	public Image fillImage;

	private void Update()
	{
		SetEcoHpBar();
	}

	void SetEcoHpBar()
	{
		fillImage.fillAmount = DataManager.Instance.ecoDamage / (float)DataManager.Instance.EcoHp;
	}
}
