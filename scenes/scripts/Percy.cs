using Godot;
using System;

public partial class Percy : CharacterBody2D
{
	[Export]
	public float speed { get; set; } = 200.0f;
	[Export]
	public float jump { get; set; } = 350.0f;
	public bool animationBusy = false;
	public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();
 	private AnimatedSprite2D animator;

	public override void _Ready()
	{
		animator = GetNode<AnimatedSprite2D>("animator");
	}

	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

		if (!IsOnFloor()) 
		{
			if (velocity.Y < 0)
			{
				animator.Play("jump");
			} else
			{
				animator.Play("fall");
			}

			velocity.Y += gravity * (float)delta;
		}
		else
		{
			if (Input.IsActionPressed("jump"))
			{
				velocity.Y = -jump;
			}

			if (velocity.X == 0)
			{
				animator.Play("idle");
			}

			_UpdateMovementAnimations(velocity.X);
		}

		velocity.X = 0;

		if (Input.IsActionPressed("left"))
		{
			velocity.X -= speed;
		}

		if (Input.IsActionPressed("right"))
		{
			velocity.X = speed;
		}		

		Velocity = velocity;
		MoveAndSlide();
	}

	private void _UpdateMovementAnimations(float velocityX)
	{
		bool walking = velocityX != 0;

		if (walking) {
			animator.Play("walk");
			animator.FlipH = velocityX < 0;
		}
	}

}
