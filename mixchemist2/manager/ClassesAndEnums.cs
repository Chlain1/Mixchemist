using System.Collections.Generic;
using Godot;
using System.Numerics;

public partial class ClassesAndEnums
{

    // TODO:
    // 1. Consider to map the colors to map on textures AND colors (as if in tuple)
    // => make sure to change the ElementStorage class.
    public static readonly Dictionary<Element, (Color, Texture)> ElementTextureMap = new()
    {
        { Element.FIRE, (Colors.Red, (Texture)GD.Load("res://player/Assets/fire.png")) },
        { Element.WATER, (Colors.Blue, (Texture)GD.Load("res://player/Assets/water.png")) },
        { Element.EARTH, (Colors.Brown, (Texture)GD.Load("res://player/Assets/earth.png")) },
        { Element.AIR, (Colors.White, (Texture)GD.Load("res://player/Assets/air.png")) },
        { Element.FIRE_WATER, (Colors.Purple, (Texture)GD.Load("res://player/Assets/fireWater.png")) },
        { Element.FIRE_EARTH, (Colors.Orange, (Texture)GD.Load("res://player/Assets/fireEarth.png")) },
        { Element.FIRE_AIR, (Colors.Pink, (Texture)GD.Load("res://player/Assets/fireAir.png")) },
        { Element.WATER_EARTH, (Colors.Green, (Texture)GD.Load("res://player/Assets/waterEarth.png")) },
        { Element.WATER_AIR, (Colors.Cyan, (Texture)GD.Load("res://player/Assets/waterAir.png")) },
        { Element.EARTH_AIR, (Colors.Yellow, (Texture)GD.Load("res://player/Assets/earthAir.png")) },
        { Element.FIRE_WATER_EARTH, (Colors.Lavender, (Texture)GD.Load("res://player/Assets/fireWaterEarth.png")) },
        { Element.FIRE_EARTH_AIR, (Colors.Olive, (Texture)GD.Load("res://player/Assets/fireEarthAir.png")) },
        { Element.WATER_EARTH_AIR, (Colors.Chocolate, (Texture)GD.Load("res://player/Assets/waterEarthAir.png")) },
        { Element.FIRE_WATER_AIR, (Colors.Magenta, (Texture)GD.Load("res://player/Assets/fireWaterAir.png")) },
        { Element.SHADOW, (Colors.Black, (Texture)GD.Load("res://player/Assets/shadow.png")) },
        { Element.NULL, (Colors.Gray, (Texture)GD.Load("res://player/Assets/null.png")) }
    };

    public static readonly Dictionary<Element, Dictionary<Element, double>> ModifierStrongVsEnemy = new()
{
    {
        //WATER > FIRE > AIR > EARTH
        //Ranking: 1. Water, 2. Earth, 3. Fire, 4. Air
        Element.FIRE, new Dictionary<Element, double>()
        {
            { Element.FIRE, 0.5 },
            { Element.WATER, 2.0 },
            { Element.EARTH, 1.0 },
            { Element.AIR, 0.25 },
            { Element.FIRE_WATER, 2.0 },
            { Element.FIRE_EARTH, 1.0 },
            { Element.FIRE_AIR, 1.0 },
            { Element.WATER_EARTH, 2.0 },
            { Element.WATER_AIR, 2.0 },
            { Element.EARTH_AIR, 1.0 },
            { Element.FIRE_WATER_EARTH, 4.0 },
            { Element.FIRE_WATER_AIR, 2.0 },
            { Element.FIRE_EARTH_AIR, 2.0 },
            { Element.WATER_EARTH_AIR, 2.0 },
            { Element.SHADOW, 6.0 }
        }
    },
    {
        //WATER > FIRE > AIR > EARTH
        //Ranking: 1. Earth, 2. Air, 3. Water, 4. Fire
        Element.WATER, new Dictionary<Element, double>()
        {
            { Element.FIRE, 0.25 },
            { Element.WATER, 0.5 },
            { Element.EARTH, 2.0 },
            { Element.AIR, 1.0 },
            { Element.FIRE_WATER, 1.0 },
            { Element.FIRE_EARTH, 2.0 },
            { Element.FIRE_AIR, 1.0 },
            { Element.WATER_EARTH, 2.0 },
            { Element.WATER_AIR, 1.0 },
            { Element.EARTH_AIR, 2.0 },
            { Element.FIRE_WATER_EARTH, 2.0 },
            { Element.FIRE_WATER_AIR, 2.0 },
            { Element.FIRE_EARTH_AIR, 4.0 },
            { Element.WATER_EARTH_AIR, 2.0 },
            { Element.SHADOW, 6.0 }
        }
    },
    {
        //WATER > FIRE > AIR > EARTH
        //Ranking: 1. Air, 2. Fire, 3. Earth, 4. Water
        Element.EARTH, new Dictionary<Element, double>()
        {
            { Element.FIRE, 1.0 },
            { Element.WATER, 0.25 },
            { Element.EARTH, 0.5 },
            { Element.AIR, 2.0 },
            { Element.FIRE_WATER, 1.0 },
            { Element.FIRE_EARTH, 1.0 },
            { Element.FIRE_AIR, 2.0 },
            { Element.WATER_EARTH, 1.0 },
            { Element.WATER_AIR, 2.0 },
            { Element.EARTH_AIR, 2.0 },
            { Element.FIRE_WATER_EARTH, 2.0 },
            { Element.FIRE_WATER_AIR, 2.0 },
            { Element.FIRE_EARTH_AIR, 4.0 },
            { Element.WATER_EARTH_AIR, 2.0 },
            { Element.SHADOW, 6.0 }
        }
    },
    {
        //WATER > FIRE > AIR > EARTH
        //Ranking: 1. Fire, 2. Water, 3. AIR, 4. EARTH
        Element.AIR, new Dictionary<Element, double>()
        {
            { Element.FIRE, 2.0 },
            { Element.WATER, 1.0 },
            { Element.EARTH, 0.25 },
            { Element.AIR, 0.5 },
            { Element.FIRE_WATER, 2.0 },
            { Element.FIRE_EARTH, 2.0 },
            { Element.FIRE_AIR, 2.0 },
            { Element.WATER_EARTH, 1.0 },
            { Element.WATER_AIR, 1.0 },
            { Element.EARTH_AIR, 1.0 },
            { Element.FIRE_WATER_EARTH, 2.0 },
            { Element.FIRE_WATER_AIR, 4.0 },
            { Element.FIRE_EARTH_AIR, 2.0 },
            { Element.WATER_EARTH_AIR, 2.0 },
            { Element.SHADOW, 6.0 }
        }
    }
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
