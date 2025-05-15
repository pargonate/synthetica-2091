using System;
using System.Collections.Generic;
using Godot;

public partial class MainMenu : Control
{
	// Nodes 
	private Camera2D camera;
	private AudioStreamPlayer2D bootAudio;
	private AnimationPlayer animator;

	// Variables
	private SceneManager sceneManager;
	private List<String> scenes;

	public override void _Ready()
	{
		animator = GetNode<AnimationPlayer>("animator");
		camera = GetNode<Camera2D>("camera");
		bootAudio = GetNode<AudioStreamPlayer2D>("boot_audio");
		sceneManager = GetNode<SceneManager>("/root/SceneManager");

		scenes = sceneManager.Fetch();

		if (scenes.Count == 1)
		{
			animator.Play("appear");
			bootAudio.Play();
		}
		else
		{
			camera.Position = new Vector2(960.0f, 540.0f);
		}
	}

	private void _on_play_pressed()
	{
		GetTree().ChangeSceneToFile("res://scenes/intro.tscn");
		sceneManager.Add("res://scenes/intro.tscn");
	}

	private void _on_credits_pressed()
	{
		GetTree().ChangeSceneToFile("res://scenes/credits.tscn");
		sceneManager.Add("res://scenes/credits.tscn");
	}

	private void _on_quit_pressed()
	{
		GetTree().Quit();
	}
}
