using Godot;
using System;
using System.Diagnostics;

public partial class MainMenu : Control
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void OnExitButtonPressed()
	{
		Debug.WriteLine("Quitting game");
		GetTree().Quit();
	}

	private void OnStartButtonPressed()
	{
		Debug.WriteLine("Start Game");
		GetTree().ChangeSceneToFile("res://devTesting/DevScene.tscn");
	}

	private void OnContinueButtonPressed()
	{
		// TODO: not working :c
		MusicManager.Instance.ChangeStream("res://music/mixchemist_title_octup.mp3");
	}
    
}
