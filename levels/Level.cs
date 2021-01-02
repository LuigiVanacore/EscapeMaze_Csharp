using Godot;
using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Xml.Linq;
using EscapeMaze_Csharp.Items;
using Object = Godot.Object;

public class Level : Node2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    [Export]
    private PackedScene _enemy;
    [Export]
    private PackedScene _pickup;

    private TileMap _itemsMap;
    private TileMap _wallsMap;
    private TileMap _groundMap;

    private HUD _hud;

    private Global _global;

    private List<Vector2> _doors;
    private Player _player;
    
    
    
    
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        _itemsMap = GetNode<TileMap>("Items");
        _wallsMap = GetNode<TileMap>("Walls");
        _groundMap = GetNode<TileMap>("Ground");
        
        _global = GetNode<Global>("/root/Global");

        _hud = GetNode<HUD>("HUD");


        _player = GetNode<Player>("Player");
        _doors = new List<Vector2>();
        
        // randomize();
        _itemsMap.Hide();
        
        set_camera_limits();
        
        int doorId = _wallsMap.TileSet.FindTileByName("door_red");
         foreach (var cell in _wallsMap.GetUsedCellsById(doorId))
         {
             _doors.Add((Vector2)cell);
         }

        spawn_items();
            
            _player.Connect("dead", this, "GameOver");
                _player.Connect("grabbed_key", this, "OnPlayerGrabbedKey");
                    _player.Connect("win", this, "OnPlayerWin");
                

        
        
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }

    void set_camera_limits()
    {
        var mapSize = _groundMap.GetUsedRect();
        var cellSize = _groundMap.CellSize;
        _player.GetNode<Camera2D>("Camera2D").LimitLeft = (int) ( mapSize.Position.x * cellSize.x );
        _player.GetNode<Camera2D>("Camera2D").LimitTop = (int) (mapSize.Position.y * cellSize.y);
        _player.GetNode<Camera2D>("Camera2D").LimitRight = (int) (mapSize.End.x * cellSize.x);
        _player.GetNode<Camera2D>("Camera2D").LimitBottom = (int) (mapSize.End.y * cellSize.y);
    }

    void spawn_items()
    {
        foreach (Vector2 cell in _itemsMap.GetUsedCells())
        {
            var id = _itemsMap.GetCellv(cell);
            var type = _itemsMap.TileSet.TileGetName(id);
            var pos = _itemsMap.MapToWorld(cell) + _itemsMap.CellSize / 2;
            switch (type)
            {
                case "slime_spawn":
                    Area2D s = (Area2D) _enemy.Instance();
                    s.Position = pos;
                    AddChild(s);
                    break;
                case "player_spawn":
                    _player.Position = pos;
                    break;
                case "coin":
                case "key_red":
                case "star":
                     Pickup p = (Pickup) _pickup.Instance();
                     AddChild(p);
                     p.Init(type, pos);
                     
                     p.Connect("coin_pickup", _hud, "UpdateScore");
                    break;

            }
        }
    }

    void OnPlayerGrabbedKey()
    {
        foreach (Vector2 cell in _doors)
        {
            _wallsMap.SetCellv(cell, -1);
        }
    }


            

            void OnPlayerWin()
            {
                _global.NextLevel();
            }

            void GameOver()
            {
                _global.GameOver();
            }
        
    
}



