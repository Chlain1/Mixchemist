using Godot;
using System;
using static ClassesAndEnums;

public abstract partial class AbstractEnemy : CharacterBody2D
{
	protected const int DEFAULT_DAMAGE = 10;
	protected const int DEFAULT_HEALTH = 10;
	
	protected DifficultyModifier Difficulty;
	protected double Health;
	protected double Damage;
	protected double MovementSpeed;

	public abstract void Attack();

	public abstract void TargetPlayer();

}

/*public interface IEnemy
{
	
	protected const int DEFAULT_DAMAGE = 10;
	protected const int DEFAULT_HEALTH = 10;
	
	protected DifficultyModifier Difficulty;
	protected double Health;
	protected double Damage;
	protected double MovementSpeed;

	public void Attack();

	public void TargetPlayer();
	
}*/
