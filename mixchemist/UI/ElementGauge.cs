using Godot;
using System;
using System.Collections.Generic;
using static ClassesAndEnums;

public class ElementGauge : HBoxContainer
{
    private static TextureProgress fireGauge;
    private static TextureProgress waterGauge;
    private static TextureProgress earthGauge;
    private static TextureProgress airGauge;

    private static Label fireNumber;
    private static Label waterNumber;
    private static Label earthNumber;
    private static Label airNumber;

    private List<Element> allowedBasicElements;
    public override void _Ready()
    {
        fireGauge = GetNode<TextureProgress>("Fire/TextureProgress");
        waterGauge = GetNode<TextureProgress>("Water/TextureProgress");
        earthGauge = GetNode<TextureProgress>("Earth/TextureProgress");
        airGauge = GetNode<TextureProgress>("Air/TextureProgress");

        fireNumber = GetNode<Label>("Fire/Number");
        waterNumber = GetNode<Label>("Water/Number");
        earthNumber = GetNode<Label>("Earth/Number");
        airNumber = GetNode<Label>("Air/Number");

        while (GameManager.Instance.AllowedBasicElements.Count != 0) allowedBasicElements = GameManager.Instance.AllowedBasicElements;
    }

    private void HideGauges()
    {
        
    }

    public void UpdateGauge(Element element, int value)
    {
        switch (element)
        {
            case Element.FIRE:
                fireGauge.Value = value;
                fireNumber.Text = value.ToString();
                break;
            case Element.WATER:
                waterGauge.Value = value;
                waterNumber.Text = value.ToString();
                break;
            case Element.EARTH:
                earthGauge.Value = value;
                earthNumber.Text = value.ToString();
                break;
            case Element.AIR:
                airGauge.Value = value;
                airNumber.Text = value.ToString();    
                break;
            default:
                break;
        }
    }
}
