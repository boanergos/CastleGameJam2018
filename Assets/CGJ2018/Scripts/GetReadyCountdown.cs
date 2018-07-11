using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetReadyCountdown : MonoBehaviour 
{
	public Image CountdownCircle;
	public GameEvent OnPaintingCountdownFinished;

	public IntVariable PosesCompleted;
	public IntVariable PosesMax;

	private float countdownTimer;
	private const float countdownGoal = 1.5f;

	void OnEnable ()
	{
		countdownTimer = 0;

		CheckIfGameEnded();
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

	public void CheckIfGameEnded ()
	{
		if (PosesCompleted.Value > PosesMax.Value - 1)
		{
			OnPaintingCountdownFinished.Raise();
			this.gameObject.SetActive(false);
		}
	}
}
