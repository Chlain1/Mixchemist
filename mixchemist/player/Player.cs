using Godot;
using System;

public partial class Player : CharacterBody2D
{
	private float StartRot = 0f;
	
	public const float Speed = 300.0f;
	public const float SprintSpeed = Speed * 2;
	public const float Acceleration = 50.0f;

    public override void _Ready()
    {
		StartRot = Rotation;
    }

    public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

		Vector2 direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
		if (direction != Vector2.Zero)
		{
			if(Input.IsActionPressed("ui_sprint"))
			{
                velocity.X = Mathf.MoveToward(Velocity.X, direction.X * SprintSpeed, Acceleration);
                velocity.Y = Mathf.MoveToward(Velocity.Y, direction.Y * SprintSpeed, Acceleration);
            }
			else
			{
                velocity.X = Mathf.MoveToward(Velocity.X, direction.X * Speed, Acceleration);
                velocity.Y = Mathf.MoveToward(Velocity.Y, direction.Y * Speed, Acceleration);
            }
        }
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Acceleration);
			velocity.Y = Mathf.MoveToward(Velocity.Y, 0, Acceleration);
		}

		Velocity = velocity;
		MoveAndSlide();
	}
    public override void _Process(double delta)
    {
		Vector2 CursorPos = GetLocalMousePosition();

		Rotation += CursorPos.Angle() + StartRot;
		
    }
}
