using Godot;
using System;

namespace mixchemist2.spell
{
	public partial class ConcreteSpell : RigidBody2D
	{

		[Export] public int Damage;
    	public Vector2 Direction {get; set;}
    	public ClassesAndEnums.Element ElementType {get; set;}
    	
    	// Called when the node enters the scene tree for the first time.
    	public override void _Ready()
	    {
		    Damage = 10;
    		Timer timer = GetNode<Timer>("Timer");
		    
    		// TODO: nochmal gucken
    		//timer.Timeout += () => QueueFree();
    	}
    
    	// Called every frame. 'delta' is the elapsed time since the previous frame.
    	public override void _Process(float delta)
    	{
    	}

	    public int GetDamage()
	    {
		    return Damage;
	    }

	    public Vector2 GetPosition()
	    {
		    return Position;
	    }
		
    	private void _OnTimeout()
    	{
    		QueueFree();
    	}
    
    }
}

