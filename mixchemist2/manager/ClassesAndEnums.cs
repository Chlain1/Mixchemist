using System.Collections.Generic;
using Godot;
using System.Numerics;

public partial class ClassesAndEnums
{

    // TODO:
    // 1. Consider to map the colors to map on textures AND colors (as if in tuple)
    // => make sure to change the ElementStorage class.
    public static readonly Dictionary<Element, Color> ElementTextureMap = new()
    {
        { Element.FIRE, Colors.Red },
        { Element.WATER, Colors.Blue },
        { Element.EARTH, Colors.Brown },
        { Element.AIR, Colors.White },
        { Element.FIRE_WATER, Colors.Purple },
        { Element.FIRE_EARTH, Colors.Orange },
        { Element.FIRE_AIR, Colors.Pink },
        { Element.WATER_EARTH, Colors.Green },
        { Element.WATER_AIR, Colors.Cyan },
        { Element.EARTH_AIR, Colors.Yellow },
        { Element.FIRE_WATER_EARTH, Colors.Lavender },
        { Element.FIRE_EARTH_AIR, Colors.Olive },
        { Element.WATER_EARTH_AIR, Colors.Chocolate },
        { Element.FIRE_WATER_AIR, Colors.Magenta },
        { Element.SHADOW, Colors.Black },
        {Element.NULL, Colors.Gray}
    };
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

        SHADOW,
        
        NULL
    }
    public class SpawnPosition
    {
        public Godot.Vector2 Vector;
        public bool Valid = false;
    }
}
