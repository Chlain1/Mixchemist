using Godot;
using System;

public class PauseMenu : Control
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        
    }

    /// <summary>
    /// Event for when the Pause Button is Pressed that pauses the game
    /// </summary>
    private void OnPauseButtonPressed()
    {
        GetTree().Paused = true;
        Show();
    }
    
    /// <summary>
    /// Event for when the Resume Button is Pressed that resumes the game
    /// </summary>
    private void OnCloseButtonPressed()
    {
        Hide();
        GetTree().Paused = false;
    }
    
    /// <summary>
    /// Event for when the Main Menu Button is Pressed that returns us to the Main Menu
    /// </summary>
    private void OnMainMenuButtonPressed() 
    {
        GetTree().ChangeScene("res://UI/MainMenu.tscn");
    }
    
    /// <summary>
    /// Event for when the Quit Button is Pressed that quits the game
    /// </summary>
    private void OnQuitButtonPressed()
    {
        GetTree().Quit();
    }

    public override void _Input(InputEvent @event)
    {
        if(@event.IsActionPressed("ui_pause"))
        {
            OnPauseButtonPressed();
        }
    }
}
