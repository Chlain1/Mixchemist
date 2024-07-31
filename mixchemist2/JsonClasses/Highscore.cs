using Godot;
using System;
using NJsonSchema;

public class Highscore
{
    
    public string Name;
    public int Score;

    public Highscore(string name, int score)
    {
        Name = name;
        Score = score;
    }

}
