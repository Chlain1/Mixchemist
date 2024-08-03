using Godot;
using System;

public class HealthBar : HBoxContainer
{
    private static Label hpNumber;
    private static TextureProgress hpTextureProgress;
    

    
    public override void _Ready()
    {
        hpNumber = GetNode<Label>("Background/Number");
        hpTextureProgress = GetNode<TextureProgress>("TextureProgress");
    }

    /// <summary>
    /// Updates the health bar with the current HP value
    /// </summary>
    /// <param name="currentHP">The new health value of the player</param>
    public void UpdateHealthBar(int currentHP) 
    { 
        hpTextureProgress.Value = currentHP;
        hpNumber.Text = currentHP.ToString();
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
