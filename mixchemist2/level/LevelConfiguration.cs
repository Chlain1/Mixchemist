using Godot;
using System;
using System.Collections.Generic;
using static ClassesAndEnums;

public class LevelConfiguration : Node2D
{
    [Export] private List<Element> allowedBasicElements = new List<Element>();
    
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        GameManager.Instance.AllowedBasicElements = allowedBasicElements;
    }
}
