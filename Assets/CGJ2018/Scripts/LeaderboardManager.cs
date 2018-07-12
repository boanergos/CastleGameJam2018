using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Newtonsoft.Json;

public class LeaderboardManager : MonoBehaviour 
{
	private float startScore = 1000000;
	private bool lowerScore = false;
	private string filePath;

	public ScreenshotManager ScreenshotManager;
	public FloatVariable PlayerScore;
	public List<PlayerScoreElement> PlayerScoreElements;

	public void OnEnable()
	{
		filePath = Application.dataPath + "/Resources/Scores/Scores.txt";

		LoadData();
	}

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

	public void AddPlayerScoreElement ()
	{
		Texture2D lastFace = ScreenshotManager.FaceScreenshots[ScreenshotManager.FaceScreenshots.Count - 1];
		PlayerScoreElement element = new PlayerScoreElement(PlayerScore.Value, lastFace);
		PlayerScoreElements.Add(element);

		SaveData();
	}

	public void LoadData ()
	{
		TextAsset datafile = Resources.Load(this.name) as TextAsset;
		
		if (datafile)
			PlayerScoreElements = JsonConvert.DeserializeObject<List<PlayerScoreElement>>(datafile.text);
	}

	public void SaveData ()
	{
		string dataContent = JsonConvert.SerializeObject(PlayerScoreElements);
		System.IO.File.WriteAllText(filePath, dataContent);
	}
}

[Serializable]
public class PlayerScoreElement
{
	public float PlayerScore;
	public Texture2D PlayerPhoto;

	public PlayerScoreElement (float score, Texture2D photo)
	{
		this.PlayerScore = score;
		this.PlayerPhoto = photo;
	}
}