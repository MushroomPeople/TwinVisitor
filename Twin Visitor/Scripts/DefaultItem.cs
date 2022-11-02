using Godot;
using System;


public class DefaultItem : Area
{
	private void _on_Item_body_entered(Node body)
	{
		// Replace with function body.
		GD.Print("I am the item and something touched me");
		body.GetNode<CollisionShape>("InteractHitBox").Disabled = true;
	}
}
