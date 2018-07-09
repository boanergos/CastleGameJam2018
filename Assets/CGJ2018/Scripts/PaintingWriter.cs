using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.iOS;

public class PaintingWriter : MonoBehaviour 
{
	public Painting Painting;
	public bool IsWriting;

	private bool shapeEnabled = false;

	void Start ()
	{
		UnityARSessionNativeInterface.ARFaceAnchorAddedEvent += FaceAdded;
		UnityARSessionNativeInterface.ARFaceAnchorUpdatedEvent += FaceUpdated;
		UnityARSessionNativeInterface.ARFaceAnchorRemovedEvent += FaceRemoved;
	}

	void FaceAdded (ARFaceAnchor anchorData)
	{
		// shapeEnabled = true;

		if (!IsWriting)
			return;

		foreach (KeyValuePair<string, float> kvp in anchorData.blendShapes) 
		{
			if (!Painting.Expression.ContainsKey(kvp.Key))
				Painting.Expression.Add(kvp);
		} 
	}

	void FaceUpdated (ARFaceAnchor anchorData)
	{
		if (!IsWriting)
			return;

		foreach (KeyValuePair<string, float> kvp in anchorData.blendShapes) 
		{
			if (Painting.Expression.ContainsKey(kvp.Key))
				Painting.Expression[kvp.Key] = kvp.Value;
		} 
	}

	void FaceRemoved (ARFaceAnchor anchorData)
	{
		// shapeEnabled = false;
	}
}
