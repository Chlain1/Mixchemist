using Godot;
using System;

	public partial class ConcreteSpell : RigidBody2D
	{
		// Called when the node enters the scene tree for the first time.
		public override void _Ready()
		{
			Timer timer = GetNode<Timer>("Timer");

			// TODO: nochmal gucken
			//timer.Timeout += () => QueueFree();
		}

		// Called every frame. 'delta' is the elapsed time since the previous frame.
		public override void _Process(float delta)
		{
		}

		private void _OnTimeout()
		{
			QueueFree();
		}

	}
