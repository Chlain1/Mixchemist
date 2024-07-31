using Godot;
using static ClassesAndEnums;

public partial class Player : KinematicBody2D
{
    [Export] private PackedScene healthBarScene;
    private const float SPEED = 7.5f;
	private const float SPRINT_SPEED = SPEED * 2;
	private const float ACCELERATION = 50.0f;
	private const int MAX_HP = 100;
	private const int MIN_HP = 0;
	
	private int currentHp = MAX_HP;
	private float startRot = 0f;
	private HealthBar healthBar;
	private PlayerTexture sprite;

	
	public override void _Ready()
	{
        startRot = Rotation;
		healthBar = (HealthBar)healthBarScene.Instance();
		sprite = GetNode<PlayerTexture>("PlayerTexture");
        healthBar.UpdateHealthBar(MAX_HP);
		sprite.SetPlayerTexture(currentHp, MAX_HP);
    }

	public override void _Process(float delta)
	{
		MovePlayer();
		RotateWithCursor();
	}

    private void MovePlayer()
	{
		Vector2 velocity = Vector2.Zero;
        //Vector2 direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
        Vector2 direction = new Vector2(
			Input.GetActionStrength("ui_right") - Input.GetActionStrength("ui_left"), 
			Input.GetActionStrength("ui_down") - Input.GetActionStrength("ui_up"));
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
			velocity.x = Mathf.MoveToward(0, damageVector.Normalized().x * 50.0f, ACCELERATION);
			velocity.y = Mathf.MoveToward(0, damageVector.Normalized().y * 50.0f, ACCELERATION);
			healthBar.UpdateHealthBar(currentHp);
			MoveAndCollide(velocity);
		}
		else if (currentHp <= MIN_HP)
		{
			currentHp = 0;
			healthBar.UpdateHealthBar(currentHp);
			GetTree().ChangeScene("res://UI/DeathMenu.tscn");
		}
		sprite.SetPlayerTexture(currentHp, MAX_HP);
	}
}
