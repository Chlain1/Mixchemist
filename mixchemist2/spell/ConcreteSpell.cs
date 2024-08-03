using Godot;
using System;
using System.Diagnostics;

namespace mixchemist2.spell
{
	public partial class ConcreteSpell : RigidBody2D
	{

		[Export] public int Damage;
    	public Vector2 Direction {get; set;}
	    public ClassesAndEnums.Element ElementType;
    	
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

	    /// <summary>
	    /// Get the element of the spell
	    /// </summary>
	    /// <returns>The element of the spell</returns>
	    public ClassesAndEnums.Element GetElement()
	    {
		    return ElementType;
	    }

	    /// <summary>
	    /// Set the element of the spell
	    /// </summary>
	    /// <param name="element">The element the spell should have</param>
	    public void SetElement(ClassesAndEnums.Element element)
	    {
		    ElementType = element;
	    }

	    /// <summary>
	    /// Get the damage of the spell
	    /// </summary>
	    /// <returns>The amount of damage the spell does</returns>
	    public int GetDamage()
	    {
		    return Damage;
	    }

	    /// <summary>
	    /// Get the position of the spell
	    /// </summary>
	    /// <returns>The position of the spell</returns>
	    public Vector2 GetPosition()
	    {
		    return Position;
	    }
		
	    /// <summary>
	    /// Event handler for the timeout event that frees the spell
	    /// </summary>
    	private void _OnTimeout()
    	{
    		QueueFree();
    	}
    
    }
}

