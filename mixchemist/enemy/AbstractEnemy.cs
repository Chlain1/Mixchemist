using Godot;
using System;
using static ClassesAndEnums;

public abstract partial class AbstractEnemy : Node
{
	protected const int DEFAULT_DAMAGE = 10;
	protected const int DEFAULT_HEALTH = 10;
	
	public DifficultyModifier Difficulty;
	public double Health;
	public double Damage;
	public double MovementSpeed;

	public abstract void Attack();

	public void TargetPlayer()
	{
		
	}

}
