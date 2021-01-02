using Godot;
using System;
using EscapeMaze_Csharp.Items;

public class Player : Character
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    [Signal]
    delegate void moved();
    [Signal]
    delegate void dead();
    [Signal]
    delegate void grabbed_key();
    [Signal]
    delegate void win();
    // Called when the node enters the scene tree for the first time.
    

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        if (_can_move)
        {
            foreach( string dir in moves.Keys)
            {
                if (Input.IsActionPressed(dir)) {
                    if (move(dir)){
                        EmitSignal("moved");
                    }
                }
            } 
        }
        
    }


    private void _on_Player_area_entered(Area2D area)
    {
        if (area.IsInGroup("enemies"))
        {
            EmitSignal("dead");
        }
        if (area.HasMethod("pickup"))
        {
            Pickup p = (Pickup) area;
            p.pickup();
            
            if (p._type == "key_red")
            {
                EmitSignal("grabbed_key");
            }
            if (p._type == "star")
            {
                EmitSignal("win");
            }
        }


    }


}
