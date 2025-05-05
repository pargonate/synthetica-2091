using Godot;
using System;

public partial class Percy : CharacterBody2D
{
	[Export]
	public float walk { get; set; } = 200.0f;
	[Export]
	public float run { get; set; } = 500.0f;
	[Export]
	public float jump { get; set; } = 600.0f;
	public bool animationBusy = false;
	public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();
 	private AnimatedSprite2D animator;

	public override void _Ready()
	{
		animator = GetNode<AnimatedSprite2D>("animator");
	}

	public void flip()
	{
		if (Input.IsKeyPressed(Godot.Key.A) || Input.IsKeyPressed(Godot.Key.Left)) {
			animator.FlipH = true;
		} else if (Input.IsKeyPressed(Godot.Key.D) || Input.IsKeyPressed(Godot.Key.Right)) {
			animator.FlipH = false;
		}		
	}

	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

		if (!IsOnFloor()) 
		{
			if (velocity.Y < 0)
			{
				animator.Play("jump");
				flip();
			} else
			{
				animator.Play("fall");
				flip();
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

			Camera2D camera = GetNode<Camera2D>("Camera2D");
			camera.Zoom = new Vector2(1.4f, 1.4f);
			_UpdateMovementAnimations(velocity.X);
		}

		velocity.X = 0;

		if (Input.IsActionPressed("left"))
		{
			velocity.X -= walk;
			if (Input.IsKeyPressed(Godot.Key.Shift))
			{
				velocity.X -= run;
			}
		}

		if (Input.IsActionPressed("right"))
		{
			velocity.X = walk;

			if (Input.IsKeyPressed(Godot.Key.Shift))
			{
				velocity.X = run;
			}
		}		

		Velocity = velocity;
		MoveAndSlide();
	}

	private void _UpdateMovementAnimations(float velocityX)
	{
		bool walking = velocityX != 0;
		bool running = velocityX > 200.0f || velocityX < -200.0f;

		if (running) {
			animator.Play("run");
		}
		else if (walking) {
			animator.Play("walk");
		}

		flip();
	}

}
