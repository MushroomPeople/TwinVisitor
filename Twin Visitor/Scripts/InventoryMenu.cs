using Godot;
using System;

public class InventoryMenu : Control
{
	private bool isPaused = false;
	private GameControl gc;
	private Label equippedItemLabel;
	
	public override void _Ready()
	{
		Visible = false;
		gc = GetNode<GameControl>("/root/GameControl");
		equippedItemLabel = GetNode<Label>("/root/GameControl/EquippedItem/Label");
	}
	
	public override void _Input(InputEvent @event)
	{
		if (Input.IsActionJustPressed("Inventory"))
		{
			if (isPaused)
			{
				InvUnpause();
			}
			else{
				InvPause();
				
			}
		}
	}
	
	public void InvPause()
	{
		// pause game
		isPaused = true;
		Visible = true;
		GetTree().Paused = true;
		gc.paused = true;
	
		// show mouse (this also uncaptures mouse)
		Input.MouseMode = Input.MouseModeEnum.Visible;
	}
	
	public void InvUnpause()
	{
		// unpause game
		isPaused = false;
		Visible = false;
		GetTree().Paused = false;
		gc.paused = false;

		// hide and capture mouse
		Input.MouseMode = Input.MouseModeEnum.Hidden;
		Input.MouseMode = Input.MouseModeEnum.Captured;
	}
	
	public void AddItem(string newItem)
	{
		// Find child node with the matching itemName and make it visible
		GetNode<Panel>("CenterContainer/VBoxContainer/GridContainer/" + newItem).Visible = true;
	}
	
	public void EquipItem(string itemName)
	{
		gc.equippedItem = itemName;
		equippedItemLabel.Text = itemName;
	}
	
	public void Clear()
	{
		GetNode<Panel>("CenterContainer/VBoxContainer/GridContainer/Keycard").Visible = false;
	}
}




