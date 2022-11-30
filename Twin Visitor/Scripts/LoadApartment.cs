using Godot;
using System;

public class LoadApartment : Area
{
	[Export]
	public string sceneToLoad = "res://LoadSceneTest.tscn";
	[Export]
	public string necessaryItem = "";
	private AudioStreamPlayer asp;
	private AudioStreamPlayer asp2;
	
	public override void _Ready()
	{
		asp = GetNode<AudioStreamPlayer>("AudioStreamPlayer");
		asp2 = GetNode<AudioStreamPlayer>("AudioStreamPlayer2");
	}

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
			asp.Play();
			GetNode<GameControl>("/root/GameControl").currentScene = sceneToLoad;
			var scene = GD.Load<PackedScene>(sceneToLoad);
			var instance = scene.Instance();
			GetNode("/root/GameControl/CurrentScene").GetChild(0).QueueFree();
			GetNode("/root/GameControl/CurrentScene").AddChild(instance);
		}
		else
			asp2.Play();
	}
}

