using Godot;
using System;
using System.Collections.Generic; 

public class GameControl : Node
{
	private PlayerA playerA;
	private PlayerB playerB;
	// Dictionary for storing Inventory data
	public Dictionary<string, bool> inventory = new Dictionary<string, bool>();

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		playerA = GetNode<PlayerA>("PlayerA");
		playerB = GetNode<PlayerB>("PlayerB");
	}
	
	public override void _Input(InputEvent @event)
	{
		if (Input.IsActionPressed("SwitchPlayer"))
		{
			var temp = playerB.active;
			playerB.active = playerA.active;
			 playerA.active = temp;
			playerA.camera.Current = playerA.active;
			playerB.camera.Current = playerB.active;
		}
	}
	
	public void _on_pause_button_pressed()
	{
		if (GetTree().Paused == false)
			GetTree().Paused = true;
		else
			GetTree().Paused = false;
	}

	// Called from DefaultItem.cs when a new item is picked up
	public void AddItem(string itemName)
	{
		inventory.Add(itemName, true);
		GD.Print(inventory[itemName]);
	}
}
