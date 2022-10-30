using Godot;
using System;


public class LoadScene : Area
{
	[Export]
	string sceneToLoad = "res://load_scene_test.tscn";

	private void _on_Item_body_entered(Node body)
	{
		var scene = GD.Load<PackedScene>(sceneToLoad);
		var instance = scene.Instance();
		GetTree().Root.AddChild(instance);
		GetParent().QueueFree();
		body.GetNode<CollisionShape>("InteractHitBox").Disabled = true;
	}
}

