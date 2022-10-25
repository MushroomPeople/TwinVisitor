using Godot;
using System;


public class LoadScene : Area
{
	[Export]
	string sceneToLoad = "res://load_scene_test.tscn";

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		
	}

	private void _on_Item_body_entered(Node body)
	{
		// Replace with function body.
		GD.Print("Load new scene");
		var scene = GD.Load<PackedScene>(sceneToLoad);
		var instance = scene.Instance();
		GetTree().Root.AddChild(instance);
		GetParent().QueueFree();
		body.GetNode<CollisionShape>("InteractHitBox").Disabled = true;
	}
}

