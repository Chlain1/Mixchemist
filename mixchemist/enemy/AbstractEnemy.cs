using Godot;
using System;
using static ClassesAndEnums;

public abstract partial class AbstractEnemy : Node
{
	protected const int DEFAULT_DAMAGE = 10;
	protected const int DEFAULT_HEALTH = 10;
	
	protected DifficultyModifier Difficulty;
	protected double Health;
	protected double Damage;
	protected double MovementSpeed;

	public abstract void Attack();

	public void TargetPlayer()
	{
		
	}

}
