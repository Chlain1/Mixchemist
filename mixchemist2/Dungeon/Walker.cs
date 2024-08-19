using Godot;
using System;
using System.Collections.Generic;
using Vector2 = System.Numerics.Vector2;

namespace Dungeon.Walker
{
	public class Walker : TileMap
	{
		Godot.Vector2[] DIRECTION = new Godot.Vector2[]
		{
			Godot.Vector2.Right,  // Right
			Godot.Vector2.Up,  // Up
			-Godot.Vector2.Right, // Left
			-Godot.Vector2.Up  // Down
		};
		
		private Godot.Vector2 position = Godot.Vector2.Zero;
		private Godot.Vector2 direction = Godot.Vector2.Right;
		Godot.Rect2 borders = new Godot.Rect2();
		List<Godot.Vector2> stepHistory = new List<Godot.Vector2>();
		int steps_since_turn = 0;

		public Walker() 
		{

		}

		/// <summary>
		/// A walker that walks in a 2D space
		/// </summary>
		/// <param name="startingPosition">The starting position of the Walker</param>
		/// <param name="new_borders">The border of the Map</param>
		public Walker(Godot.Vector2 startingPosition, Godot.Rect2 new_borders)
		{
			if (new_borders.HasPoint(startingPosition))
			{
				position = startingPosition;
				stepHistory.Add(position);
				borders = new_borders;
			}
		}

		/// <summary>
		/// A method that makes the walker walk in a 2D space
		/// </summary>
		/// <param name="steps">The amount of steps the walker should take</param>
		/// <returns>stepHistory is a list of Steps the walker has taken</returns>
		public List<Godot.Vector2> walk(int steps)
		{
			Random random = new Random();
			create_room(position);
			for (int i = 0; i < steps; i++)
			{
				if (random.NextDouble() < 0.25 && steps_since_turn >= 10)
				{
					change_direction();
				}

				if (step())
				{
					stepHistory.Add(position);
				}
				else
				{
					change_direction();
				}
			}

			return stepHistory;
		}

		/// <summary>
		/// One step of the walker
		/// </summary>
		/// <returns>A boolean if the step was taken</returns>
		public bool step()
		{
			Godot.Vector2 target_position = position + direction;
			if (borders.HasPoint(target_position))
			{
				steps_since_turn++;
				position = target_position;
				return true;
			}
			else
			{
				return false;
			}
		}

		/// <summary>
		/// Changes the direction the walker walks to 
		/// </summary>
		public void change_direction()
		{
			create_room(position);
			steps_since_turn = 0;
			List<Godot.Vector2> directions = new List<Godot.Vector2>(DIRECTION);
			directions.Remove(direction);
			Shuffle(directions);
			direction = directions[0];
			directions.RemoveAt(0);
			while (!(borders.HasPoint(position + direction)))
			{
				direction = directions[0];
				directions.RemoveAt(0);
			}
		} 
		
		/// <summary>
		/// Creates a room at a given position
		/// </summary>
		/// <param name="position">Position of the room that should be created</param>
		public void create_room(Godot.Vector2 position)
		{
			Random random = new Random();
			Godot.Vector2 size = new Godot.Vector2(random.Next(5, 10), random.Next(5, 10));

			Godot.Vector2 topLeftCorner = (position - size / 2).Ceil();

			for(int y=0; y < size.y; y++)
			{
				for(int x=0; x < size.x; x++)
				{
					var new_step = topLeftCorner + new Godot.Vector2(x, y);
					if (borders.HasPoint(new_step))
					{
						stepHistory.Add(new_step);
					}
				}
			}
		}
		 
		/// <summary>
		/// Helper Method to shuffle a list
		/// </summary>
		/// <param name="list">the list that has to be shuffled</param>
		/// <typeparam name="T"></typeparam>
		public static void Shuffle<T>(List<T> list)
		{
			Random rng = new Random();
			int n = list.Count;
			while (n > 1)
			{
				n--;
				int k = rng.Next(n + 1);
				T value = list[k];
				list[k] = list[n];
				list[n] = value;
			}
		}
		
	}
}

