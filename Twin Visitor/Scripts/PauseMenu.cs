using Godot;
using System;

public class PauseMenu : Control
{
	private bool isPaused = false;
	
	public override void _Ready()
	{
		Visible = false;
	}

	public override void _Input(InputEvent @event)
	{
		if (Input.IsActionPressed("Pause"))
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
