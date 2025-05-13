using Godot;
using System;

public partial class MainMenu : Control
{
	private Button play;
	private Button credits;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		play = GetNode<Button>("MarginContainer/VBoxContainer/play");
		credits = GetNode<Button>("MarginContainer/VBoxContainer/credits");
	}

	private void _on_play_pressed()
	{
		GetTree().ChangeSceneToFile("res://scenes/phase_1.tscn");
	}

	private void _on_credits_pressed()
	{
		GetTree().ChangeSceneToFile("res://scenes/credits.tscn");
	}
}
