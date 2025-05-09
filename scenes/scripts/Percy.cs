using Godot;
using System;
using System.Threading.Tasks;

// [Tool]

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
	private bool died;
 	private AnimatedSprite2D animator;
	private PackedScene packet_bomb_scene;
	private Camera2D camera;
	private AnimationPlayer death_animator;
	private Sprite2D top_bar;
	private Sprite2D bottom_bar;
	private Label label;

	public override void _Ready()
	{
		animator = GetNode<AnimatedSprite2D>("animator");
		camera = GetNode<Camera2D>("Camera2D");
		packet_bomb_scene = GD.Load<PackedScene>("res://scenes/packet_bomb.tscn");

		death_animator = GetNode<AnimationPlayer>("death_animator");
		top_bar = GetNode<Sprite2D>("top_bar");
		bottom_bar = GetNode<Sprite2D>("bottom_bar");
		label = GetNode<Label>("Label");
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
		// top_bar.Visible = false;
		// bottom_bar.Visible = false;
		// label.Visible = false;

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

		if (Input.IsKeyPressed(Godot.Key.E)) {
			var packet_bomb = packet_bomb_scene.Instantiate() as CharacterBody2D;
			GetParent().AddChild(packet_bomb);
			packet_bomb.GlobalPosition = GlobalPosition + new Vector2(0, -1000);
		}
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

	public async void kill()
	{
		if (died == false) 
		{
			death_animator.Play("death");
			died = true;
			await Task.Delay(TimeSpan.FromSeconds(2));
			QueueFree();
			
		}
	}
}
