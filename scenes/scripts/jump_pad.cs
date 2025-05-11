using Godot;
using System;

public partial class jump_pad : Area2D
{
	
	private AnimatedSprite2D animator;

	public override void _Ready()
	{
		animator = GetNode<AnimatedSprite2D>("animator");
		animator.AnimationFinished += OnAnimationFinished;
	}


	private async void _on_body_entered(Node body)
	{
		if (body is CharacterBody2D character)
		{
			if (character is Percy player)
			{
				player.Velocity = new Vector2(player.Velocity.X, -2000.0f);
				animator.Play("bounce");

				if (player.Velocity.Y < 0)
				{
					await ToSignal(GetTree().CreateTimer(0.01f), "timeout");
				}

				Camera2D camera = player.GetNode<Camera2D>("Camera2D");
				Control ui = player.GetNode<Control>("UI");
				camera.Zoom = new Vector2(0.8f, 0.8f);
				ui.Size = new Vector2(1976, 1088);
				ui.Position = new Vector2(-485, -314);
				ui.Scale = new Vector2(0.49f, 0.49f);
			}
		}
	}

	private void OnAnimationFinished()
	{
		animator.Play("idle");
	}
}
