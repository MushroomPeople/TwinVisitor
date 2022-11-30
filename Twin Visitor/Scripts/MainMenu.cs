using Godot;
using System;

public class MainMenu : Control
{
	public override void _Ready()
	{
		Visible = true;
		var scene = GD.Load<PackedScene>("res://Scenes/Stage1-1.tscn");
		var instance = scene.Instance();
		instance.QueueFree();
		scene = GD.Load<PackedScene>("res://Scenes/Hallway.tscn");
		instance = scene.Instance();
		instance.QueueFree();
		scene = GD.Load<PackedScene>("res://Scenes/Apartment.tscn");
		instance = scene.Instance();
		instance.QueueFree();
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
