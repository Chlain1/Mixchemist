using Godot;
using System;

public class PlayerInputField : Panel
{
    private static Label label; 
    public override void _Ready()
    {
        label = GetChild<Label>(0);
    }

    /// <summary>
    /// Adds text to the label
    /// </summary>
    /// <param name="text">The text that should be added to the label</param>
    public void AddTextToLabel(string text)
    {
        label.Text += text;
    }

    /// <summary>
    /// Resets the Label Text
    /// </summary>
    public void ResetLabelText()
    {
        label.Text = "";
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
