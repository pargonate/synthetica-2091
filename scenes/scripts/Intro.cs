using Godot;
using System;

public partial class Intro : Node2D
{
	private void _on_animator_animation_finished(string animationName)
	{
		if (animationName == "appear")
		{
			GetTree().ChangeSceneToFile("res://scenes/phase_1.tscn");
		}
	}
}
