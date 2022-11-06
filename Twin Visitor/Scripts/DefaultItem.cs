using Godot;
using System;


public class DefaultItem : Area
{
	// Creates a GameControl Node
	public GameControl gc;
	public override void _Ready()
	{
		// Initializes the GameControl node to the root node
		gc = GetNode<GameControl>("/root/GameControl");
	}
	private void _on_Item_body_entered(Node body)
	{
		// Replace with function body.
		GD.Print("I am the item and something touched me");
		body.GetNode<CollisionShape>("InteractHitBox").Disabled = true;
		// Calls the AddItem script on the GameControl node
		gc.AddItem(GetName());
	}
}
