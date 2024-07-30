using System.Diagnostics;
using Godot;
using static ClassesAndEnums;
using mixchemist2.spell;

public abstract partial class AbstractEnemy : KinematicBody2D
{

	[Export] private Element enemyElement;
	[Export] private double health = 30.0f;
	[Export] private double damage = 3.0f;
	[Export] private float movementSpeed = 2.5f;
	
	// Damage Modifiers
	[Export] private double fireModifier = 1f;
	[Export] private double waterModifier = 1f;
	[Export] private double earthModifier = 1f;
	[Export] private double airModifier = 1f;
	[Export] private double fireWaterModifier = 1f;
	[Export] private double fireEarthModifier = 1f;
	[Export] private double fireAirModifier = 1f;
	[Export] private double waterEarthModifier = 1f;
	[Export] private double waterAirModifier = 1f;
	[Export] private double earthAirModifier = 1f;
	[Export] private double fireWaterEarthModifier = 1f;
	[Export] private double fireWaterAirModifier = 1f;
	[Export] private double fireEarthAirModifier = 1f;
	[Export] private double waterEarthAirModifier = 1f;
	[Export] private double shadowModifier = 1f;
	
	private Area2D spellCollisionArea;
	private KinematicCollision2D colObj;
	private TextureProgress healthBar;
	
    private Area2D playerDetectionArea;
	private bool isPlayerDetected = false;

	private Node2D player = null;
	private int frameCounter = 0;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		playerDetectionArea = GetNode<Area2D>("PlayerDetectionArea");
		healthBar = GetNode<TextureProgress>("HealthBar");

		healthBar.MaxValue = health;
		healthBar.Value = health;
		
		//playerDetectionArea.Connect("body_entered", this, nameof(OnBodyEntered));
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
	/*private void OnBodyEntered(Node body)
	{
		
		if (body is ConcreteSpell spell)
		{
			TakeDamage(spell.Damage, spell.GlobalPosition - GlobalPosition);
		}
		
	}*/

	public void _OnBulletHit(Node body)
	{

		// Dont look at this shitshow
		if (body.Name == "Spell")
		{
			int damage = (int) body.Call("GetDamage");
			Element projectileElement = (Element) body.Call("GetElement");
			TakeDamage(damage, projectileElement, Position - (Vector2) body.Call("GetPosition"));
			body.QueueFree();
		}
		
	}

	/// <summary>
	/// This method is called when the enemy takes damage.
	/// </summary>
	/// <param name="damage">The amount of Damage the Enemy should take</param>
	/// <param name="knockback">The desired direction the enemy should take knockback to</param>
	public void TakeDamage(int damage, Element projectileElement, Vector2 knockback)
	{

		double realDamage = damage;
		switch (projectileElement) 
		{
			case Element.FIRE:
				realDamage = realDamage * fireModifier;
				break;
			case Element.WATER:
				realDamage = realDamage * waterModifier;
				break;
			case Element.EARTH:
				realDamage = realDamage * earthModifier;
				break;
			case Element.AIR:
				realDamage = realDamage * airModifier;
				break;
			case Element.FIRE_WATER:
				realDamage = realDamage * fireWaterModifier;
				break;
			case Element.FIRE_EARTH:
				realDamage = realDamage * fireEarthModifier;
				break;
			case Element.FIRE_AIR:
				realDamage = realDamage * fireAirModifier;
				break;
			case Element.WATER_EARTH:
				realDamage = realDamage * waterEarthModifier;
				break;
			case Element.WATER_AIR:
				realDamage = realDamage * waterAirModifier;
				break;
			case Element.EARTH_AIR:
				realDamage = realDamage * earthAirModifier;
				break;
			case Element.FIRE_WATER_AIR:
				realDamage = realDamage * fireWaterAirModifier;
				break;
			case Element.FIRE_WATER_EARTH:
				realDamage = realDamage * fireWaterEarthModifier;
				break;
			case Element.WATER_EARTH_AIR:
				realDamage = realDamage * waterEarthAirModifier;
				break;
			case Element.FIRE_EARTH_AIR:
				realDamage = realDamage * fireEarthAirModifier;
				break;
			case Element.SHADOW:
				realDamage = realDamage * shadowModifier;
				break;
			default:
				Debug.WriteLine("Element does not exist");
				break;
		}
		
		health -= realDamage;
		
		if (health <= 0)
		{
			healthBar.Value = 0;
			QueueFree();
		}
		else
		{
			
			healthBar.Value = healthBar.Value - realDamage;
			
			// Apply knockback
			// You may want to adjust the multiplier to get the desired knockback effect
			MoveAndCollide(knockback.Normalized() * 10);
		}
	}

}