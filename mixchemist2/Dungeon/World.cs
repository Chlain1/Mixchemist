using Godot;
using System;
using System.Collections.Generic;
using Dungeon.Walker;
using Vector2 = System.Numerics.Vector2;

namespace Dungeon.Generator 
{
	public class World : Node2D
	{
		private TileMap tileMap;
		private Rect2 borders = new Rect2(-4096, -4096, 8192, 8192);
		private bool doneStatus = false;
		
		/// <summary>
		/// Shows a loading screen if the map is currently loading
		/// </summary>
		public void loadScreen(){
			if (!doneStatus)
			{
				//TODO Show "Loading..."
			}
			else
			{
				//TODO dont show it
			}
		}

		public override void _Ready()
		{
			tileMap = GetNode<TileMap>("TileMap");
			generate_level();
		}

		/// <summary>
		/// Lets the walker generate a level
		/// </summary>
		public void generate_level()
		{
			loadScreen();
			Walker.Walker walker = new Walker.Walker(new Godot.Vector2(0, 0), borders);
			List<Godot.Vector2> map = walker.walk(200);
			walker.QueueFree();
			
			HashSet<Godot.Vector2> mapSet = new HashSet<Godot.Vector2>(map);

			for (int x = (int)borders.Position.x; x < (int)borders.End.x; x++)
			{
				for (int y = (int)borders.Position.y; y < (int)borders.End.y; y++)
				{
					Godot.Vector2 location = new Godot.Vector2(x, y);
					if (!mapSet.Contains(location))
					{
						tileMap.SetCellv(location, -1);
					}
				}
			}
			tileMap.UpdateBitmaskRegion(borders.Position, borders.End);
			doneStatus = true;
			loadScreen();
		}
		
		/// <summary>
		/// Getters for the status of the generation
		/// </summary>
		/// <returns>boolean if the generation is done</returns>
		public bool GetStatus()
		{
			return doneStatus; 
		}
	}
}

