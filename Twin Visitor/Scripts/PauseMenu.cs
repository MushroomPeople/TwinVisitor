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
			{
				Unpause();
			}
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
		Visible = false;
		GetNode<SaveMenu>("%SaveMenu").Visible = true;
		
		//var gameData = new GameData(DataTools.TransformToArray(gc.playerA.GlobalTransform),
		//							DataTools.TransformToArray(gc.playerB.GlobalTransform),
		//							gc.playerA.active,
		//							gc.playerB.active,
		//							gc.inventory.Keys.ToArray(),
		//							gc.currentScene,
		//							gc.currentSceneName,
		//							gc.playtime);
		//Save.WriteData(gameData);
	}

	
	private void _on_LoadButton_pressed()
	{
		Visible = false;
		GetNode<LoadMenu>("%LoadMenu").Visible = true;
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
		GetNode<GameControl>("/root/GameControl").paused = true;
	
		// show mouse (this also uncaptures mouse)
		Input.MouseMode = Input.MouseModeEnum.Visible;
	}

	
	public void Unpause()
	{
		if (GetNode<SaveMenu>("/root/GameControl/SaveMenu").Visible == false && GetNode<LoadMenu>("/root/GameControl/LoadMenu").Visible == false)
		{
			// unpause game
			isPaused = false;
			Visible = false;
			GetTree().Paused = false;
			GetNode<GameControl>("/root/GameControl").paused = false;

			// hide and capture mouse
			Input.MouseMode = Input.MouseModeEnum.Hidden;
			Input.MouseMode = Input.MouseModeEnum.Captured;
		}
	}
}
