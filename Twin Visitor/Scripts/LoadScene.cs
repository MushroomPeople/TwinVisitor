using Godot;
using System;

public class LoadScene : Area
{
	[Export]
	public string sceneToLoad = "res://LoadSceneTest.tscn";
	[Export]
	public string necessaryItem = "";

	private void _on_Item_body_entered(Node body)
	{
		var doTransition = false;
		
		body.GetNode<CollisionShape>("InteractHitBox").Disabled = true;
		if (necessaryItem != "")
		{
			//if (GetNode<GameControl>("/root/GameControl").inventory.ContainsKey(necessaryItem))
			if (GetNode<GameControl>("/root/GameControl").equippedItem == necessaryItem)
			{
				doTransition = true;
			}
		}
		else
			doTransition = true;
		
		if (doTransition)
		{
			GetNode<GameControl>("/root/GameControl").currentScene = sceneToLoad;
			var scene = GD.Load<PackedScene>(sceneToLoad);
			var instance = scene.Instance();
			GetNode("/root/GameControl/CurrentScene").GetChild(0).QueueFree();
			GetNode("/root/GameControl/CurrentScene").AddChild(instance);
		}
	}
}

