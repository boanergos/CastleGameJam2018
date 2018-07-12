using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenshotManager : MonoBehaviour 
{
	public List<Texture2D> FaceScreenshots;

	private bool grab;

	public void Init ()
	{
		FaceScreenshots = new List<Texture2D>();
	}

	// public void OnGUI ()
	// {
	// 	if (GUI.Button(new Rect(0, Screen.height / 2, Screen.width, Screen.height / 2), "SCREEN"))
	// 	{
	// 		Init();
	// 		grab = true;
	// 	}
	// }

	public void TakeScreenshot ()
	{
		grab = true;
	}

	private void OnPostRender()
    {
        if (grab)
        {
            Texture2D texture = new Texture2D(Screen.width / 2, Screen.height / 2, TextureFormat.RGB24, false);
            texture.ReadPixels(new Rect(Screen.width / 4, 0, Screen.width / 2, Screen.height), 0, 0, false);
            texture.Apply();

			FaceScreenshots.Add(texture);

            grab = false;
        }
    }
}
