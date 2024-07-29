using Godot;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using static ClassesAndEnums;

public class LevelConfiguration : Node2D
{
    [Export] private List<Element> allowedBasicElements = new List<Element>();

    private int target_fps = 60;
    
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        Debug.WriteLine(this.Name);
        Engine.TargetFps = target_fps;
        
        // P.S: Ich weiß das ist extrem dämlich, aber es funktioniert xoxo Eric
        switch (this.Name)
        {
            case "Level1":
                allowedBasicElements.Add(Element.FIRE);
                allowedBasicElements.Add(Element.WATER);
                allowedBasicElements.Add(Element.EARTH);
                allowedBasicElements.Add(Element.AIR);
                break;
            default:
                Debug.WriteLine("ALTER FALTER, DU HAST EIN LEVEL AUFGERUFEN DAS ES GAR NICHT GEBEN TUT!");
                break;
        }
        
        GameManager.Instance.AllowedBasicElements = allowedBasicElements;
    }
}
