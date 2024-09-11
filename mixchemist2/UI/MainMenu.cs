using Godot;
using System;
using System.Diagnostics;
using Range = Godot.Range;

public partial class MainMenu : Control
{
	
	private int music_bus_index = AudioServer.GetBusIndex("Music"); // Index of the Music Audio Bus

	/**
	 * TODO: Changes scene to the first level (or level select)
	 */
	/// <summary>
	/// Event Handler for when the Start Button is Pressed that changes the scene to the World Scene
	/// </summary>
	private void _OnStartButtonPressed()
	{
		GetTree().ChangeScene("res://Dungeon/World.tscn");
		MusicManager.Instance.ChangeStream("res://music/default_music.mp3");
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
