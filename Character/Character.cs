using Godot;
using System;

public class Character : Area2D
{
	[Export]
	protected int _speed;
	protected bool _can_move = true;
	protected string _facing = "right";
	public const int tile_size=64;

	protected System.Collections.Generic.Dictionary<string,Vector2> moves;
	protected System.Collections.Generic.Dictionary<string, RayCast2D> raycasts;
	protected AnimationPlayer anim;
	protected Tween tween;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		moves = new System.Collections.Generic.Dictionary<string, Vector2>();
		moves.Add("right", new Vector2(1, 0));
		moves.Add("left", new Vector2(-1, 0));
		moves.Add("up", new Vector2(0, -1));
		moves.Add("down", new Vector2(0, 1));

		raycasts = new System.Collections.Generic.Dictionary<string, RayCast2D>();
		raycasts.Add("right", (RayCast2D)GetNode<RayCast2D>("RayCastRight"));
		raycasts.Add("left", (RayCast2D)GetNode<RayCast2D>("RayCastLeft"));
		raycasts.Add("up", (RayCast2D)GetNode<RayCast2D>("RayCastUp"));
		raycasts.Add("down", (RayCast2D)GetNode<RayCast2D>("RayCastDown"));

		anim = GetNode<AnimationPlayer>("AnimationPlayer");
		tween = GetNode<Tween>("MoveTween");

	}

	public bool move(string dir)
	{
		anim.PlaybackSpeed = _speed;
		_facing = dir;
		var ray = (RayCast2D)raycasts[dir];
		if (ray.IsColliding())
		{
			return false;
		}

		_can_move = false;
		anim.Play(_facing);
		tween.InterpolateProperty(this, "position", this.Position, this.Position + (Vector2)moves[_facing] * tile_size, 1.0f / _speed, Tween.TransitionType.Sine, Tween.EaseType.InOut);
		tween.Start();

		return true;

	}


	private void _on_MoveTween_tween_completed(Godot.Object @object, NodePath key)
	{
		_can_move = true;
	}

}
