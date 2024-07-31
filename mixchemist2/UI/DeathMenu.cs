using Godot;
using System;
using System.Diagnostics;

public class DeathMenu : Control
{

    private static Panel name_panel;
    private static Panel highscore_panel;
    private static Label score_label;
    private static LineEdit name_input_field;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        name_panel = GetNode<Panel>("NamePanel");
        highscore_panel = GetNode<Panel>("HighScorePanel");
        score_label = GetNode<Label>("NamePanel/ScoreLabel");
        name_input_field = GetNode<LineEdit>("NamePanel/NameField");

        name_panel.Visible = true;
        highscore_panel.Visible = false;

        score_label.Text = "Score: " + GameManager.Instance.GetScore();
    }

    private void _OnNameButtonPressed()
    {
        Debug.WriteLine(name_input_field.Text);

        if (name_input_field.Text.Length == 3)
        {
            highscore_panel.Visible = true;
            name_panel.Visible = false;
            // TODO: Serialize score with name
        }
        
    }

    private void _OnContinueButtonPressed()
    {
        GetTree().ChangeScene("res://UI/MainMenu.tscn");
    }
    
}
