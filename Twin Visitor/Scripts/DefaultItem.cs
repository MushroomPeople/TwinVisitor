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
		// The Player interaction hitbox gets disabled
		body.GetNode<CollisionShape>("InteractHitBox").Disabled = true;
		// Calls the AddItem script in the GameControl node
		gc.AddItem(GetName());
		// Removes the item from the scene
		QueueFree();
	}
}
