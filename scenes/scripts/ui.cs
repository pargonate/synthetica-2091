using Godot;
using System;

public partial class ui : Control
{
	private Label id;
	private TextureRect logo;
	private Label line;

	public override void _Ready()
	{
		id = GetNode<Label>("VBoxContainer/top_bar/Player");
		logo = GetNode<TextureRect>("VBoxContainer/bottom_bar/HBoxContainer/logo");
		line = GetNode<Label>("VBoxContainer/bottom_bar/HBoxContainer/line");

		if (GetParent().GetParent().SceneFilePath != "res://scenes/phase_1.tscn")
		{
			id.Text = "Percy";
		}
	}


	public void ShowLine(string speaker, string text)
	{
		logo.Visible = true;
		if (speaker == "A")
		{
			logo.Texture = (Texture2D)ResourceLoader.Load("res://sprites/abell_logo.png");
			line.LabelSettings.FontColor = new Color("#D4B97E");
		}
		else if (speaker == "P")
		{
			logo.Texture = (Texture2D)ResourceLoader.Load("res://sprites/percy_logo.png");
			line.LabelSettings.FontColor = new Color("#378CE1");
		}

		line.Text = text;
	}
}
