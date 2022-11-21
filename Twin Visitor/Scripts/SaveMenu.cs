using Godot;
using System;
using System.Linq;
using System.Text.Json;

public class SaveMenu : Control
{
	private Button file1Button;
	private Button file2Button;
	private Button file3Button;
	private GameControl gc;
	public bool isSaving = false;
	
	
	public override void _Ready()
	{
		Visible = false;
		gc = GetNode<GameControl>("/root/GameControl");
		SetButtons();
	}
	
	
	private void _on_File1Button_pressed()
	{
		var gameData = new GameData(DataTools.TransformToArray(gc.playerA.GlobalTransform),
									DataTools.TransformToArray(gc.playerB.GlobalTransform),
									gc.playerA.active,
									gc.playerB.active,
									gc.inventory.Keys.ToArray(),
									gc.currentScene,
									gc.currentSceneName,
									gc.playtime);
		Save.WriteData(gameData, "file1.sav");
		SetButtons();
	}
	

	private void _on_File2Button_pressed()
	{
		var gameData = new GameData(DataTools.TransformToArray(gc.playerA.GlobalTransform),
									DataTools.TransformToArray(gc.playerB.GlobalTransform),
									gc.playerA.active,
									gc.playerB.active,
									gc.inventory.Keys.ToArray(),
									gc.currentScene,
									gc.currentSceneName,
									gc.playtime);
		Save.WriteData(gameData, "file2.sav");
		SetButtons();
	}


	private void _on_File3Button_pressed()
	{
		var gameData = new GameData(DataTools.TransformToArray(gc.playerA.GlobalTransform),
									DataTools.TransformToArray(gc.playerB.GlobalTransform),
									gc.playerA.active,
									gc.playerB.active,
									gc.inventory.Keys.ToArray(),
									gc.currentScene,
									gc.currentSceneName,
									gc.playtime);
		Save.WriteData(gameData, "file3.sav");
		SetButtons();
	}
	
	
	private void _on_ReturnButton_pressed()
	{
		GetNode<PauseMenu>("/root/GameControl/PauseMenu").Visible = true;
		Visible = false;
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
