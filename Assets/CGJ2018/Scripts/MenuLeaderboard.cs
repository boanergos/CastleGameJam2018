using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuLeaderboard : MonoBehaviour 
{
	public LeaderboardManager LeaderboardManager;
	public Transform Grid;
	public GameObject GridElement;

	private void OnEnable ()
	{
		foreach (Transform child in Grid.transform) 
		{
     		Destroy(child.gameObject);
 		}



		for (int i = 0; i < Mathf.Clamp(LeaderboardManager.PlayerScoreElements.Count, 0, 9); i++)
		{
			float playerScore = LeaderboardManager.PlayerScoreElements[i].PlayerScore;
			byte[] rawImage = LeaderboardManager.PlayerScoreElements[i].PlayerPhoto;
			Texture2D photoProfile = new Texture2D(1, 1);
			ImageConversion.LoadImage(photoProfile, rawImage);
			Sprite spriteProfile = Sprite.Create(photoProfile, new Rect(0, 0, photoProfile.width, photoProfile.height), Vector2.zero);

			GameObject element = Instantiate(GridElement, transform.position, Quaternion.identity, Grid) as GameObject;
			element.transform.Find("Photo").GetComponent<Image>().sprite = spriteProfile;
			element.transform.Find("Score").transform.Find("ScoreTxt").GetComponent<Text>().text = Mathf.RoundToInt(playerScore).ToString();
			element.SetActive(true);
		}
	}
}
