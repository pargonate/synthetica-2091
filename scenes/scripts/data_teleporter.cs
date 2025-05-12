using Godot;
using System;
using System.Text.RegularExpressions;

public partial class data_teleporter : Area2D
{
	private string currentScene;
	// private int nextSceneNumber;
	private string nextScene;

	public void _on_body_entered(Node body)
	{
		if (body is CharacterBody2D character)
		{
			if (character is Percy)
			{
				currentScene = GetTree().CurrentScene.SceneFilePath;
				if (currentScene == "res://scenes/phase_1.tscn")
				{
					nextScene = "level_1";
				}
				else if (currentScene == "res://scenes/level_1.tscn")
				{
					nextScene = "phase_2";
				}
				else if (currentScene == "res://scenes/phase_2.tscn")
				{
					nextScene = "level_2";
				}
				else if (currentScene == "res://scenes/level_2.tscn")
				{
					nextScene = "phase_3";
				}
				else if (currentScene == "res://scenes/phase_3.tscn")
				{
					nextScene = "level_3";
				}
				else if (currentScene == "res://scenes/level_3.tscn")
				{
					nextScene = "end";
				}

				GetTree().ChangeSceneToFile($"res://scenes/{nextScene}.tscn");
			}
		}
	}
}
