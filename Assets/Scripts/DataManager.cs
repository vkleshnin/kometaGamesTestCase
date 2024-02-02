using System.IO;
using Core;
using UnityEngine;

public class DataManager
{
	[System.Serializable]
	public class SaveData
	{
		public int[][] FieldValues;
		public PlayerType lastMove;
	}
	
	public void Save(int[][] fieldValues, PlayerType _lastMove)
	{
		var saveData = new SaveData
		{
			FieldValues = fieldValues,
			lastMove = _lastMove
		};
		
		string json = Newtonsoft.Json.JsonConvert.SerializeObject(saveData);
		Debug.Log($"FIle save: {json}");
		File.WriteAllText(Application.persistentDataPath + "/save.json", json);
	}

	public SaveData Load()
	{
		string filePath = Application.persistentDataPath + "/save.json";
		if (File.Exists(filePath))
		{
			string json = File.ReadAllText(filePath);
			SaveData saveData = Newtonsoft.Json.JsonConvert.DeserializeObject<SaveData>(json);
			return saveData;
		}
		Debug.LogWarning("Save file not found.");
		return null;
	}

	public void CleanData()
	{
		string filePath = Application.persistentDataPath + "/save.json";
		if (File.Exists(filePath))
		{
			File.Delete(filePath);
		}
		else
		{
			Debug.LogWarning("Save file not found.");
		}
	}
}