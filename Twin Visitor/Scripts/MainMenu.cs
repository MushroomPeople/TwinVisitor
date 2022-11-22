using Godot;
using System;

public class MainMenu : Control
{
	public override void _Ready()
	{
		Visible = true;
	}
	
	
	private void _on_NewGameButton_pressed()
	{
		// Replace with function body.
	}


	private void _on_LoadGameButton_pressed()
	{
		// Replace with function body.
	}


	private void _on_ExitButton_pressed()
	{
		GetTree().Quit();
	}
}
