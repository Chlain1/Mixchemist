using Godot;
using System;
using System.Diagnostics;
using Range = Godot.Range;

public partial class MainMenu : Control
{
	
	private int music_bus_index = AudioServer.GetBusIndex("Music"); // Index of the Music Audio Bus
	
	/**
	 * Exits the game
	 */
	private void _OnExitButtonPressed()
	{
		GetTree().Quit();
	}

	/**
	 * TODO: Changes scene to the first level (or level select)
	 */
	private void _OnStartButtonPressed()
	{
		GetTree().ChangeScene("res://devTesting/DevScene.tscn");
	}

	/**
	 * TODO: Changes scene to the last unlocked level
	 */
	private void _OnContinueButtonPressed()
	{
		MusicManager.Instance.ChangeStream("res://music/mixchemist_title_octup.mp3");
	}

	/// <summary>
	/// Updates Music Volume by changing the music slider
	/// </summary>
	/// <param name="value">percentage of volume</param>
	private void _OnMusicVolumeSliderValueChanged(float value)
	{
		MusicManager.Instance.SetVolume(10*Mathf.Log(value) - 56);
	}
    
}
