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
	
	public override void _Ready() 
	{
		camera_pivot = GetNode<Spatial>("CameraPivot");
		camera = GetNode<Camera>("CameraPivot/CameraBoom/Camera");

		Input.MouseMode = Input.MouseModeEnum.Captured;
	}

	public override void _Process(float delta)
	{
		if (Input.IsActionJustPressed("ui_cancel"))
		{
			Input.MouseMode = Input.MouseModeEnum.Visible;
		}
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
		}
	}

	public override void _PhysicsProcess(float delta)
	{
		base._PhysicsProcess(delta);
		handle_movement(delta);
	}

	private async void handle_movement(float delta)
	{
		Vector3 direction = new Vector3(Vector3.Zero);

		if (active)
		{
			if (Input.IsActionPressed("MoveForward"))
				direction -= Transform.basis.z;
			if (Input.IsActionPressed("MoveBack"))
				direction += Transform.basis.z;
			if (Input.IsActionPressed("MoveLeft"))
				direction -= Transform.basis.x;
			if (Input.IsActionPressed("MoveRight"))
				direction += Transform.basis.x;

			direction = direction.Normalized();

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
}
