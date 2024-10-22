using Godot;
using System.Numerics;

public partial class ClassesAndEnums
{
    public enum Element
    {
        FIRE,
        WATER,
        EARTH,
        AIR,

        FIRE_WATER,
        FIRE_EARTH,
        FIRE_AIR,
        WATER_EARTH,
        WATER_AIR,
        EARTH_AIR,

        FIRE_WATER_EARTH,
        FIRE_EARTH_AIR,
        WATER_EARTH_AIR,
        FIRE_WATER_AIR,

        SHADOW
    }
    public class SpawnPosition
    {
        public Godot.Vector2 Vector;
        public bool Valid = false;
    }
}
