using Godot;
using System;
using System.Threading.Tasks;

public class Enemy : Character
{
  
    private RandomNumberGenerator _rand;
    private string[] _movesArray;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        base._Ready();

        _rand = new RandomNumberGenerator();
        _movesArray = new string[moves.Keys.Count];
        moves.Keys.CopyTo(_movesArray, 0);
        _can_move = false;
        _facing = _movesArray[_rand.RandiRange(0, 3)];
        AwaitTimer();

    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        if (_can_move)
        {
            if (!move(_facing) || (_rand.RandiRange(0,10) > 5))
            {
                
                _facing = _movesArray[_rand.RandiRange(0, 3)];
            }
        }
    }

    private async void AwaitTimer()
    {
         await ToSignal(this.GetTree().CreateTimer(0.5f), "timeout");
         _can_move = true;

    }
}
