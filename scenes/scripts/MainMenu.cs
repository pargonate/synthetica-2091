using Godot;

public partial class MainMenu : Control
{
	private void _on_play_pressed()
	{
		GetTree().ChangeSceneToFile("res://scenes/phase_1.tscn");
	}

	private void _on_credits_pressed()
	{
		GetTree().ChangeSceneToFile("res://scenes/credits.tscn");
	}

	private void _on_quit_pressed()
	{
		GetTree().Quit();
	}
}
