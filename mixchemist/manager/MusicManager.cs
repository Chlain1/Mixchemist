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
		Instance = this;
		this.Stream = GD.Load<AudioStream>("res://music/mixchemist_title_mp3.mp3");
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
