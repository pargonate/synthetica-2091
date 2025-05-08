using Godot;
using System;
using System.Threading.Tasks;

public partial class laser_beam : Area2D
{
	private AnimatedSprite2D animator;
	private CollisionShape2D beam;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		animator = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		beam = GetNode<CollisionShape2D>("CollisionShape2D");
		ChangeBeamCollision(600, 0.8f);
		animator.AnimationFinished += OnAnimationFinished; 
	}

	private void ChangeBeamCollision(float y, float duration)
	{

		if (beam.Shape is RectangleShape2D rectangleShape) {
			float deltaY = (y - rectangleShape.Size.Y) / 2;

			Tween size = GetTree().CreateTween();
			size.TweenProperty(rectangleShape, "size", new Vector2(rectangleShape.Size.X, y), duration);
		
			Tween position = GetTree().CreateTween();
			position.TweenProperty(beam, "position", new Vector2(beam.Position.X, beam.Position.Y + deltaY), duration);
		}
	}

	private async void OnAnimationFinished() 
	{
		if (animator.Animation == "extend")
		{
			await Task.Delay(TimeSpan.FromSeconds(5));
			animator.Play("retract");
			ChangeBeamCollision(0, 0.6f);
		}
		else if (animator.Animation == "retract")
		{
			await Task.Delay(TimeSpan.FromSeconds(5));
			ChangeBeamCollision(600, 0.8f);
			animator.Play("extend");
		}
	}
}
