using Godot;
using System;

public class PlayerInputField : Panel
{
    private static Label label; 
    public override void _Ready()
    {
        label = GetChild<Label>(0);
    }

    public void AddTextToLabel(string text)
    {
        label.Text += text;
    }

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
