using Godot;
using System;
using System.Threading.Tasks;

public partial class End : Node2D
{
	// Nodes
	private Camera2D camera;
	private AudioStreamPlayer2D audio;
	private AnimationPlayer animator;

	public override void _Ready()
	{
		camera = GetNode<Camera2D>("camera");
		audio = GetNode<AudioStreamPlayer2D>("credits");
		animator = GetNode<AnimationPlayer>("animator");
	}

	private async void _on_end_trigger_body_entered(Node body)
	//	Since this scene will be instantiated into level 3, 
	//	thus we need to adjust Percy, the camera, and the audio
	//	to play the end scene.
	{
		if (body is CharacterBody2D character)
		{
			if (character is Percy player)
			{
				// Player
				player.DisableFalling();
				player.AdjustUI();
				player.Modulate = new Color(1, 1, 1, 0);
				// Camera 
				camera.Enabled = true;
				camera.MakeCurrent();
				// Actually play the animation
				animator.Play("appear");
				audio.Play();
				await Task.Delay(TimeSpan.FromSeconds(45));
				// Reset SimpsonWave1995 (for extra effect :P)
				var background_audio = GetParent().GetNode<AudioStreamPlayer2D>("background_music");
				background_audio.VolumeDb = 0.0f;
				// End the game
				await Task.Delay(TimeSpan.FromSeconds(55));
				GetTree().ChangeSceneToFile("res://scenes/main_menu.tscn");
			}
		}
	}
}
