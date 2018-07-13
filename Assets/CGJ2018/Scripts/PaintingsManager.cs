using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintingsManager : MonoBehaviour 
{
	// References
	public PaintingReader PaintingReader;
	public Painting[] Paintings;
	public List<Painting> PaintingsQueue;
	// Variables
	public IntVariable PosesCompleted;
	public IntVariable PosesMax;
	public FloatVariable PlayerScore;
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
			// I know it's bad but it's a jam lol
			GameObject.Find("CountdownCircle").SetActive(false);
			return;
		}
		else
		{
			// I know it's bad but it's a jam lol
			GameObject.Find("DebugText").SetActive(false);
		}

		ResetGameParameters();
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
		if (PosesCompleted.Value > PosesMax.Value - 1)
		{
			OnGameCompleted.Raise();
			return;
		}

		// PaintingReader.Painting = Paintings[PosesCompleted.Value];
		PaintingReader.Painting = PaintingsQueue[PosesCompleted.Value];
		PaintingReader.Refresh();

		PosesCompleted.Value++;
	}

	public void ResetGameParameters ()
	{
		PosesCompleted.Value = 0;
		PlayerScore.Value = 0;
		// ShufflePaintings();
		MakePaintingsQueue();
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

	public void MakePaintingsQueue ()
	{
		List<Painting> AllPaintings = new List<Painting>();

		foreach (Painting p in Paintings)
		{
			AllPaintings.Add(p);
		}

		PaintingsQueue = new List<Painting>();

		for (int i = 0; i < 5; i++)
		{
			bool found = false;
			while (!found)
			{
				int randomPainting = Random.Range(0, AllPaintings.Count);
				int paintingDifficulty = AllPaintings[randomPainting].Difficulty;

				if (paintingDifficulty == (i + 1))
				{
					PaintingsQueue.Add(AllPaintings[randomPainting]);
					AllPaintings.Remove(AllPaintings[randomPainting]);
					found = true;
				}
			}
		}
	}
}
