using System;
using System.Diagnostics;
using Godot;
using mixchemist2.spell;

public partial class FireEnemy : AbstractEnemy
{
	private Area2D spellCollisionArea;
	private KinematicCollision2D colObj;


    private Area2D playerDetectionArea;
	private bool isPlayerDetected = false;
	private const float SPEED = 2.5f;

	private Node2D player = null;
	private int i = 0;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		playerDetectionArea = GetNode<Area2D>("PlayerDetectionArea");
		playerDetectionArea.
			Connect("body_entered", this, nameof(OnBodyEntered));
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(float delta)
	{
		TargetPlayer();
		if (i % 10 == 0)
		{
			Attack();
		}
		i++;
		
	}

    public override void Attack()
	{
        if (colObj != null && colObj.Collider.HasMethod("TakeDamage"))
        {
            // TODO: change to whatever I just did to test lol xoxo Eric
            colObj.Collider.Call("TakeDamage", 2, player.Position - Position);
            //Debug.WriteLine("Enemy collided with: " + colObj.Collider.HasMethod("GetDamage"));
        }
    }

	public override void TargetPlayer()
	{
		if (isPlayerDetected && player != null)
		{
			//Position += (player.Position - Position).Normalized() * SPEED;
			colObj = MoveAndCollide((player.Position - Position).Normalized() * SPEED);
		}
	}

	private void _OnPlayerDetected(Node2D player)
	{
		if (player.Name == "Player")
		{
			this.player = player;
			isPlayerDetected = true;
		}
	}

	private void _OnPlayerDetectionLost(Node2D player)
	{
		if(player.Name == "Player") isPlayerDetected = false;
	}
	
	/// <summary>
	/// Detects if the spell has collided with the enemy
	/// </summary>
	/// <param name="body">The body of spell</param>
	private void OnBodyEntered(Node body)
	{
		if (body is ConcreteSpell spell)
		{
			TakeDamage(spell.Damage, spell.GlobalPosition - GlobalPosition);
		}
	}

	/// <summary>
	/// This method is called when the enemy takes damage.
	/// </summary>
	/// <param name="damage">The amount of Damage the Enemy should take</param>
	/// <param name="knockback">The desired direction the enemy should take knockback to</param>
	public void TakeDamage(int damage, Vector2 knockback)
	{
		Health -= damage;
		if (Health <= 0)
		{
			QueueFree();
		}
		else
		{
			// Apply knockback
			// You may want to adjust the multiplier to get the desired knockback effect
			MoveAndCollide(knockback.Normalized() * 10);
		}
	}
	
	
	
}
