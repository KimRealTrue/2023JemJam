using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio_Controller : MonoBehaviour
{
    public static Audio_Controller instance;

    [SerializeField] AudioClip BGM_EndingLose;
    [SerializeField] AudioClip BGM_EndingWin;
    [SerializeField] AudioClip BGM_GameStart;
    [SerializeField] AudioClip BGM_PlayingGame;
    [SerializeField] AudioClip BGM_PlayingRecycle;

    [SerializeField] AudioClip Effect_RecycleSuccess;
    [SerializeField] AudioClip Effect_RecycleFail;
    [SerializeField] AudioClip Effect_SceneMove;
    [SerializeField] AudioClip Effect_TrashPickup;
    [SerializeField] AudioClip Effect_TrashDrop;
    [SerializeField] AudioClip Effect_TrashFail;

    [SerializeField] AudioSource BGM_AudioSource;
    [SerializeField] AudioSource Effect_AudioSource;


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

	public void EffectPlay_RecycleSuccess()
    {
        Effect_AudioSource.PlayOneShot(Effect_RecycleSuccess);
    }
	public void EffectPlay_RecycleFail()
	{
		Effect_AudioSource.PlayOneShot(Effect_RecycleFail);
	}
	public void EffectPlay_SceneMove()
    {
        Effect_AudioSource.PlayOneShot(Effect_SceneMove);
    }
    public void EffectPlay_TrashPickup()
    {
        Effect_AudioSource.PlayOneShot(Effect_TrashPickup);
    }    
	public void EffectPlay_TrashFail()
    {
        Effect_AudioSource.PlayOneShot(Effect_TrashFail);
    }
    public void EffectPlay_TrashDrop()
    {
        Effect_AudioSource.PlayOneShot(Effect_RecycleSuccess);
    }

    public void BGMPlay_EndingLose()
    {
        BGM_AudioSource.clip = BGM_EndingLose;
        BGM_AudioSource.Play();
    }

    public void BGMPlay_EndingWin()
    {
        BGM_AudioSource.clip = BGM_EndingWin;
        BGM_AudioSource.Play();
    }

    public void BGMPlay_GameStart()
    {
        BGM_AudioSource.clip = BGM_GameStart;
        BGM_AudioSource.Play();
    }

    public void BGMPlay_PlayingGame()
    {
        BGM_AudioSource.clip = BGM_PlayingGame;
        BGM_AudioSource.Play();
    }

    public void BGMPlay_PlayingRecycle()
    {
        BGM_AudioSource.clip = BGM_PlayingRecycle;
        BGM_AudioSource.Play();
    }
}
