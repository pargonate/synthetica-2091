using Godot;
using System;

public partial class firewall : CharacterBody2D
{
	public float moveSpeedLevel1 { get; set; } = 425.0f;
	public float moveSpeedLevel2 { get; set; } = 315.0f;
	public float moveSpeedLevel3 { get; set; } = 470.0f;
	private float universalMove;


	public override void _Ready()
	{
		if (GetParent().SceneFilePath == "res://scenes/level_1.tscn")
		{
			universalMove = moveSpeedLevel1;
		}
		else if (GetParent().SceneFilePath == "res://scenes/level_2.tscn")
		{
			universalMove = moveSpeedLevel2;
		}
		else if (GetParent().SceneFilePath == "res://scenes/level_3.tscn")
		{
			universalMove = moveSpeedLevel3;
		}
	}

	private void _on_area_2d_body_entered(Node body)
	{
		if (body is CharacterBody2D character)
		{
			if (character is Percy player)
			{
				player.kill();
			}
		}
	}

	public override void _Process(double delta)
	{
		Vector2 velocity = Velocity;

		if (Position.X < 16100.0f)
		{
			velocity.X = universalMove;
		}
		else
		{
			velocity.X = 0.0f;
		}

		Velocity = velocity;
		MoveAndSlide();
	}
}
