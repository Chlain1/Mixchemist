using Godot;
using System;
using System.Diagnostics;

public partial class Player : CharacterBody2D
{
    private const float SPEED = 2.5f;
    private const float SPRINT_SPEED = SPEED * 2;
    private const float ACCELLERATION = 50.0f;
    private float StartRot = 0f;

    public override void _Ready()
    {
		StartRot = Rotation;
    }

    public override void _Process(double delta)
	{
        MovePlayer();
    }

    public override void _Input(InputEvent @event)
    {
	    if (@event.IsActionPressed("w_cast")) Debug.WriteLine("Hello w");
	    else if (@event.IsActionPressed("a_cast")) Debug.WriteLine("Hello a");
	    else if (@event.IsActionPressed("d_cast")) Debug.WriteLine("Hello d");
	    else if (@event.IsActionPressed("s_cast")) Debug.WriteLine("Hello s");
    }

	private void MovePlayer()
	{
        Vector2 velocity = Velocity;
        Vector2 direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
        Vector2 cursorPos = GetLocalMousePosition();

        if (direction != Vector2.Zero)
        {
            if (Input.IsActionPressed("ui_sprint"))
            {
                velocity.X = Mathf.MoveToward(Velocity.X, direction.X * SPRINT_SPEED, ACCELLERATION);
                velocity.Y = Mathf.MoveToward(Velocity.Y, direction.Y * SPRINT_SPEED, ACCELLERATION);
            }
            else
            {
                velocity.X = Mathf.MoveToward(Velocity.X, direction.X * SPEED, ACCELLERATION);
                velocity.Y = Mathf.MoveToward(Velocity.Y, direction.Y * SPEED, ACCELLERATION);
            }
        }
        else
        {
            velocity.X = Mathf.MoveToward(Velocity.X, 0, ACCELLERATION);
            velocity.Y = Mathf.MoveToward(Velocity.Y, 0, ACCELLERATION);
        }

        Velocity = velocity;

        MoveAndCollide(Velocity);

        Rotation += cursorPos.Angle() + StartRot;
    }

    private void TakeDamage(float damageAmount)
    {

    }
}
