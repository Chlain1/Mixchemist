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
            Walker.Walker walker = new Walker.Walker(new Godot.Vector2(0, 0), borders);
            List<Godot.Vector2> map = walker.walk(500);
            walker.QueueFree();
            foreach (Godot.Vector2 location in map)
            {
                tileMap.SetCellv(location, -1);
            }
            tileMap.UpdateBitmaskRegion(borders.Position, borders.End);
            doneStatus = true;
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

