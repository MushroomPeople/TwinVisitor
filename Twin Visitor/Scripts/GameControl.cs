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
	
	public override void _Process(float delta)
	{
		//if (playerA.active)
		//{
			//GD.Print("testing");
			//playerB.RotationDegrees = Vector3.Up * Mathf.LerpAngle(playerB.Rotation.y, Mathf.Atan2(playerA.Translation.x - playerB.Translation.x, playerA.Translation.z - playerB.Translation.z), 1f);
		//	float dist = playerB.GlobalTransform.origin.DistanceSquaredTo(playerA.GlobalTransform.origin);
		//	if (dist > 16f)
		//	{
		//		Vector3 direction = playerB.GlobalTransform.origin.DirectionTo(playerA.GlobalTransform.origin);
		//		float angle = playerB.GlobalTransform.basis.GetEuler().AngleTo(direction);
		//		playerB.RotateY(angle);
		//	}
		//}
	}
}
