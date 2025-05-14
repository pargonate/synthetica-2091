using Godot;
using Godot.Collections;
using System.Collections.Generic;
using System.Linq;

public partial class SceneManager : Node
{
	// Variables
	public List<string> scenes;

	public override void _Ready()
	{
		scenes = new List<string> { $"{GetTree().CurrentScene}" };
	}

	public void Add(string scene)
	{
		scenes.Add(scene);
	}

	public List<string> Fetch()
	{
		return scenes;
	}
}