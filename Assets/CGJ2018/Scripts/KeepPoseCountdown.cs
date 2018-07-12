using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeepPoseCountdown : MonoBehaviour 
{
	public FloatVariable PoseSimilarity;
	public Image CountdownCircle;

	public GameEvent OnPoseMatchCompleted;
	public GameEvent OnPaintingCountdownFinished;

	private float poseCompletion;
	private float poseFillSpeed = 0.5f;
	private bool record;

	void Awake ()
	{
		poseCompletion = 0f;
	}

	void Update () 
	{
		if (!record)
			return;

		// CHEATCODE
		if (Input.touchCount >= 2)
			OnPoseMatchCompleted.Raise();

		poseCompletion = Mathf.Clamp(poseCompletion, 0f, 1.1f);

		if (PoseSimilarity.Value > 99f)
			poseCompletion += poseFillSpeed * Time.deltaTime;
		else
			poseCompletion -= (poseFillSpeed / 2f) * Time.deltaTime;

		CountdownCircle.fillAmount = poseCompletion;

		if (poseCompletion > 1f)
		{
			OnPoseMatchCompleted.Raise();
			poseCompletion = 0f;
		}
	}

	public void StartRecording ()
	{
		poseCompletion = 0f;
		record = true;
	}

	public void StopRecording ()
	{
		poseCompletion = 0f;
		record = false;
	}
}
