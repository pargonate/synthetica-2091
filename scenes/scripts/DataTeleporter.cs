using Godot;

public partial class DataTeleporter : Area2D
{
	// Variables
	private string currentScene;
	private string nextScene;
	private SceneManager sceneManager;

	public override void _Ready()
	{
		sceneManager = GetNode<SceneManager>("/root/SceneManager");
	}

	public void _on_body_entered(Node body)
	//	Once Percy enters the teleporter, we have to switch
	//	from a phase to a level (vice versa).
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

				sceneManager.Add($"res://scenes/{nextScene}.tscn");
				GetTree().CallDeferred("change_scene_to_file", $"res://scenes/{nextScene}.tscn");
			}
		}
	}
}
