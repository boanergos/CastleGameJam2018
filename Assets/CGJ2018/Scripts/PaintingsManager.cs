using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintingsManager : MonoBehaviour 
{
	public PaintingReader PaintingReader;
	public Painting[] Paintings;
	public GameEvent OnGameCompleted;
	public bool DebugMode;
	private int currentPaintingIndex;

	public void Start ()
	{
		PaintingReader.DebugMode = DebugMode;

		if (DebugMode)
		{
			PaintingReader.Refresh();
			GameObject.Find("CountdownCircle").SetActive(false);
			return;
		}

		GetNewPainting();
	}

	public void GetNewPainting ()
	{
		if (DebugMode)
			return;

		if (currentPaintingIndex >= Paintings.Length)
		{
			OnGameCompleted.Raise();
			return;
		}

		PaintingReader.Painting = Paintings[currentPaintingIndex];
		PaintingReader.Refresh();

		currentPaintingIndex++;
	}
}
