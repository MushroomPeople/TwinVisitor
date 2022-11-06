using Godot;
using System;

public class PlayerB : KinematicBody
{
	[Export]
	float speed = 20f;
	[Export]
	float acceleration = 15f;
	[Export]
	float air_acceleration = 5f;
	[Export]
	float gravity = 0.98f;
	[Export]
	float max_terminal_velocity = 54f;
	[Export] 
	float jump_power = 20f;
	[Export(PropertyHint.Range, "0.1,1.0")]
	float mouse_sensitivity = 0.3f;
	[Export(PropertyHint.Range, "-90,0,1")]
	float min_pitch = -90f;
	[Export(PropertyHint.Range, "0,90,1")]
	float max_pitch = 90f;
	public bool active = false;
	public Camera camera;
	private Vector3 velocity;
	private float y_velocity;
	private Spatial camera_pivot;
	public CollisionShape interact_collider;
	private PlayerA playerA;
	
	public override void _Ready() 
	{
		camera_pivot = GetNode<Spatial>("CameraPivot");
		camera = GetNode<Camera>("CameraPivot/CameraBoom/Camera");
		interact_collider = GetNode<CollisionShape>("Interact/InteractHitBox");

		interact_collider.Disabled = true;
		Input.MouseMode = Input.MouseModeEnum.Captured;
		
		playerA = GetNode<PlayerA>("/root/GameControl/PlayerA");
	}

	public override void _Process(float delta)
	{
		if (Input.IsActionJustPressed("ui_cancel"))
		{
			Input.MouseMode = Input.MouseModeEnum.Visible;
		}
		interact_collider.Disabled = true;
	}

	public override void _Input(InputEvent @event)
	{
		if (active)
		{
			base._Input(@event);
			if (@event is InputEventMouseMotion motionEvent)
			{
				Vector3 rotDeg = RotationDegrees;
				rotDeg.y -= motionEvent.Relative.x * mouse_sensitivity;
				RotationDegrees = rotDeg;

				rotDeg = camera_pivot.RotationDegrees;
				rotDeg.x -= motionEvent.Relative.y * mouse_sensitivity;
				rotDeg.x = Mathf.Clamp(rotDeg.x, min_pitch, max_pitch);
				camera_pivot.RotationDegrees = rotDeg;
			}
			// Checks if the interaction button has just been pressed on this frame
			// and then fires off the block code once
			if (Input.IsActionJustPressed("Interact"))
			{
				// Hitbox in front of the player becomes active
				interact_collider.Disabled = false;
			}
		}
	}

	public override void _PhysicsProcess(float delta)
	{
		base._PhysicsProcess(delta);
		Vector3 direction = new Vector3(Vector3.Zero);
		if (active)
			direction = GetDirection(delta);
		else
		{
			LookAt(playerA.GlobalTransform.origin, Vector3.Up);
			float dist = playerA.GlobalTransform.origin.DistanceSquaredTo(GlobalTransform.origin);
			if (dist > 25f)
			{
				direction -= Transform.basis.z;
				direction = direction.Normalized();
			}
		}
		HandleMovement(delta, direction);
	}
	
	private Vector3 GetDirection(float delta)
	{
		Vector3 direction = new Vector3(Vector3.Zero);
		
		if (Input.IsActionPressed("MoveForward"))
			direction -= Transform.basis.z;
		if (Input.IsActionPressed("MoveBack"))
			direction += Transform.basis.z;
		if (Input.IsActionPressed("MoveLeft"))
			direction -= Transform.basis.x;
		if (Input.IsActionPressed("MoveRight"))
			direction += Transform.basis.x;
			
		direction = direction.Normalized();
		return direction;
	}

	private async void HandleMovement(float delta, Vector3 direction)
	{
		float accel = IsOnFloor() ? acceleration : air_acceleration;
		velocity = velocity.LinearInterpolate(direction * speed, accel * delta);

		if (IsOnFloor())
			y_velocity = -0.01f;
		else
			y_velocity = Mathf.Clamp(y_velocity - gravity, -max_terminal_velocity, max_terminal_velocity);
		
		if (Input.IsActionJustPressed("Jump") && IsOnFloor())
			y_velocity = jump_power;

		velocity.y = y_velocity;
		velocity = MoveAndSlide(velocity, Vector3.Up);
	}
}
