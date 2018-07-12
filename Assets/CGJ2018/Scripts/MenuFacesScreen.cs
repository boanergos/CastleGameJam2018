using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuFacesScreen : MonoBehaviour 
{
	public ScreenshotManager SM;

	public Image[] FacePanels;
	public Sprite BlackScreenshot;

	void OnEnable () 
	{
		foreach (Image facePanel in FacePanels)
		{
			facePanel.sprite = BlackScreenshot;
		}

		StartCoroutine(ShowFaces());
	}

	IEnumerator ShowFaces ()
	{
		for (int i = 0; i < FacePanels.Length; i++)
		{
			yield return new WaitForSeconds(0.2f);

			Texture2D tex2d = SM.FaceScreenshots[i];
			Sprite spr = Sprite.Create(tex2d, new Rect(0, 0, tex2d.width, tex2d.height), Vector2.zero);

			if (FacePanels[i])
				FacePanels[i].sprite = spr;
		}
	}
}
