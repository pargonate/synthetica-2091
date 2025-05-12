using Godot;
using System;
using System.Threading.Tasks;

public partial class end : Node2D
{
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
	{
		if (body is CharacterBody2D character)
		{
			if (character is Percy player)
			{
				player.stop();
				player.adjustUI();
				camera.Enabled = true;
				camera.MakeCurrent();
				player.Modulate = new Color(1, 1, 1, 0);
				animator.Play("appear");
				audio.Play();
				await Task.Delay(TimeSpan.FromSeconds(45));
				var background_audio = GetParent().GetNode<AudioStreamPlayer2D>("background_music");
				background_audio.VolumeDb = 0.0f;
				await Task.Delay(TimeSpan.FromSeconds(55));
				GetTree().ChangeSceneToFile("res://scenes/main_menu.tscn");
			}
		}
	}
}
