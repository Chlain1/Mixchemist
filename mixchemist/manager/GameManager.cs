using Godot;
using System;
using System.Diagnostics;

public partial class GameManager : Node
{
    private static GameManager _instance;
    public static GameManager Instance => _instance;
  
    public int Health {get; set;} = 3;
    // Use _EnterTree to make sure the Singleton instance is avaiable in _Ready()
    public override void _EnterTree(){
        if(_instance != null){
            this.QueueFree(); // The Singletone is already loaded, kill this instance
        }
        _instance = this;
    }
}
