using Godot;
using System;
using System.Diagnostics;
using System.Text.Json;
using NJsonSchema;

public class DeathMenu : Control
{

    private static Panel name_panel;
    private static Panel highscore_panel;
    private static Label score_label;
    private static LineEdit name_input_field;
    private static TextEdit highscore_label;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        name_panel = GetNode<Panel>("NamePanel");
        highscore_panel = GetNode<Panel>("HighScorePanel");
        score_label = GetNode<Label>("NamePanel/ScoreLabel");
        name_input_field = GetNode<LineEdit>("NamePanel/NameField");
        highscore_label = GetNode<TextEdit>("HighScorePanel/Scores");

        name_panel.Visible = true;
        highscore_panel.Visible = false;

        score_label.Text = "Score: " + GameManager.Instance.GetScore();
    }

    /// <summary>
    /// Event handler for when the name button is pressed
    /// </summary>
    private void _OnNameButtonPressed()
    {
        Debug.WriteLine(name_input_field.Text);

        if (name_input_field.Text.Length == 3)
        {
            highscore_panel.Visible = true;
            name_panel.Visible = false;

            highscore_label.Text = name_input_field.Text + " - " + GameManager.Instance.GetScore();
            // TODO: Serialize score with name
            //Highscore playerScore = new Highscore(name_input_field.Text, GameManager.Instance.GetScore());
            //Debug.WriteLine(JsonDocument.Parse(playerScore.ToString()));


        }
        
    }

    private void _OnContinueButtonPressed()
    {
        GameManager.Instance.SetScore(0);
        GetTree().ChangeScene("res://UI/MainMenu.tscn");
    }
    
}
