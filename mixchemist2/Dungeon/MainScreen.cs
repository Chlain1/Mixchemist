using Godot;
using System;
using DungeonGenerator.Room;

namespace DungeonGenerator.Scene
{
    public class MainScreen : Node2D
    {
        PackedScene roomScene = (PackedScene)GD.Load("res://Dungeon/Room.tscn");
    
        private int tile_size = 32;
        private int num_rooms = 15;
        private int min_size = 4;
        private int max_size = 10;
    
        public override void _Ready()
        {
            Random random = new Random();
            make_rooms();
        }
    
        public void make_rooms()
        {
            Random random = new Random();
            for (int i=0; i < num_rooms ; i++)
            {
                Vector2 pos = new Vector2(0, 0);
                _Room instance = (_Room)roomScene.Instance();
                if (instance is _Room room)
                {
                    int width = min_size + (int)random.Next() % (max_size - min_size);
                    int height = min_size + (int)random.Next() % (max_size - min_size);
                    Vector2 roomVector = new Vector2(width, height);
                    room.make_room(pos, roomVector);
                    AddChild(room);
                    GD.Print("Room added");
                }
                else
                {
                    GD.Print("The instance is not of type _Room");
                }
            }
        }
    }
}

