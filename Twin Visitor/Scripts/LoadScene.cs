using Godot;
using System;


public class LoadScene : Area
{
	[Export]
	string sceneToLoad = "res://load_scene_test.tscn";

	public override void _Ready()
	{
		
	}

	private void _on_Item_body_entered(Node body)
	{
		//GD.Print("Load new scene");
		var scene = GD.Load<PackedScene>(sceneToLoad);
		var instance = scene.Instance();
		GetTree().Root.AddChild(instance);
		GetParent().QueueFree();
		body.GetNode<CollisionShape>("InteractHitBox").Disabled = true;
	}
}

