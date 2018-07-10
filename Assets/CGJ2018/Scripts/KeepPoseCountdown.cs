using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeepPoseCountdown : MonoBehaviour 
{
	public FloatVariable PoseSimilarity;
	public Image CountdownCircle;
	public GameEvent OnPoseMatchCompleted;
	private float poseCompletion;
	private float poseFillSpeed = 0.5f;

	void Awake ()
	{
		poseCompletion = 0f;
	}

	void Update () 
	{
		poseCompletion = Mathf.Clamp(poseCompletion, 0f, 1.1f);

		if (PoseSimilarity.Value > 99f)
			poseCompletion += poseFillSpeed * Time.deltaTime;
		else
			poseCompletion -= poseFillSpeed * Time.deltaTime;

		CountdownCircle.fillAmount = poseCompletion;

		if (poseCompletion > 1f)
		{
			OnPoseMatchCompleted.Raise();
			poseCompletion = 0f;
		}
	}
}
