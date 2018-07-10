using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintingsManager : MonoBehaviour 
{
	public PaintingReader PaintingReader;
	public Painting[] Paintings;
	public GameEvent OnGameCompleted;
	private int currentPaintingIndex;

	public void Start ()
	{
		GetNewPainting();
	}

	public void GetNewPainting ()
	{
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
