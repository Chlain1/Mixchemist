using Godot;
using System;
using System.Diagnostics;

public partial class Player : KinematicBody2D
{
	public const float SPEED = 2.5f;
	public const float SPRINT_SPEED = SPEED * 2;
	public const float ACCELERATION = 50.0f;

	private float StartRot = 0f;
	
	public override void _Ready()
	{
		StartRot = Rotation;
	}

	public override void _Process(float delta)
	{
		MovePlayer();
		RotateWithCursor();
	}

	private void MovePlayer()
	{
		Vector2 velocity = Vector2.Zero;
		Vector2 direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
		if (direction != Vector2.Zero && !Input.IsActionPressed("ui_cast"))
		{
			if (Input.IsActionPressed("ui_sprint"))
			{
				velocity.x = Mathf.MoveToward(0, direction.x * SPRINT_SPEED, ACCELERATION);
				velocity.y = Mathf.MoveToward(0, direction.y * SPRINT_SPEED, ACCELERATION);
			}
			else
			{
				velocity.x = Mathf.MoveToward(0, direction.x * SPEED, ACCELERATION);
				velocity.y = Mathf.MoveToward(0, direction.y * SPEED, ACCELERATION);
			}
		}
		else
		{
			velocity = Vector2.Zero;
		}

		MoveAndCollide(velocity);
	}

	private void RotateWithCursor()
	{
		Vector2 CursorPos = GetLocalMousePosition();
		Rotation += CursorPos.Angle() + StartRot;
	}
}
