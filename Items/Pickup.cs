using System.Collections;
using Godot;

namespace EscapeMaze_Csharp.Items
{
    public class Pickup  : Area2D
    {
        [Signal]
        delegate void coin_pickup(int value);
        
        
        private System.Collections.Generic.Dictionary<string,string> _textures;
        public string _type;

        private Sprite _sprite;

        private Tween _tween;

        private CollisionShape2D _collisionShape2d;

        private AudioStreamPlayer _coinPickup;
        private AudioStreamPlayer _keyPickup;

        // Declare member variables here. Examples:
        // private int a = 2;
        // private string b = "text";

        // Called when the node enters the scene tree for the first time.
        public override void _Ready()
        {
            _sprite = GetNode<Sprite>("Sprite");
            _tween = GetNode<Tween>("Tween");
            _collisionShape2d = GetNode<CollisionShape2D>("CollisionShape2D");
            _coinPickup = GetNode<AudioStreamPlayer>("CoinPickup");
            _keyPickup = GetNode<AudioStreamPlayer>("KeyPickup");

            
            _textures = new System.Collections.Generic.Dictionary<string, string>();
            _textures.Add("coin", "res://assets/coin.png");
            _textures.Add("key_red", "res://assets/keyRed.png");
            _textures.Add("star", "res://assets/star.png");

            _tween.InterpolateProperty(_sprite, "scale", new Vector2(1, 1), new Vector2(3, 3),
                0.5f, Tween.TransitionType.Quad, Tween.EaseType.InOut);
            _tween.InterpolateProperty(_sprite, "modulate", new Color(1,1, 1, 1), new Color(1, 1, 1, 0), 0.5f,
                 Tween.TransitionType.Quad, Tween.EaseType.InOut);

        }

        public void Init(string type, Vector2 pos)
        {
            _sprite.Texture = (Texture) GD.Load(_textures[type]);
            _type = type;
            Position = pos;
        }

        public void pickup()
        {
            switch (_type)
            {
                case "coin":
                    _coinPickup.Play();
                    EmitSignal("coin_pickup", 1);
                    break;
                case "key_red":
                    _keyPickup.Play();
                    break;
                
            }
            _collisionShape2d.Disabled = true;
            _tween.Start();
        } 
        
        void _on_Tween_tween_completed( Godot.Object @object, NodePath key ) {
            this.QueueFree();
        }
    }
}
 
  
