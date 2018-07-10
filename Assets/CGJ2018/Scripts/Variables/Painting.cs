using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu]
public class Painting : ScriptableObject 
{
	public Sprite Image;
	public List<ExpressionData> Expression = new List<ExpressionData>();

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