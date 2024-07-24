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

    private Queue<ColorRect> colorRects = new Queue<ColorRect>();
    private Panel[] panels = new Panel[3];

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        panel1 = GetChild<Panel>(0);
        panel2 = GetChild<Panel>(1);
        panel3 = GetChild<Panel>(2);
        // Debugging to ensure panels are assigned correctly
    }

    public void StoreSpellColor(Element chosenElement)
    {
        panels[0] = panel1;
        panels[1] = panel2;
        panels[2] = panel3;
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

        // Remove last element from queue if storage already full
        if (colorRects.Count == 3)
        {
            ColorRect oldRect = colorRects.Dequeue();
            oldRect.QueueFree(); // Ensure the old ColorRect is removed from the scene tree
        }

        // Add new element to storage
        colorRects.Enqueue(spellColorRect);

        for (int i = 0; i < panels.Length; i++) {
            if (panels[i].GetChildCount() > 0) panels[i].RemoveChild(panels[i].GetChild(0));
        }
        
        int j = colorRects.Count-1;
        foreach (var colorRect in colorRects)
        {
            panels[j].AddChild(colorRect);
            j--;
        }
    }
}