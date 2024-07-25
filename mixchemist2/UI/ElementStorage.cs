using Godot;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using static ClassesAndEnums;

public class ElementStorage : Control
{
    private static Panel panel1;
    private static Panel panel2;
    private static Panel panel3;

    private static Queue<ColorRect> colorRects = new Queue<ColorRect>();
    private static Panel[] panels = new Panel[3];
    

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        panel1 = GetChild<Panel>(0);
        panel2 = GetChild<Panel>(1);
        panel3 = GetChild<Panel>(2);
        panels[0] = panel1;
        panels[1] = panel2;
        panels[2] = panel3;
    }

    public void StoreSpellColor(Element chosenElement)
    {
        ColorRect spellColorRect = new ColorRect();
        spellColorRect.RectMinSize = new Vector2(panel1.RectSize.x, panel1.RectSize.y);
        switch (chosenElement)
        {
            case Element.FIRE:
                spellColorRect.Color = Colors.Red;
                break;
            case Element.EARTH:
                spellColorRect.Color = Colors.Brown;
                break;
            case Element.WATER:
                spellColorRect.Color = Colors.Blue;
                break;
            case Element.AIR:
                spellColorRect.Color = Colors.White;
                break;
        }
        NewElementInLastElementOut(spellColorRect);
    }
    
    private void NewElementInLastElementOut(ColorRect spellColorRect)
    {
        if (colorRects.Count == 3)
        {
            ColorRect oldRect = colorRects.Dequeue();
            oldRect.QueueFree(); // Ensure the old ColorRect is removed from the scene tree
        }
        
        colorRects.Enqueue(spellColorRect);
        RemoveElements();
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
        
        int j = colorRects.Count - 1;
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

    public ColorRect CastFirstElementInStorage()
    {
        ColorRect colorRect = null;
        for (int i = panels.Length-1; i >= 0; i--)
        {
            if (panels[i].GetChildCount() > 0)
            {
                colorRect = colorRects.Dequeue();
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