using Godot;
using System;

public class GameControl : Node
{
	private PlayerA playerA;
	private PlayerB playerB;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		playerA = GetNode<PlayerA>("PlayerA");
		playerB = GetNode<PlayerB>("PlayerB");
	}
	
	public override void _Input(InputEvent @event)
	{
		if (Input.IsActionPressed("SwitchPlayer"))
		{
			var temp = playerB.active;
			playerB.active = playerA.active;
			playerA.active = temp;
			playerA.camera.Current = playerA.active;
			playerB.camera.Current = playerB.active;
		}
	}
}
