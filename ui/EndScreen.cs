using Godot;
using System;

public class EndScreen : Control
{
   
    private Global _global;
    
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        _global = GetNode<Global>("/root/Global");
    }

    public void _on_Timer_timeout()
    {
        GetTree().ChangeScene(_global._startScreen);
    }
}
