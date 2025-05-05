using Godot;
using System;

public partial class data_stream : Path2D
{

	private AnimationPlayer animator;

	public override void _Ready()
	{
		animator = GetNode<AnimationPlayer>("AnimationPlayer");
	}


	private void _on_area_2d_body_entered(Node body)
	{
		if (body is CharacterBody2D character)
		{
			if (character is Percy player)
			{
				animator.Play("moving");
			}
		}
	}

	private void _on_area_2d_body_exited(Node body) 
	{
		if (body is CharacterBody2D character)
		{
			if (character is Percy player)
			{
				animator.PlayBackwards("moving");
			}
		}
	}

}
