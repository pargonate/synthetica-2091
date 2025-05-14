using Godot;
using System;
using System.Threading.Tasks;

public partial class Percy : CharacterBody2D
{
	// Nodes
 	private AnimatedSprite2D animator;
	private Camera2D camera;
	private Control UI;
	private Label debugY;
	private AnimationPlayer deathAnimator;
	private Node2D terminatedShade;
	private Sprite2D topBar;
	private Sprite2D bottomBar;
	private AudioStreamPlayer2D terminatedAudio;

	// Variables
	[Export]
	public float walk { get; set; } = 200.0f;
	[Export]
	public float run { get; set; } = 500.0f;
	[Export]
	public float jump { get; set; } = 600.0f;
	[Export]
	public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();
	public bool animationBusy = false;
	private bool died;
	private bool lowGravity = false;
	private bool stopMovement = false;
	private bool stopFalling = false;

	public override void _Ready()
	{
		animator = GetNode<AnimatedSprite2D>("animator");
		camera = GetNode<Camera2D>("Camera2D");
		UI = GetNode<Control>("UI");

		deathAnimator = GetNode<AnimationPlayer>("death_animator");
		terminatedShade = GetNode<Node2D>("terminated_shade");
		topBar = GetNode<Sprite2D>("terminated_shade/top_bar");
		bottomBar = GetNode<Sprite2D>("terminated_shade/bottom_bar");
		debugY = GetNode<Label>("debug_y");
		terminatedAudio = GetNode<AudioStreamPlayer2D>("terminated_audio");
	}

	public void flip(float velocityX)
	//	Change the direction of Percy based on the
	//	key and velocity.
	{
		if (Input.IsKeyPressed(Godot.Key.A) || Input.IsKeyPressed(Godot.Key.Left)) {
			if (velocityX < 0)
			{
				animator.FlipH = true;
			}
		} else if (Input.IsKeyPressed(Godot.Key.D) || Input.IsKeyPressed(Godot.Key.Right)) {
			if (velocityX > 0)
			{
				animator.FlipH = false;
			}
		}		
	}

	public async void Kill()
	{
		if (died == false) 
		{
			deathAnimator.Play("death");

			foreach (Node node in GetTree().GetNodesInGroup("speech"))
			{
				if (node is AudioStreamPlayer2D player)
				{
					player.Stop();
					player.Stream = null;
					player.Seek(-1);
				}
			}

			terminatedAudio.Play();
			died = true;
			await Task.Delay(TimeSpan.FromSeconds(2));
			GetTree().ChangeSceneToFile(GetParent().SceneFilePath);
		}
	}

	public void DisableMoving()
	{
		stopMovement = true;
	}

	public void DisableFalling()
	{
		stopFalling = true;
	}

	public void SlowFall()
	{
		lowGravity = true;
	}

	public void AdjustUI()
	{
		UI.QueueFree();
		camera.Zoom = new Vector2(0.5f, 0.5f);
	}

	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

		if (!IsOnFloor()) 
		{
			if (stopFalling == false) // Flag for when in a phase
			{
				if (velocity.Y < 0)
				{
					animator.Play("jump");
					flip(velocity.X);
				} else
				{
					animator.Play("fall");
					flip(velocity.X);
				}

				if (lowGravity == true) // Also with phases, the player needs to move at a constant speed
				{
					velocity.Y = gravity;
				}
				else
				{
					velocity.Y += gravity * (float)delta;
				}
			}
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

			// Due to the camera zooming out, 
			// we also have to zoom out the 
			// other nodes (UI, Terminated) 
			camera.Zoom = new Vector2(1.4f, 1.4f);
			UI.Size = new Vector2(2020, 1094);
			UI.Position = new Vector2(-285, -202);
			UI.Scale = new Vector2(0.28f, 0.28f);
			terminatedShade.Scale = new Vector2(1f, 1f);
			_UpdateMovementAnimations(velocity.X);
		}

		velocity.X = 0;

		if (stopMovement == false) // For when we just need to stop x-axis movement
		{
			if (Input.IsActionPressed("left"))
			{
				if (!Input.IsActionPressed("right"))
				{
					velocity.X -= walk;
					if (Input.IsKeyPressed(Godot.Key.Shift))
					{
						velocity.X -= run;
					}
				}
			}

			if (Input.IsActionPressed("right"))
			{
				if (!Input.IsActionPressed("left"))
				{
					velocity.X = walk;
	
					if (Input.IsKeyPressed(Godot.Key.Shift))
					{
						velocity.X = run;
					}
				}
			}	
		}	

		debugY.Text = $"Y-cord: {Math.Round(Position.Y, 0)}";
		if (stopFalling == false)
		{
			Velocity = velocity;
			MoveAndSlide();
		}
	}

	private void _UpdateMovementAnimations(float velocityX)
	//	Play animations based on movement speed
	{
		bool walking = velocityX != 0;
		bool running = velocityX > 200.0f || velocityX < -200.0f;

		if (running) {
			animator.Play("run");
		}
		else if (walking) {
			animator.Play("walk");
		}

		flip(velocityX);
	}
}
