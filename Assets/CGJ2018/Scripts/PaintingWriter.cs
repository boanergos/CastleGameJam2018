using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.iOS;
using UnityEngine.UI;

public class PaintingWriter : MonoBehaviour 
{
	public Image PaintingImage;
	public Painting Painting;
	private bool shapeEnabled = false;
	private Dictionary<string, float> currentBlendShapes;

	void Start ()
	{
		UnityARSessionNativeInterface.ARFaceAnchorAddedEvent += FaceAdded;
		UnityARSessionNativeInterface.ARFaceAnchorUpdatedEvent += FaceUpdated;
		UnityARSessionNativeInterface.ARFaceAnchorRemovedEvent += FaceRemoved;

		UpdatePaintingImage();
	}

	public void UpdatePaintingImage ()
	{
		PaintingImage.sprite = Painting.Image;
	}

	void FaceAdded (ARFaceAnchor anchorData)
	{
		// shapeEnabled = true;

		currentBlendShapes = anchorData.blendShapes;
	}

	void FaceUpdated (ARFaceAnchor anchorData)
	{
		currentBlendShapes = anchorData.blendShapes;
	}

	void FaceRemoved (ARFaceAnchor anchorData)
	{
		// shapeEnabled = false;
	}

	void WriteValues ()
	{
		foreach (KeyValuePair<string, float> kvp in currentBlendShapes) 
		{
			Painting.AddOrRefresh(kvp.Key, kvp.Value);
		}

		Painting.Write();
		Painting.Load();
	}

	void OnGUI ()
	{
		if (GUI.Button(new Rect(0, Screen.height / 2, Screen.width, Screen.height / 2), "Register Pose"))
		{
			WriteValues();
		}
	}
}
