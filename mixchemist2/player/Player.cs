using Godot;
using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

public partial class Player : KinematicBody2D/*, IDamageable*/
{
	private const float SPEED = 2.5f;
	private const float SPRINT_SPEED = SPEED * 2;
	private const float ACCELERATION = 50.0f;
	private const int MAX_HP = 100;
	private const int MIN_HP = 0;
	
	private int currentHp = MAX_HP;
	private float startRot = 0f;
	
	public override void _Ready()
	{
		startRot = Rotation;
	}

	public override void _Process(float delta)
	{
		MovePlayer();
		RotateWithCursor();
	}

    public override void _PhysicsProcess(float delta)
    {
        base._PhysicsProcess(delta);
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
		Rotation += CursorPos.Angle() + startRot;
	}

	public void TakeDamage(int dmgAmount, Vector2 damageVector)
	{
		Vector2 velocity = Vector2.Zero;

        currentHp -= dmgAmount;
		if (currentHp > MIN_HP)
		{
			velocity.x = Mathf.MoveToward(0, damageVector.Normalized().x * 1.5f, ACCELERATION);
			velocity.y = Mathf.MoveToward(0, damageVector.Normalized().y * 1.5f, ACCELERATION);
			//UI.HealthBar.UpdateHealthBar(currentHp);
			MoveAndCollide(velocity);
		}
		else if (currentHp <= MIN_HP)
		{
			currentHp = 0;
			//UI.HealthBar.UpdateHealthBar(currentHp);
			//Gamemanager.ActivateDeathScene
		}
	}
}
