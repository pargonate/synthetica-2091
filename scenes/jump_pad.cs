using Godot;
using System;

public partial class jump_pad : RigidBody2D
{
	
	private AnimatedSprite2D animator;

	public override void _Ready()
	{
		animator = GetNode<AnimatedSprite2D>("animator");
		animator.AnimationFinished += OnAnimationFinished;
	}


	private void _on_area_2d_body_entered(Node body)
	{
		if (body is CharacterBody2D character)
		{
			if (character is Percy player)
			{
				player.Velocity = new Vector2(player.Velocity.X, -20.0f);
				Camera2D camera = player.GetNode<Camera2D>("Camera2D");
				camera.Zoom = new Vector2(0.8f, 0.8f);
				animator.Play("bounce");
			}
		}
	}

	private void OnAnimationFinished()
	{
		animator.Play("idle");
	}
}
