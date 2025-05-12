using Godot;
using System;

public partial class packet_bomb : CharacterBody2D
{
	public float fall { get; set; } = 200.0f;
	private AnimatedSprite2D animator;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		animator = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		animator.AnimationFinished += OnAnimationFinished;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Vector2 velocity = Velocity;

		if (!IsOnFloor())
		{
			velocity.Y = fall;
		}

		Velocity = velocity;
		MoveAndSlide();
	}

	private void _on_area_2d_body_entered(Node body)
	{
		if (body is StaticBody2D || (body is TileMap)) 
		{
			animator.Play("explode");
		}
	}

	private void _on_death_area_body_entered(Node body)
	{
		if (body is CharacterBody2D character)
		{
			if (character is Percy player)
			{
				if (animator.IsPlaying() == true)
				{
					player.kill();	
				}
			}
		}
	}

	private void OnAnimationFinished() 
	{
		QueueFree();
	}
}
