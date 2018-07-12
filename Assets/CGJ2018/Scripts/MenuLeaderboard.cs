using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuLeaderboard : MonoBehaviour 
{
	public Transform Grid;
	public GameObject GridElement;

	private void OnEnable ()
	{
		foreach (Transform child in Grid.transform) 
		{
     		Destroy(child.gameObject);
 		}

		for (int i = 0; i < 9; i++)
		{
			GameObject element = Instantiate(GridElement, transform.position, Quaternion.identity, Grid) as GameObject;
			element.transform.Find("Photo").GetComponent<Image>();
			element.transform.Find("Score").transform.Find("ScoreTxt").GetComponent<Text>().text = "100";
			element.SetActive(true);
		}
	}
}
