using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public enum SceneName
{
	Start,
	Main_Easy,
	Main_Normal,
	Main_Hard,
	Recycle,
	End_Success,
	End_Fail
}

public class SceneChanger : MonoBehaviour
{
	public static SceneChanger Instance {
		get {
			if (_instance == null) {
				Create();
			}
			return _instance;
		}
	}

	private static SceneChanger _instance;
	public static void Create()
	{
		GameObject go = new GameObject("DataManager");
		_instance = go.AddComponent<SceneChanger>();
		DontDestroyOnLoad(go);
	}


	public void ChangeScene(SceneName sceneName)
	{
		SceneManager.LoadScene((int)sceneName);
	}
}
