using Godot;
using System;
using static ClassesAndEnums;

public class EnemyTexture : Sprite
{
    [Export] Texture fireEnemy;
    [Export] Texture waterEnemy;
    [Export] Texture earthEnemy;
    [Export] Texture airEnemy;
    public override void _Ready()
    {
        
    }

    public void SetEnemyTexture(Element element)
    {
        switch (element)
        {
            case Element.FIRE:
                Texture = fireEnemy;
                break;
            case Element.WATER:
                Texture = waterEnemy;
                break;
            case Element.EARTH:
                Texture = earthEnemy;
                break;
            case Element.AIR:
                Texture = airEnemy;
                break;
            default:
                break;
        }
    }
}