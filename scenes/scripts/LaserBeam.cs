using Godot;
using System;
using System.Threading.Tasks;

public partial class LaserBeam : Area2D
{
	// Nodes
	private AnimatedSprite2D animator;
	private CollisionShape2D beam;

	// Variables
	private int retractedLength = 0;
	private int extendedLength = 500;

	public override void _Ready()
	{
		animator = GetNode<AnimatedSprite2D>("beam");
		beam = GetNode<CollisionShape2D>("death_collider");
		ChangeBeamCollision(extendedLength, 0.8f);
		animator.AnimationFinished += OnAnimationFinished; 
	}

	private void ChangeBeamCollision(float y, float duration)
	//	To change the beam height, we have to follow the collider
	//	with the animation. This is done through tween-ing, the values
	//	and setting new vectors based on the new size. 
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
	//	Once the beam has finished staying extended,
	//	it'll start retracting.
	{
		if (!IsInstanceValid(animator)) return; // Ensure animator is valid

		if (animator.Animation == "extend")
		{
			await Task.Delay(TimeSpan.FromSeconds(2));
			if (!IsInstanceValid(animator)) return;
			animator.Play("retract");
			ChangeBeamCollision(retractedLength, 0.6f);
		}
		else if (animator.Animation == "retract")
		{
			await Task.Delay(TimeSpan.FromSeconds(2));
			if (!IsInstanceValid(animator)) return;
			ChangeBeamCollision(extendedLength, 0.8f);
			animator.Play("extend");
		}
	}

	private void _on_body_entered(Node body)
	{
		if (body is CharacterBody2D character)
		{
			if (character is Percy player)
			{
				player.kill();
			}
		}
	}
}
