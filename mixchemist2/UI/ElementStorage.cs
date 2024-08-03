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
    private static GridContainer gridCont;
    
    private static Queue<ColorRect> colorRects = new Queue<ColorRect>();
    private static Panel[] panels = new Panel[3];
    private static Queue<Element> elemQueue = new Queue<Element>();

    private Vector2 gridPos = new Vector2(20, -10);
    

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {

        base._Ready();

        gridCont = GetNode<GridContainer>("Input");
        gridCont.RectRotation = 45f;
        gridCont.SetPosition(gridCont.RectPosition + gridPos);
        
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

    /// <summary>
    /// Sets the colours of the element panels to the default colour
    /// </summary>
    /// <param name="element">The element that needs to be set</param>
    /// <param name="setColor">Bool if the color should be set</param>
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

    /// <summary>
    /// The function to store the color of the spell in the queue
    /// </summary>
    /// <param name="chosenElement">Element that should be set</param>
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
    
    /// <summary>
    /// Adds a new element to the queue and removes the last element
    /// </summary>
    /// <param name="spellColorRect">Color of the new element</param>
    /// <param name="element">The new elemnt that should be inserted</param>
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
    
    /// <summary>
    /// Function to shift the elements in the queue
    /// </summary>
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
    
    /// <summary>
    /// Event handler for when the rotate button is pressed
    /// </summary>
    /// <param name="event">The event that needs to be handled</param>
    public override void _Input(InputEvent @event)
    {
        if (@event.IsActionPressed("rotateElements"))
        {
            ShiftElements();
        }
    }
    
    /// <summary>
    /// Removes the elements from the queue
    /// </summary>
    private void RemoveElements()
    {
        for (int i = 0; i < panels.Length; i++) {
            if (panels[i].GetChildCount() > 0) panels[i].RemoveChild(panels[i].GetChild(0));
        }
    }

    /// <summary>
    /// Gets the first element in the queue
    /// </summary>
    /// <returns>Returns the first element of the queue</returns>
    public Element GetFirstRealmElement()
    {
        if (elemQueue.Count() > 0)
        {
            return elemQueue.Peek();
        }

        return Element.FIRE;
    }
    
    /// <summary>
    /// Return to cast the first element in the queue
    /// </summary>
    /// <returns>ColorRext of the Element that got cast</returns>
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

    public override void _Process(float delta)
    {
        gridCont.RectRotation = 45f;
    }
}