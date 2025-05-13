using Godot;

public partial class Level3 : Node2D
{
	// Nodes
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
				player.DisableMoving();
				backgroundMusic.VolumeDb = -20.0f;
			}
		}
	}
}
