using Godot;
using System;

public partial class MusicManager : AudioStreamPlayer
{
	
	public static MusicManager Instance { get; private set; }
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Instance = this;
		this.Stream = GD.Load<AudioStream>("res://music/mixchemist_title_mp3.mp3");
		this.VolumeDb = -10;
		this.Play();
	}

	public void SetVolume(float db)
	{
		this.VolumeDb = db;
	}

	public void ChangeStream(string path)
	{
		this.Stream = GD.Load<AudioStream>(path);
		this.Play();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
