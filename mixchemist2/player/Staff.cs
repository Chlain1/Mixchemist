using System.Diagnostics;
using Godot;

public partial class Staff : Node2D
{
	[Export] private PackedScene bulletScene;
	[Export] float bulletSpeed = 1500f;
	[Export] private float bps = 5;
	[Export] float damage = 10;

	private float fireRate;
	private float timeUntilFire = 0f;
	private Color spellColor = Colors.Black;
	private bool spellReadyToCast = false; // Flag to indicate a spell is ready to cast

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		fireRate = 1 / bps;
	}

	public override void _Input(InputEvent @event)
	{
		
		if (@event.IsActionPressed("w_cast"))
		{
			spellColor = Colors.Red;
			spellReadyToCast = true;
		}
		else if (@event.IsActionPressed("a_cast"))
		{
			spellColor = Colors.Brown;
			spellReadyToCast = true;
		}
		else if (@event.IsActionPressed("d_cast"))
		{
			spellColor = Colors.Blue;
			spellReadyToCast = true;
		}
		else if (@event.IsActionPressed("s_cast"))
		{
			spellColor = Colors.White;
			spellReadyToCast = true;
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(float delta)
	{
		if (timeUntilFire <= fireRate)
		{
			timeUntilFire += delta;
		}

		if (spellReadyToCast && timeUntilFire > fireRate)
		{
			CastSpell(spellColor);
			timeUntilFire = 0;
			spellReadyToCast = false; // Reset the flag
		}
	}

	private void CastSpell(Color bulletColor)
	{
		RigidBody2D spell = bulletScene.Instance<RigidBody2D>();
		spell.Modulate = bulletColor;
		spell.Rotation = GlobalRotation; //current staff's rotation
		spell.GlobalPosition = GlobalPosition; //current staff's position
		spell.LinearVelocity = spell.Transform.x * bulletSpeed;
		GetTree().Root.AddChild(spell);
	}
}
