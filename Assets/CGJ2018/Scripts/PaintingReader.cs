using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.iOS;
using UnityEngine.UI;

public class PaintingReader : MonoBehaviour 
{
	public Painting Painting;
	public Dictionary<string, float> CurrentExpression;
	public float Similarity;
	public Text DebugText;

	void Start ()
	{
		UnityARSessionNativeInterface.ARFaceAnchorUpdatedEvent += FaceUpdated;
	}

	void FaceUpdated (ARFaceAnchor anchorData)
	{
		CurrentExpression = anchorData.blendShapes;

		CompareFace();
	}
	
	void CompareFace ()
	{
		// if (!CurrentExpression)
		// 	return;

		float error = 0f;

		foreach (KeyValuePair<string, float> kvp in CurrentExpression)
		{
			if (Painting.Expression.ContainsKey(kvp.Key))
			{
				error += Painting.Expression[kvp.Key] - kvp.Value;
			}
		}

		float errorPercentage = error / 52f;
		Similarity = 100 - (Mathf.Abs(errorPercentage) * 100);

		if (DebugText)
			DebugText.text = Similarity.ToString();
	}
}
