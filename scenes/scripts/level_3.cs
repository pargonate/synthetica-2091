using Godot;
using System;

public partial class level_3 : Node2D
{
	private AudioStreamPlayer2D backgroundMusic;

	public override void _Ready()
	{
		backgroundMusic = GetNode<AudioStreamPlayer2D>("background_music");
	}


	private void _on_move_trigger_body_entered(Node body)
	{
		if (body is CharacterBody2D character)
		{
			if (character is Percy player)
			{
				player.disable();
				backgroundMusic.VolumeDb = -20.0f;
			}
		}
	}
}
