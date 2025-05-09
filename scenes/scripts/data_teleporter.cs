using Godot;
using System;
using System.Text.RegularExpressions;

public partial class data_teleporter : Area2D
{
	private string currentScene;
	private int nextSceneNumber;
	private string nextScene;

	public void _on_body_entered(Node body)
	{
		if (body is CharacterBody2D character)
		{
			if (character is Percy)
			{
				currentScene = GetTree().CurrentScene.SceneFilePath;
				var match = Regex.Match(currentScene, @"_(\d+)");

				if (match.Success)
				{
					nextSceneNumber = Convert.ToInt32(match.Groups[1].Value) + 1;
				}
				else
				{
					GD.PrintErr("Failed to find number value in scene file path");
				}

				GetTree().ChangeSceneToFile("res://scenes/level_" + nextSceneNumber + ".tscn");
			}
		}
	}
}
