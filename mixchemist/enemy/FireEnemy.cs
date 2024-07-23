using System;
using System.Diagnostics;
using Godot;

namespace mixchemist.enemy;

public partial class FireEnemy : AbstractEnemy
{

    private bool isPlayerDetected = false;
    private const float SPEED = 2.5f;
    private const float Acceleration = 50.0f;

    private Node2D player = null;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
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
            Vector2 velocity = Velocity;
            Vector2 direction = (player.Position - Position).Normalized();

            velocity.X = Mathf.MoveToward(Velocity.X, direction.X * SPEED, Acceleration);
            velocity.Y = Mathf.MoveToward(Velocity.Y, direction.Y * SPEED, Acceleration);

            Velocity = velocity;
            MoveAndCollide(Velocity);
        }
    }

    private void _OnPlayerDetected(Node2D player)
    {
        this.player = player;
        isPlayerDetected = true;
    }

    private void _OnPlayerDetectionLost(Node2D player)
    {
        isPlayerDetected = false;
    }
    
}