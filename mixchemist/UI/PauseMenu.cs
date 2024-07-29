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

    private void OnPauseButtonPressed()
    {
        GetTree().Paused = true;
        Show();
    }
    private void OnCloseButtonPressed()
    {
        Hide();
        GetTree().Paused = false;
    }
    private void OnMainMenuButtonPressed() 
    {
        GetTree().ChangeScene("res://UI/MainMenu.tscn");
    }
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
