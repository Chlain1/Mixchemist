using Godot;
using System;

public class PlayerTexture : Sprite
{
    [Export] private Texture highHp;
    [Export] private Texture mediumHighHp;
    [Export] private Texture mediumLowHp;
    [Export] private Texture lowHp;
    public override void _Ready()
    {
        
    }

    /// <summary>
    /// Sets the player texture based on the current health of the player.
    /// </summary>
    /// <param name="hp">The health of the player</param>
    /// <param name="maxHp">The maximum amount of health of the player</param>
    public void SetPlayerTexture(double hp, int maxHp) 
    {
        if (hp <= maxHp)
        {
            Texture = highHp;
        }
        if (hp <= maxHp * 0.75)
        {
            Texture = mediumHighHp;
        }
        if (hp <= maxHp * 0.5) 
        {
            Texture = mediumLowHp;
        }
        if (hp <= maxHp * 0.25)
        {
            Texture = lowHp;
        }
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
