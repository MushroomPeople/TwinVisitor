using Godot;
using System;


public class LoadScene : Area
{
	[Export]
	string scene_to_load = "res://load_scene_test.tscn";

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		
	}

	private void _on_Item_body_entered(Node body)
	{
		// Replace with function body.
		GD.Print("Load new scene");
		//var rootNode = GetNode<Spatial>("MyScene").QueueFree();
		var scene = GD.Load<PackedScene>(scene_to_load);
		var instance = scene.Instance();
		//rootNode.AddChild(instance);
		AddChild(instance);
		body.GetNode<CollisionShape>("InteractHitBox").Disabled = true;
	}
}

