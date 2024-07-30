using Godot;
using System;
using System.Drawing;

namespace DungeonGenerator.Room
{
    public class _Room : RigidBody2D
    {
        public Vector2 size;
    
        public void make_room(Vector2 _pos, Vector2 _size)
        {
            Position = _pos;
            size = _size;
            RectangleShape2D s = new RectangleShape2D();
            s.Extents = size;
            CollisionShape2D collisionShape = new CollisionShape2D();
        }
    }
}


