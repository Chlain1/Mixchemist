using System;
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
	private EnemyTexture sprite;
	private HealthBarControl healthBarControl;
	
    private Area2D playerDetectionArea;
	private bool isPlayerDetected = false;

	private Node2D player = null;
	private int frameCounter = 0;
    private float startRot = 0f;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
		playerDetectionArea = GetNode<Area2D>("PlayerDetectionArea");
		healthBar = GetNode<TextureProgress>("HealthBarControl/EnemyHealthBar");
		healthBarControl = GetNode<HealthBarControl>("HealthBarControl");
        sprite = GetNode<EnemyTexture>("EnemyTexture");
		healthBar.MaxValue = health;
		healthBar.Value = health;
		Rotation = startRot;
		
		//playerDetectionArea.Connect("body_entered", this, nameof(OnBodyEntered));
		switch (enemyElement)
		{
			case Element.FIRE:
			{
				sprite.SetEnemyTexture(Element.FIRE);
				break;
			}
			case Element.WATER:
			{
				sprite.SetEnemyTexture(Element.WATER);
				break;
			}
			case Element.EARTH:
			{
				sprite.SetEnemyTexture(Element.EARTH);
				break;
			}
			case Element.AIR:
			{
				sprite.SetEnemyTexture(Element.AIR);
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

	/// <summary>
	/// Function for the enemy to damage the player on collision
	/// </summary>
    public void Attack()
	{
        if (colObj != null && colObj.Collider.HasMethod("TakeDamage"))
        {
            // TODO: change to whatever I just did to test lol xoxo Eric
            colObj.Collider.Call("TakeDamage", damage, player.Position - Position);
            //Debug.WriteLine("Enemy collided with: " + colObj.Collider.HasMethod("GetDamage"));
        }
    }

	/// <summary>
	/// Function for the enemy to walk into the direction of the player if it is in range
	/// </summary>
	public void TargetPlayer()
	{
		if (isPlayerDetected && player != null)
		{
			//Position += (player.Position - Position).Normalized() * SPEED;
			colObj = MoveAndCollide((player.Position - Position).Normalized() * movementSpeed);
			LookAt(player.Position);
			Rotate(Mathf.Pi / 2);
			healthBarControl.RectRotation = -(Rotation * (180/Mathf.Pi));
		}
	}

	/// <summary>
	/// Event for when the player is detected
	/// </summary>
	/// <param name="player">The player on the map</param>
	private void _OnPlayerDetected(Node2D player)
	{
		if (player.Name == "Player")
		{
			this.player = player;
			isPlayerDetected = true;
            MusicManager.Instance.ChangeStream("res://music/mixchemist_battle.mp3");
        }
	}

	/// <summary>
	/// Event for when the player is no longer detected
	/// </summary>
	/// <param name="player">The player on the map</param>
	private void _OnPlayerDetectionLost(Node2D player)
	{
		if (player.Name == "Player")
		{
			isPlayerDetected = false;
            MusicManager.Instance.ChangeStream("res://music/default_music.mp3");
        }
	}
	
	/*private void OnBodyEntered(Node body)
	{
		
		if (body is ConcreteSpell spell)
		{
			TakeDamage(spell.Damage, spell.GlobalPosition - GlobalPosition);
		}
		
	}*/

	/// <summary>
	/// Event for when the enemy is hit by a bullet
	/// </summary>
	/// <param name="body">The body that hit the Enemy</param>
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
		double received_score = 100;
		switch (projectileElement) 
		{
			case Element.FIRE:
				received_score = received_score * fireModifier;
				realDamage = realDamage * fireModifier;
				break;
			case Element.WATER:
				received_score = received_score * waterModifier;
				realDamage = realDamage * waterModifier;
				break;
			case Element.EARTH:
				received_score = received_score * earthModifier;
				realDamage = realDamage * earthModifier;
				break;
			case Element.AIR:
				received_score = received_score * airModifier;
				realDamage = realDamage * airModifier;
				break;
			case Element.FIRE_WATER:
				received_score = received_score * fireWaterModifier;
				realDamage = realDamage * fireWaterModifier;
				break;
			case Element.FIRE_EARTH:
				received_score = received_score * fireEarthModifier;
				realDamage = realDamage * fireEarthModifier;
				break;
			case Element.FIRE_AIR:
				received_score = received_score * fireAirModifier;
				realDamage = realDamage * fireAirModifier;
				break;
			case Element.WATER_EARTH:
				received_score = received_score * waterEarthModifier;
				realDamage = realDamage * waterEarthModifier;
				break;
			case Element.WATER_AIR:
				received_score = received_score * waterAirModifier;
				realDamage = realDamage * waterAirModifier;
				break;
			case Element.EARTH_AIR:
				received_score = received_score * earthAirModifier;
				realDamage = realDamage * earthAirModifier;
				break;
			case Element.FIRE_WATER_AIR:
				received_score = received_score * fireWaterAirModifier;
				realDamage = realDamage * fireWaterAirModifier;
				break;
			case Element.FIRE_WATER_EARTH:
				received_score = received_score * fireWaterEarthModifier;
				realDamage = realDamage * fireWaterEarthModifier;
				break;
			case Element.WATER_EARTH_AIR:
				received_score = received_score * waterEarthAirModifier;
				realDamage = realDamage * waterEarthAirModifier;
				break;
			case Element.FIRE_EARTH_AIR:
				received_score = received_score * fireEarthAirModifier;
				realDamage = realDamage * fireEarthAirModifier;
				break;
			case Element.SHADOW:
				received_score = received_score * shadowModifier;
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
			received_score = received_score * 5;
			GameManager.Instance.SetScore(GameManager.Instance.GetScore() + (int) received_score);
			QueueFree();
		}
		else
		{
			
			healthBar.Value = healthBar.Value - realDamage;
			GameManager.Instance.SetScore(GameManager.Instance.GetScore() + (int) received_score);
			
			// Apply knockback
			// You may want to adjust the multiplier to get the desired knockback effect
			MoveAndCollide(knockback.Normalized() * 10);
		}
	}
	
	/// <summary>
	/// Setter for the element of the enemy
	/// </summary>
	/// <param name="element">The element the Enemy should have</param>
	public void SetElement(Element element)
	{ 
		enemyElement = element;
	}

}