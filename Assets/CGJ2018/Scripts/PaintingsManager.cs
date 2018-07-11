﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintingsManager : MonoBehaviour 
{
	// References
	public PaintingReader PaintingReader;
	public Painting[] Paintings;
	// Variables
	public IntVariable PosesCompleted;
	public IntVariable PosesMax;
	// Events
	public GameEvent OnGameCompleted;
	public GameEvent OnStartGamePressed;
	// Debug
	public bool DebugMode;

	public void Start ()
	{
		PaintingReader.DebugMode = DebugMode;

		if (DebugMode)
		{
			PaintingReader.Refresh();
			GameObject.Find("CountdownCircle").SetActive(false);
			return;
		}
		else
		{
			GameObject.Find("DebugText").SetActive(false);
		}

		ShufflePaintings();
	}

	public void StartGame ()
	{
		PosesCompleted.Value = 0;
		GetNewPainting();
	}

	public void GetNewPainting ()
	{
		if (DebugMode)
			return;

		// if (currentPaintingIndex >= Paintings.Length)
		if (PosesCompleted.Value > PosesMax.Value)
		{
			OnGameCompleted.Raise();
			return;
		}

		PaintingReader.Painting = Paintings[PosesCompleted.Value];
		PaintingReader.Refresh();

		PosesCompleted.Value++;
	}

	public void ShufflePaintings ()
	{
        for (int t = 0; t < Paintings.Length; t++ )
        {
            Painting tmp = Paintings[t];
            int r = Random.Range(t, Paintings.Length);
            Paintings[t] = Paintings[r];
            Paintings[r] = tmp;
        }
	}
}
