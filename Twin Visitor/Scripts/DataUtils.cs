using Godot;
using System;
using System.Collections.Generic;
using System.Text.Json;


[Serializable]
public class GameData
{
	public float[] playerATransform;
	public float[] playerBTransform;
	public bool playerAActive;
	public bool playerBActive;
	public string[] playerInventory;
	public string currentScene;
	public string currentSceneName;
	public float playtime;
	
	// default constructor for loading
	public GameData()
	{
		playerATransform = new float[12] {1f, 0f, 0f, 0f, 1f, 0f, 0f, 0f, 1f, 4f, 0.5f, 0f};
		playerBTransform = new float[12] {0f, 0f, 1f, 0f, 1f, 0f, -1f, 0f, 0f, 0f, 0.5f, 0f};
		playerAActive = true;
		playerBActive = false;
		playerInventory = new string[0];
		currentScene = "res://Scenes/Stage1-1.tscn";
		currentSceneName = "New Game";
		playtime = 0f;
	}
	
	// normal constructor for saving
	public GameData(float[] transformA,
					float[] transformB,
					bool activeA,
					bool activeB,
					string[] inventory,
					string scene,
					string name,
					float time)
	{
		playerATransform = transformA;
		playerBTransform = transformB;
		playerAActive = activeA;
		playerBActive = activeB;
		playerInventory = inventory;
		currentScene = scene;
		currentSceneName = name;
		playtime = time;
	}
}


public static class Save
{
	public static void WriteData(GameData gameData, string filename = "save.sav")
	{
		// convert GameData to JSON string
		var options = new JsonSerializerOptions {IncludeFields = true, WriteIndented = true};
		string saveData = JsonSerializer.Serialize<GameData>(gameData, options);
		// write JSON string to file
		var saveFile = new File();
		saveFile.Open($"user://{filename}", File.ModeFlags.Write);
		saveFile.StoreString(saveData);
		saveFile.Close();
	}
}


public static class Load
{
	public static GameData GetData(string filename = "save.sav")
	{
		// open save file and read as JSON string
		var saveFile = new File();
		if (saveFile.FileExists($"user://{filename}") == false)
		{
			Save.WriteData(new GameData(), filename);
		}
		saveFile.Open($"user://{filename}", File.ModeFlags.Read);
		//saveFile.Open($"user://{filename}", File.ModeFlags.WriteRead);
		string rawSaveData = saveFile.GetAsText();
		saveFile.Close();
		// convert JSON string to GameData
		if (rawSaveData == "")
			return new GameData();
		else
		{
			var options = new JsonSerializerOptions {IncludeFields = true, WriteIndented = true};
			var gameData = JsonSerializer.Deserialize<GameData>(rawSaveData, options);
			return gameData;
		}
	}
}


public static class DataTools
{
	public static float[] TransformToArray(Transform transform)
	{
		float[] formatted = new float[] {transform.basis.x.x, transform.basis.x.y, transform.basis.x.z,
										 transform.basis.y.x, transform.basis.y.y, transform.basis.y.z,
										 transform.basis.z.x, transform.basis.z.y, transform.basis.z.z,
										 transform.origin.x, transform.origin.y, transform.origin.z};
		return formatted;
	}
	
	public static Transform ArrayToTransform(float[] array)
	{
		// transform(basis, vector3)
		Transform formatted = new Transform(
								new Basis(
								  new Vector3(array[0], array[1], array[2]),
								  new Vector3(array[3], array[4], array[5]),
								  new Vector3(array[6], array[7], array[8])),
								new Vector3(array[9], array[10], array[11]));
		return formatted;
	}
}
