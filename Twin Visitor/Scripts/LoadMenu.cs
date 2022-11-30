using Godot;
using System;
using System.Linq;
using System.Text.Json;

public class LoadMenu : Control
{
	private Button file1Button;
	private Button file2Button;
	private Button file3Button;
	private GameControl gc;
	private InventoryMenu inventory;
	
	
	public override void _Ready()
	{
		Visible = false;
		gc = GetNode<GameControl>("/root/GameControl");
		inventory = GetNode<InventoryMenu>("/root/GameControl/InventoryMenu");
		SetButtons();
	}
	
	
	private void _on_File1Button_pressed()
	{
		var gameData = Load.GetData("file1.sav");
		LoadGame(gameData);
		SetButtons();
	}
	

	private void _on_File2Button_pressed()
	{
		var gameData = Load.GetData("file2.sav");
		LoadGame(gameData);
		SetButtons();
	}


	private void _on_File3Button_pressed()
	{
		var gameData = Load.GetData("file3.sav");
		LoadGame(gameData);
		SetButtons();
	}
	
	
	private void _on_ResetButton_pressed()
	{
		var gameData = new GameData();
		Save.WriteData(gameData, "file1.sav");
		Save.WriteData(gameData, "file2.sav");
		Save.WriteData(gameData, "file3.sav");
		SetButtons();
	}
	
	
	private void _on_ReturnButton_pressed()
	{
		GetNode<PauseMenu>("/root/GameControl/PauseMenu").Visible = true;
		Visible = false;
	}
	
	
	public void LoadGame(GameData gameData)
	{
		// set correct scene
		// NOTE: this has to happen before setting player location because player position is
		//       automatically updated to the scene-specified spawn point when scene is loaded
		var scene = GD.Load<PackedScene>(gameData.currentScene);
		var instance = scene.Instance();
		
		GetNode("/root/GameControl/CurrentScene").GetChild(0).QueueFree();
		GetNode<GameControl>("/root/GameControl").currentScene = gameData.currentScene;
		GetNode("/root/GameControl/CurrentScene").AddChild(instance);
		
		// set player A and B transform and ensure correct player and camera is active
		gc.playerA.GlobalTransform = DataTools.ArrayToTransform(gameData.playerATransform);
		gc.playerB.GlobalTransform = DataTools.ArrayToTransform(gameData.playerBTransform);
		gc.playerA.active = gameData.playerAActive;
		gc.playerB.active = gameData.playerBActive;
		gc.playerA.camera.Current = gc.playerA.active;
		gc.playerB.camera.Current = gc.playerB.active;
		gc.playtime = gameData.playtime;
	
		// clear player inventory in case game is loaded in the middle of a play session
		gc.ClearInventory();
		inventory.Clear();
		
		// set player inventory
		for (int i = 0; i < gameData.playerInventory.Length; i++)
		{
			gc.AddItem(gameData.playerInventory[i]);
			inventory.AddItem(gameData.playerInventory[i]);
		}
	}

	
	public string FormatTime(float time)
	{
		int hours = (int)(time / 3600);
		time %= 3600;
		int minutes = (int)(time / 60);
		time %= 60;
		int seconds = (int)time;
		
		string sHours = hours.ToString().PadLeft(2, '0');
		string sMinutes = minutes.ToString().PadLeft(2, '0');
		string sSeconds = seconds.ToString().PadLeft(2, '0');
		
		return $"{sHours}:{sMinutes}:{sSeconds}";
	}
	
	
	private void SetButtons()
	{
		var file1 = Load.GetData("file1.sav");
		var file2 = Load.GetData("file2.sav");
		var file3 = Load.GetData("file3.sav");
		
		file1Button = GetNode<Button>("%File1Button");
		file2Button = GetNode<Button>("%File2Button");
		file3Button = GetNode<Button>("%File3Button");
		
		file1Button.Text = $"File 1 - {file1.currentSceneName} - {FormatTime(file1.playtime)}";
		file2Button.Text = $"File 2 - {file2.currentSceneName} - {FormatTime(file2.playtime)}";
		file3Button.Text = $"File 3 - {file3.currentSceneName} - {FormatTime(file3.playtime)}";
	}
}
