using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderboardManager : MonoBehaviour 
{
	private float startScore = 1000000;
	private bool lowerScore = false;
	public FloatVariable PlayerScore;

	public void StartRecording ()
	{
		PlayerScore.Value = startScore;
		lowerScore = true;
	}

	public void StopRecording ()
	{
		lowerScore = false;
	}

	private void Update ()
	{
		if (lowerScore)
			PlayerScore.Value -= Time.deltaTime;
	}
}
