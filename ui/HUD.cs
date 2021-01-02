using Godot;
using System;

public class HUD : CanvasLayer
{
    
    private Label _scoreLabel;

    private Global _global;
    
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        _global = GetNode<Global>("/root/Global");
        _scoreLabel = GetNode<MarginContainer>("MarginContainer").GetNode<Label>("ScoreLabel");
        _scoreLabel.Text = _global._score.ToString();
    }

    void UpdateScore(int value)
    {
        _global._score += value;
        _scoreLabel.Text = _global._score.ToString();
    }


}

