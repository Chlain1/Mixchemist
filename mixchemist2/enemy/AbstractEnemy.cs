using Godot;
using static ClassesAndEnums;
using mixchemist2.spell;

public abstract partial class AbstractEnemy : KinematicBody2D
{

	[Export] private Element enemyElement;
	[Export] private double health = 30.0f;
	[Export] private double damage = 3.0f;
	[Export] private float movementSpeed = 2.5f;
	
	private Area2D spellCollisionArea;
	private KinematicCollision2D colObj;
	
    private Area2D playerDetectionArea;
	private bool isPlayerDetected = false;

	private Node2D player = null;
	private int frameCounter = 0;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		playerDetectionArea = GetNode<Area2D>("PlayerDetectionArea");
		playerDetectionArea.
			Connect("body_entered", this, nameof(OnBodyEntered));
		switch (enemyElement)
		{
			case Element.FIRE:
			{
				Modulate = Colors.Red;
				break;
			}
			case Element.WATER:
			{
				Modulate = Colors.Blue;
				break;
			}
			case Element.EARTH:
			{
				Modulate = Colors.Brown;
				break;
			}
			case Element.AIR:
			{
				Modulate = Colors.White;
				break;
			}
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(float delta)
	{
		TargetPlayer();
		if (frameCounter % 10 == 0)
		{
			Attack();
		}
		frameCounter++;
		
	}

    public void Attack()
	{
        if (colObj != null && colObj.Collider.HasMethod("TakeDamage"))
        {
            // TODO: change to whatever I just did to test lol xoxo Eric
            colObj.Collider.Call("TakeDamage", damage, player.Position - Position);
            //Debug.WriteLine("Enemy collided with: " + colObj.Collider.HasMethod("GetDamage"));
        }
    }

	public void TargetPlayer()
	{
		if (isPlayerDetected && player != null)
		{
			//Position += (player.Position - Position).Normalized() * SPEED;
			colObj = MoveAndCollide((player.Position - Position).Normalized() * movementSpeed);
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
		health -= damage;
		if (health <= 0)
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