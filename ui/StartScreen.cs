using Godot;
using System;

public class StartScreen : Control
{
    
    Label _scoreNotice;
    private Global _global;


// Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
    
        _scoreNotice = GetNode<Label>("ScoreNotice");
        _global = GetNode<Global>("/root/Global");

        _scoreNotice.Text = "High Score: " + _global._highscore.ToString();
  
    }

    public override void _Input(InputEvent _event)
    {
        if (_event.IsActionPressed("ui_select"))
        {
            _global.NewGame();
        }
    }


}
