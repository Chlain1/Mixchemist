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

        public Walker(Godot.Vector2 startingPosition, Godot.Rect2 new_borders)
        {
            if (new_borders.HasPoint(startingPosition))
            {
                position = startingPosition;
                stepHistory.Add(position);
                borders = new_borders;
            }
        }

        public List<Godot.Vector2> walk(int steps)
        {
            for (int i = 0; i < steps; i++)
            {
                double randomNumber = new Random().NextDouble();
                if (randomNumber <= 0.25 || steps_since_turn >= 4)
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

        public void change_direction()
        {
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

