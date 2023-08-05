using System.Collections;
using System.Collections.Generic;
using UnityEngine;




[CreateAssetMenu(fileName = "Global Data", menuName = "Scriptable Object/GlobalData", order = int.MaxValue)]
public class GlobalData : ScriptableObject
{

	[Header("최대 환경 체력")]
	public int ecoHP;//0

	[Header("게임 플레이 타임")]
	public float gameTime;//0

	[Header("쓰레기 드랍 타임(최소, 최대)")]
	public Vector2 dropTime;//0
	
	[Header("실패 데미지")]
	public int ecoDamage;//0


	[Header("재활용 플레이 타임")]
	public int recycleTime;//0
	
	[Header("재활용 실패 데미지")]
	public int recycleEcoDamage;//0

}