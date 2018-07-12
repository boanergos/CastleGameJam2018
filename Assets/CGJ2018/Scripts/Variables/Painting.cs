using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Newtonsoft.Json;

[CreateAssetMenu]
public class Painting : ScriptableObject 
{
	private string dataPath;

	public Sprite Image;
	public int Difficulty;
	[HideInInspector]
	public List<ExpressionData> Expression = new List<ExpressionData>();

	public void OnEnable()
	{
		dataPath = Application.dataPath + "/Resources/" + this.name;
	}

	public void AddOrRefresh(string key, float value)
	{
		if (!ContainsKey(key))
			Add(key, value);
		else
			UpdateValue(key, value);
	}

	public bool ContainsKey(string key)
	{
		foreach (ExpressionData e in Expression)
		{
			if(e.Key == key)
				return true;
		}

		return false;
	}

	public void Add(string key, float value)
	{
		Expression.Add(new ExpressionData(key, value));
	}

	public float GetValue(string key)
	{
		foreach (ExpressionData e in Expression)
		{
			if (e.Key == key)
				return e.Value;
		}

		return 0;
	}

	public void UpdateValue(string key, float value)
	{
		foreach (ExpressionData e in Expression)
		{
			if (e.Key == key)
			{
				e.Value = value;
				return;
			}
		}
	}

	public void Load()
	{
		TextAsset datafile = Resources.Load(this.name) as TextAsset;
		
		if (datafile)
			Expression = JsonConvert.DeserializeObject<List<ExpressionData>>(datafile.text);
	}

	public void Write()
	{
		#if UNITY_EDITOR_OSX
		string dataContent = JsonConvert.SerializeObject(Expression);
		System.IO.File.WriteAllText(dataPath + ".txt", dataContent);
		UnityEditor.AssetDatabase.Refresh();
		#endif
	}
}

[Serializable]
public class ExpressionData
{
	public string Key;
	public float Value;

	public ExpressionData(string key, float value)
	{
		this.Key = key;
		this.Value = value;
	}
}