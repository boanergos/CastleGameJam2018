using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetReadyCountdown : MonoBehaviour 
{
	public Image CountdownCircle;
	public GameEvent OnPaintingCountdownFinished;
	private float countdownTimer;
	private const float countdownGoal = 1.5f;

	void OnEnable ()
	{
		countdownTimer = 0;
	}

	void Update ()
	{
		countdownTimer += Time.deltaTime;

		CountdownCircle.fillAmount = countdownTimer / countdownGoal;

		if (countdownTimer >= countdownGoal)
		{
			OnPaintingCountdownFinished.Raise();
		}
	}
}
