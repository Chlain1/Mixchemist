using Godot;
using System;

public class Wall : Node2D
{

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        
    }

    public void _OnWallCollided(Node body)
    {

        if (body.Name == "Spell")
        {
            body.QueueFree();
        }
        
    }
    
}
