using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.iOS;
using UnityEngine.UI;

public class PaintingReader : MonoBehaviour 
{
	public Image PaintingImage;
	public Painting Painting;
	public Dictionary<string, float> CurrentExpression;
	public FloatVariable Similarity;
	public Text DebugText;

	public void Refresh ()
	{
		Similarity.Value = 0f;
		Painting.Load();
		UpdatePaintingImage();
	}

	private void Start ()
	{
		UnityARSessionNativeInterface.ARFaceAnchorUpdatedEvent += FaceUpdated;
	}

	private void UpdatePaintingImage ()
	{
		PaintingImage.sprite = Painting.Image;
	}

	private void FaceUpdated (ARFaceAnchor anchorData)
	{
		CurrentExpression = anchorData.blendShapes;

		CompareFace();
	}
	
	private void CompareFace ()
	{
		float error = 0f;

		foreach (KeyValuePair<string, float> kvp in CurrentExpression)
		{
			if (Painting.ContainsKey(kvp.Key))
			{
				error += Painting.GetValue(kvp.Key) - kvp.Value;
			}
		}

		// Magic numbers™
		float errorPercentage = error / 52f;
		Similarity.Value = 100 - (Mathf.Abs(errorPercentage) * 100);

		// if (DebugText)
		// 	DebugText.text = Similarity.Value.ToString();
	}
}
