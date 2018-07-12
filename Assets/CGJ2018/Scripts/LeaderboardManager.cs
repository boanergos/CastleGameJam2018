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
		filePath = System.IO.Path.Combine(Application.persistentDataPath, "Scores.txt");

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

	private void LoadData ()
	{
		string dataContent = System.IO.File.ReadAllText(filePath);
		
		if (!String.IsNullOrEmpty(dataContent))
			PlayerScoreElements = JsonConvert.DeserializeObject<List<PlayerScoreElement>>(dataContent);
	}

	private void SaveData ()
	{
		string dataContent = JsonConvert.SerializeObject(PlayerScoreElements);
		System.IO.File.WriteAllText(filePath, dataContent);
	}

	private void OnGUI ()
	{
		GUI.Box(new Rect(0, 0, 200, 200), PlayerScoreElements.Count.ToString());
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