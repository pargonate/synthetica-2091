using Godot;

public partial class PacketBomb : CharacterBody2D
{
	// Nodes
	private AnimatedSprite2D animator;

	// Variables
	public float Fall { get; set; } = 200.0f;

	public override void _Ready()
	{
		animator = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		animator.AnimationFinished += OnAnimationFinished;
	}

	private void _on_area_2d_body_entered(Node body)
	{
		if (body is StaticBody2D || (body is TileMapLayer)) 
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
					player.Kill();	
				}
			}
		}
	}

	private void OnAnimationFinished() 
	{
		QueueFree();
	}
	
	public override void _Process(double delta)
	{
		Vector2 velocity = Velocity;

		if (!IsOnFloor())
		{
			velocity.Y = Fall;
		}

		Velocity = velocity;
		MoveAndSlide();
	}

}
