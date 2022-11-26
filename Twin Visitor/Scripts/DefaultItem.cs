using Godot;
using System;


public class DefaultItem : Area
{
	// Create a GameControl Node to reference GC
	public GameControl gc;
	public InventoryMenu inventory;
	public DialogBox dialogBox;
	private RichTextLabel dialogText;
	private bool given = false;
	
	public override void _Ready()
	{
		// Initializes the GameControl node to the root node
		gc = GetNode<GameControl>("/root/GameControl");
		inventory = GetNode<InventoryMenu>("/root/GameControl/InventoryMenu");
		
		if (gc.inventory.ContainsKey(Name))
			// Removes the item from the scene
			QueueFree();
		
		dialogBox = GetNode<DialogBox>("DialogBox");
		dialogText = GetNode<RichTextLabel>("DialogBox/DialogText");
		dialogBox.dialogue = new string[] {$"You got {Name}!"};
	}
	
	private void _on_Item_body_entered(Node body)
	{
		if (given == false)
		{
			// The Player interaction hitbox gets disabled
			body.GetNode<CollisionShape>("InteractHitBox").Disabled = true;
			// Calls the AddItem script in the GameControl node
			gc.AddItem(Name);
			// Calls the AddItem script in the Inventory node
			inventory.AddItem(Name);
			
			dialogText.Visible = true;
			given = true;
		}
		
		// Handle item description DialogBox
		if (dialogBox.finished == false)
		{
			if (dialogBox.Visible == false)
				dialogBox.Visible = true;
			dialogBox.Call("AdvanceDialogue");
		}
		else
		{
			dialogBox.Visible = false;
			gc.interacting = false;
			// Removes the item from the scene
			QueueFree();
		}
	}
}
