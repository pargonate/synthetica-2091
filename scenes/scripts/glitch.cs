using Godot;
using System;
using System.Threading.Tasks;

public partial class Glitch : Area2D
{
	private AnimatedSprite2D animator;
	private StaticBody2D body;
	private CollisionShape2D collision;

	public override void _Ready()
	{
		animator = GetNode<AnimatedSprite2D>("animator");
		body = GetNode<StaticBody2D>("StaticBody2D");
		collision = body.GetNode<CollisionShape2D>("collision");
		animator.AnimationFinished += OnAnimationFinished;
	}

	private void _on_body_entered(Node body)
	{
		if (body is CharacterBody2D character)
		{
			if (character is Percy player)
			{
				if (collision.Disabled != true)
				{
					animator.Play("break");
				}
			}
		}
	}

	private async void OnAnimationFinished() 
	//	Once the break animation has finished,
	//	we disable the collision, making it an 
	//	empty platform. Reenabled after 2 seconds.
	{
		if (animator.Animation == "break") 
		{
			collision.Disabled = true;
			await Task.Delay(TimeSpan.FromSeconds(2));
			if (IsInstanceValid(animator))
			{
				animator.Play("replenish");
			}
		} 
		else if (animator.Animation == "replenish")
		{
			collision.Disabled = false;
			if (IsInstanceValid(animator))
			{
				animator.Play("idle");
			}
		}
	}
}
