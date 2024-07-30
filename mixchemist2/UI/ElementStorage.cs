using Godot;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using static ClassesAndEnums;

public class ElementStorage : Control
{
    private static Panel panel1;
    private static Panel panel2;
    private static Panel panel3;

    private static ColorRect firePanel;
    private static ColorRect waterPanel;
    private static ColorRect earthPanel;
    private static ColorRect airPanel;

    private static Queue<ColorRect> colorRects = new Queue<ColorRect>();
    private static Panel[] panels = new Panel[3];
    private static Queue<Element> elemQueue = new Queue<Element>();
    

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        
        base._Ready();
        
        panel1 = GetNode<Panel>("Queue/Element1");
        panel2 = GetNode<Panel>("Queue/Element2");
        panel3 = GetNode<Panel>("Queue/Element3");
        panels[0] = panel1;
        panels[1] = panel2;
        panels[2] = panel3;

        firePanel = GetNode<ColorRect>("Input/FireHolding");
        waterPanel = GetNode<ColorRect>("Input/WaterHolding");
        earthPanel = GetNode<ColorRect>("Input/EarthHolding");
        airPanel = GetNode<ColorRect>("Input/AirHolding");

    }

    public void ToggleElementPanelColor(ClassesAndEnums.Element element, bool setColor) 
    {

        switch (element)
        {
            case Element.FIRE:
                if (setColor)
                {
                    firePanel.Color = Colors.Red;
                }
                else
                {
                    firePanel.Color = Colors.Black;
                }
                break;
            case Element.WATER:
                if (setColor)
                {
                    waterPanel.Color = Colors.Blue;
                }
                else
                {
                    waterPanel.Color = Colors.Black;
                }
                break;
            case Element.EARTH:
                if (setColor)
                {
                    earthPanel.Color = Colors.SandyBrown;
                }
                else
                {
                    earthPanel.Color = Colors.Black;
                }
                break;
            case Element.AIR:
                if (setColor)
                {
                    airPanel.Color = Colors.White;
                }
                else
                {
                    airPanel.Color = Colors.Black;
                }
                break;
            default:
                Debug.WriteLine("Nee mann falsches element");
                break;

        }

    }

    public void StoreSpellColor(Element chosenElement)
    {
        ColorRect spellColorRect = new ColorRect();
        spellColorRect.RectMinSize = new Vector2(panel1.RectSize.x, panel1.RectSize.y);
        switch (chosenElement)
        {
            // BTW: Nicht fragen was diese Farben sollen. Ich war extrem unter Drogen xoxo Eric
            case Element.FIRE:
                spellColorRect.Color = Colors.Red;
                break;
            case Element.EARTH:
                spellColorRect.Color = Colors.SandyBrown;
                break;
            case Element.WATER:
                spellColorRect.Color = Colors.Blue;
                break;
            case Element.AIR:
                spellColorRect.Color = Colors.White;
                break;
            case Element.FIRE_AIR:
                spellColorRect.Color = Colors.Aquamarine;
                break;
            case Element.FIRE_WATER:
                spellColorRect.Color = Colors.Azure;
                break;
            case Element.FIRE_EARTH:
                spellColorRect.Color = Colors.RosyBrown;
                break;
            case Element.WATER_AIR:
                spellColorRect.Color = Colors.Cyan;
                break;
            case Element.WATER_EARTH:
                spellColorRect.Color = Colors.Cornflower;
                break;
            case Element.EARTH_AIR:
                spellColorRect.Color = Colors.Gainsboro;
                break;
            case Element.FIRE_EARTH_AIR:
                spellColorRect.Color = Colors.Firebrick;
                break;
            case Element.FIRE_WATER_AIR:
                spellColorRect.Color = Colors.Olive;
                break;
            case Element.FIRE_WATER_EARTH:
                spellColorRect.Color = Colors.Lavender;
                break;
            case Element.WATER_EARTH_AIR:
                spellColorRect.Color = Colors.Chocolate;
                break;
            case Element.SHADOW:
                spellColorRect.Color = Colors.Black;
                break;
        }
        NewElementInLastElementOut(spellColorRect, chosenElement);
    }
    
    private void NewElementInLastElementOut(ColorRect spellColorRect, Element element)
    {
        if (colorRects.Count == 3)
        {
            ColorRect oldRect = colorRects.Dequeue();
            oldRect.QueueFree(); // Ensure the old ColorRect is removed from the scene tree
            Element oldElem = elemQueue.Dequeue();
        }
        
        colorRects.Enqueue(spellColorRect);
        elemQueue.Enqueue(element);
        List<ColorRect> tempList = new List<ColorRect>(colorRects);
        RemoveElements();
        int j = panels.Length - 1;
        foreach (var tempColor in tempList)
        {
            panels[j].AddChild(tempColor);
            j--;
        }
    }
    
    private void ShiftElements()
    {
        if (colorRects.Count == 0) return;
        
        RemoveElements();
        ColorRect lastElement = colorRects.Dequeue();
        colorRects.Enqueue(lastElement);
        Element lastElem = elemQueue.Dequeue();
        elemQueue.Enqueue(lastElem);
        
        int j = panels.Length - 1;
        foreach (var colorRect in colorRects)
        {
            panels[j].AddChild(colorRect);
            j--;
        }
    }
    public override void _Input(InputEvent @event)
    {
        if (@event.IsActionPressed("rotateElements"))
        {
            ShiftElements();
        }
    }
    
    private void RemoveElements()
    {
        for (int i = 0; i < panels.Length; i++) {
            if (panels[i].GetChildCount() > 0) panels[i].RemoveChild(panels[i].GetChild(0));
        }
    }

    public Element GetFirstRealmElement()
    {
        if (elemQueue.Count() > 0)
        {
            return elemQueue.Peek();
        }

        return Element.FIRE;
    }
    public ColorRect CastFirstElementInStorage()
    {
        ColorRect colorRect = null;
        for (int i = panels.Length-1; i >= 0; i--)
        {
            if (panels[i].GetChildCount() > 0)
            {
                colorRect = colorRects.Dequeue();
                elemQueue.Dequeue();
                panels[i].RemoveChild(colorRect);
                colorRect.QueueFree();
                List<ColorRect> tempList = new List<ColorRect>(colorRects);
                RemoveElements();
                int j = panels.Length - 1;
                foreach (var tempColor in tempList)
                {
                    panels[j].AddChild(tempColor);
                    j--;
                }
                break;
            }
        }
        return colorRect;
    }
}