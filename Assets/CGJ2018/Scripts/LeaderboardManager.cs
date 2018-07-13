using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Newtonsoft.Json;

public class LeaderboardManager : MonoBehaviour 
{
	private float startScore = 100000;
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
			PlayerScore.Value -= Time.deltaTime * 100f;
	}

	public void AddPlayerScoreElement ()
	{
		Texture2D lastFace = ScreenshotManager.FaceScreenshots[ScreenshotManager.FaceScreenshots.Count - 1];
		byte[] lastFaceJpg = ImageConversion.EncodeToJPG(lastFace, 60);
		PlayerScoreElement element = new PlayerScoreElement(PlayerScore.Value, lastFaceJpg);
		PlayerScoreElements.Add(element);

		SaveData();
	}

	private void LoadData ()
	{
		if (!System.IO.File.Exists(filePath))
		{
			Debug.Log("Leaderboard file doesn't exist");
			return;
		}

		string dataContent = System.IO.File.ReadAllText(filePath);

		PlayerScoreElements = JsonConvert.DeserializeObject<List<PlayerScoreElement>>(dataContent);
	}

	private void SaveData ()
	{
		string dataContent = JsonConvert.SerializeObject(PlayerScoreElements);
		System.IO.File.WriteAllText(filePath, dataContent);
	}
}

[Serializable]
public class PlayerScoreElement
{
	public float PlayerScore;
	public byte[] PlayerPhoto;

	public PlayerScoreElement (float score, byte[] photo)
	{
		this.PlayerScore = score;
		this.PlayerPhoto = photo;
	}
}