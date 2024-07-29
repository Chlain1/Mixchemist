using Godot;
using System;

public class DevScene : Node2D
{
    private int targetFps = 60;
    public override void _Ready()
    {
        Engine.TargetFps = targetFps;
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
