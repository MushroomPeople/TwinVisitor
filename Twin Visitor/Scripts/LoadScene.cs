using Godot;
using System;

public class LoadScene : Area
{
	[Export]
	string sceneToLoad = "res://LoadSceneTest.tscn";

	private void _on_Item_body_entered(Node body)
	{
		body.GetNode<CollisionShape>("InteractHitBox").Disabled = true;
		
		GetNode<GameControl>("/root/GameControl").currentScene = sceneToLoad;
		
		var scene = GD.Load<PackedScene>(sceneToLoad);
		var instance = scene.Instance();
		GetNode("/root/GameControl/CurrentScene").GetChild(0).QueueFree();
		GetNode("/root/GameControl/CurrentScene").AddChild(instance);
	}
}

