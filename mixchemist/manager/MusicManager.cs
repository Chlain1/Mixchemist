using Godot;
using System;

public partial class MusicManager : AudioStreamPlayer
{
	
	public static MusicManager Instance { get; private set; }
	
	/**
	 * Creates Singleton Instance and Starts the MainMenu song
	 */
	public override void _Ready()
	{
		string path = "res://music/default_music.mp3";
		if (GetTree().CurrentScene.Name == "DevScene")
		{
			path = "res://music/mixchemist_title_mp3.mp3";
		}
		else if (GetTree().CurrentScene.Name == "MainMenu")
		{
			path = "res://music/main_menu_theme.mp3";
		}
		
		Instance = this;
        this.Stream =
        	GD.Load<AudioStream>(path);
        this.VolumeDb = -10;
        this.Play();
	}
	
	/**
	 * Changes Music Volume in decibels.
	 * Optimal range seems to be -80db to -10db
	 */
	public void SetVolume(float db)
	{
		this.VolumeDb = db;
	}

	/**
	 * Plays the music file located at path
	 */
	public void ChangeStream(string path)
	{
		this.Stream = GD.Load<AudioStream>(path);
		this.Play();
	}
	
}
