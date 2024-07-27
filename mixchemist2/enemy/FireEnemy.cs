using System;
using System.Diagnostics;
using Godot;

public partial class FireEnemy : AbstractEnemy
{

	private bool isPlayerDetected = false;
	private const float SPEED = 2.5f;

	private Node2D player = null;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(float delta)
	{
		TargetPlayer();
	}

	public override void Attack()
	{
		
	}

	public override void TargetPlayer()
	{
		if (isPlayerDetected && player != null)
		{
			//Position += (player.Position - Position).Normalized() * SPEED;
			KinematicCollision2D colObj = MoveAndCollide((player.Position - Position).Normalized() * SPEED);

			if (colObj != null && colObj.Collider.HasMethod("TakeDamage"))
			{
				// TODO: change to whatever I just did to test lol xoxo Eric
				colObj.Collider.Call("TakeDamage", 5, player.Position - Position);
				//Debug.WriteLine("Enemy collided with: " + colObj.Collider.HasMethod("GetDamage"));
			}
			
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
	
}
