using Godot;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using Dungeon.Generator;
using static ClassesAndEnums;

public class LevelConfiguration : Node2D
{
    [Export] private PackedScene enemyScene;
    private PackedScene playerScene = GD.Load<PackedScene>("res://player/Player.tscn");
    [Export] private List<Element> allowedBasicElements = new List<Element>();
    [Export] private int maxEnemyCount;

    private TileMap tileMap;
    private Node2D worldNode;
    private Dungeon.Generator.World world;
    private int target_fps = 60;
    private Vector2 spawnArea;
    private Random rnd = new Random();
    private SpawnPosition validSpawnPos = new SpawnPosition();
    private int enemyCount = 0;
    private int frameCount = 0;
    private bool playerSpawned = false; 

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        tileMap = GetNode<TileMap>("WorldNode/TileMap");
        worldNode = GetNode<Node2D>("WorldNode");
        world = GetNode<Dungeon.Generator.World>("WorldNode");
        Debug.WriteLine(this.Name);
        Engine.TargetFps = target_fps;
        
        // P.S: Ich weiß das ist extrem dämlich, aber es funktioniert xoxo Eric
        switch (this.Name)
        {
            case "LevelNode":
                allowedBasicElements.Add(Element.FIRE);
                allowedBasicElements.Add(Element.WATER);
                allowedBasicElements.Add(Element.EARTH);
                allowedBasicElements.Add(Element.AIR);
                break;
            default:
                Debug.WriteLine("ALTER FALTER, DU HAST EIN LEVEL AUFGERUFEN DAS ES GAR NICHT GEBEN TUT!");
                break;
        }
        
        GameManager.Instance.AllowedBasicElements = allowedBasicElements;
        while (!validSpawnPos.Valid)
        {
            GetValidSpawnPosition();
        }

    }
    public override void _Process(float delta)
    {
        if (!playerSpawned && validSpawnPos.Valid && world.GetStatus())
        {
            Player player = (Player)playerScene.Instance();
            world.AddChild(player);
            player.Position = validSpawnPos.Vector;
            playerSpawned = true;
        } else
        {
            GetValidSpawnPosition();
        }

        if (frameCount % (target_fps * 10) == 0 && validSpawnPos.Valid && world.GetStatus() && !(frameCount < target_fps * 10))
        {
            SpawnEnemy();
        }
        else
        {
            GetEnemyCount();
        }
        frameCount++;
    }
    private void SpawnEnemy()
    {
        if (validSpawnPos.Valid && enemyCount < maxEnemyCount) 
        {
            AbstractEnemy enemy = (AbstractEnemy)enemyScene.Instance();
            worldNode.AddChild(enemy);
            enemy.Position = validSpawnPos.Vector;
            Debug.WriteLine("spawned enemy");
            GetEnemyCount();
        }

    }
    private void GetValidSpawnPosition()
    {
        spawnArea.x = rnd.Next(-4096, 4096);
        spawnArea.y = rnd.Next(-4096, 4096);
        var cell_coord = tileMap.WorldToMap(spawnArea);
        var cell_type_id = tileMap.GetCellv(cell_coord);
        if (cell_type_id != -1)
        {
            validSpawnPos.Vector = cell_coord;
            validSpawnPos.Valid = true;
        }
        else
        {
            validSpawnPos.Valid = false;
            GetValidSpawnPosition();
        }
    }
    private int GetEnemyCount()
    {
        int count = 0;
            foreach (Node child in worldNode.GetChildren())
            {
                if (child is AbstractEnemy)
                {
                    count++;
                }
            }
            enemyCount = count;
        Debug.WriteLine("Enemies: " + enemyCount);
        return enemyCount;
    }
}
