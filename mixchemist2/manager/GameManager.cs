using System.Collections.Generic;
using System.Diagnostics;
using Godot;
using System.Text.Json;
using static ClassesAndEnums;

public partial class GameManager : Node
{
    private static GameManager _instance;
    public static GameManager Instance => _instance;
    
    private List<Element> allowedBasicElements = new List<Element>();
    
    // Score
    private int score;
    public override void _Ready()
    {
        score = 0;
    }

    public void SetScore(int score)
    {
        this.score = score;
        Debug.WriteLine("New Score: " + score);
    }

    public int GetScore()
    {
        return score;
    }
    
    public List<Element> AllowedBasicElements
    {
        get => allowedBasicElements;
        set => allowedBasicElements = value;
    }

    // Use _EnterTree to make sure the Singleton instance is avaiable in _Ready()
    public override void _EnterTree()
    {
        if (_instance != null)
        {
            this.QueueFree(); // The Singleton is already loaded, kill this instance
        }

        _instance = this;
    }
}