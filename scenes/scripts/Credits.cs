using Godot;

public partial class Credits : Control
{
	private void _on_back_pressed()
	{
		GetTree().ChangeSceneToFile("res://scenes/main_menu.tscn");
	}
}
