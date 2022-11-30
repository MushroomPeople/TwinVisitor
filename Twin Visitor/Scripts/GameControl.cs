using Godot;
using System;
using System.Collections.Generic;


public class GameControl : Node
{
	public PlayerA playerA;
	public PlayerB playerB;
	// Dictionary for storing Inventory data
	public Dictionary<string, bool> inventory = new Dictionary<string, bool>();
	public string equippedItem = "";
	public string currentScene = "res://ScenesStage1-1.tscn";
	public string currentSceneName = "";
	public bool interacting = false;
	public float playtime = 0f;
	public bool paused = false;


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		playerA = GetNode<PlayerA>("PlayerA");
		playerB = GetNode<PlayerB>("PlayerB");
	}
	
	
	public override void _Process(float delta)
	{
		if (paused == false)
			playtime += delta;
		//GD.Print(equippedItem);
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


	// Called from DefaultItem.cs when a new item is picked up
	public void AddItem(string itemName)
	{
		inventory.Add(itemName, true);
		//GD.Print(inventory[itemName]);
	}
	
	
	// Called from LoadMenu to ensure clean progress loading
	public void ClearInventory()
	{
		inventory.Clear();
	}
}
