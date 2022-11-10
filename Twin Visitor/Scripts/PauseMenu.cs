using Godot;
using System;
using System.Linq;
using System.Text.Json;

public class PauseMenu : Control
{
	private bool isPaused = false;
	private GameControl gc;
	
	public override void _Ready()
	{
		Visible = false;
		gc = GetNode<GameControl>("/root/GameControl");
	}

	public override void _Input(InputEvent @event)
	{
		if (Input.IsActionJustPressed("Pause"))
		{
			if (isPaused)
				Unpause();
			else
				Pause();
		}
	}
	
	public void _on_ResumeButton_pressed()
	{
		Unpause();
	}
	
	public void _on_SaveButton_pressed()
	{
		var gameData = new GameData(DataTools.TransformToArray(gc.playerA.GlobalTransform),
									DataTools.TransformToArray(gc.playerB.GlobalTransform),
									gc.playerA.active,
									gc.playerB.active,
									gc.inventory.Keys.ToArray(),
									gc.currentScene,
									gc.currentSceneName);
		Save.WriteData(gameData);
	}
	
	private void _on_LoadButton_pressed()
	{
		var gameData = Load.GetData();
		
		// set player A and B transform and ensure correct player and camera is active
		gc.playerA.GlobalTransform = DataTools.ArrayToTransform(gameData.playerATransform);
		gc.playerB.GlobalTransform = DataTools.ArrayToTransform(gameData.playerBTransform);
		gc.playerA.active = gameData.playerAActive;
		gc.playerB.active = gameData.playerBActive;
		gc.playerA.camera.Current = gc.playerA.active;
		gc.playerB.camera.Current = gc.playerB.active;
		
		// set player inventory
		for (int i = 0; i < gameData.playerInventory.Length; i++)
		{
			gc.AddItem(gameData.playerInventory[i]);
		}
		
		// set correct scene
		var scene = GD.Load<PackedScene>(gameData.currentScene);
		var instance = scene.Instance();
		
		GetNode("/root/GameControl/CurrentScene").GetChild(0).QueueFree();
		GetNode<GameControl>("/root/GameControl").currentScene = gameData.currentScene;
		GetNode("/root/GameControl/CurrentScene").AddChild(instance);
	}
	
	private void _on_QuitButton_pressed()
	{
		GetTree().Quit();
	}
	
	public void Pause()
	{
		// pause game
		isPaused = true;
		Visible = true;
		GetTree().Paused = true;
	
		// show mouse (this also uncaptures mouse)
		Input.MouseMode = Input.MouseModeEnum.Visible;
	}
	
	public void Unpause()
	{
		// unpause game
		isPaused = false;
		Visible = false;
		GetTree().Paused = false;

		// hide and capture mouse
		Input.MouseMode = Input.MouseModeEnum.Hidden;
		Input.MouseMode = Input.MouseModeEnum.Captured;
	}
}
