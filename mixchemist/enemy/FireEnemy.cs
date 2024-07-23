using System;
using System.Diagnostics;
using Godot;

namespace mixchemist.enemy;

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
            Position += (player.Position - Position).Normalized() * SPEED;
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