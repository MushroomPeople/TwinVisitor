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
		var scene = GD.Load<PackedScene>("res://Scenes/Game.tscn");
		var instance = scene.Instance();
		
		GetNode("/root").GetChild(0).QueueFree();
		GetNode("/root").AddChild(instance);
	}


	private void _on_LoadGameButton_pressed()
	{
		Visible = false;
		GetNode<LoadFromMainMenu>("../LoadFromMainMenu").Visible = true;
	}


	private void _on_ExitButton_pressed()
	{
		GetTree().Quit();
	}
}
