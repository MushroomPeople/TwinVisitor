using Godot;
using System;

public class InventoryButton : Button
{
	private InventoryMenu inventory;
	
	public override void _Ready()
	{
		inventory = GetNode<InventoryMenu>("/root/GameControl/InventoryMenu");
	}
	
	private void _on_ItemButton_pressed()
	{
		// Replace with function body.
		inventory.EquipItem(GetParent().Name);
	}
}
