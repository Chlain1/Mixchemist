using Godot;
using System;
using System.Collections.Generic;

public class ElementGauge : HBoxContainer
{
    private List<ClassesAndEnums.Element> allowedBasicElements;
    public override void _Ready()
    {
        while (GameManager.Instance.AllowedBasicElements.Count != 0) allowedBasicElements = GameManager.Instance.AllowedBasicElements;
    }

    private void HideGauges()
    {
        
    }
}
