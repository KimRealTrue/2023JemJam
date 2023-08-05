using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecycleTimer
{
	private float _totalTime = 30;
	private float _currentTime;
	private float _speed = 1;
	private bool _enableflowTime = false;
	private bool _gameStop = false;


	public delegate void OnTimerDelegate(float maxTime, float time);
	public event OnTimerDelegate OnTimerStart;
	public event OnTimerDelegate OnTimerUpdate;
	public event OnTimerDelegate OnTimerEnd;

	public RecycleTimer(float time, float speed = 1)
	{
		_enableflowTime = false;
		_gameStop = false;
		_speed = speed;
		_totalTime = time;
		_currentTime = time;
	}

	public void TimerStart()
	{
		_enableflowTime = true;
		OnTimerStart?.Invoke(_totalTime, _currentTime);
	}


	public void Update()
	{
		if (_enableflowTime) {
			FlowTime();
		}
	}

	private void FlowTime()
	{
		_currentTime -= Time.deltaTime * _speed;
		OnTimerUpdate?.Invoke(_totalTime, _currentTime);
		if (_currentTime <= 0) {
			_currentTime = 0;
			_enableflowTime = false;
			OnTimerEnd?.Invoke(_totalTime, _currentTime);
		}
	}

	public void Stop()
	{
		_enableflowTime = false;
		_gameStop = false;
	}
}
