using Godot;
using System;
using System.Text.RegularExpressions;

public partial class UI : Control
{
	private Label id;
	private TextureRect logo;
	private Label line;
	private Label level;

	public override void _Ready()
	{
		id = GetNode<Label>("VBoxContainer/top_bar/Player");
		logo = GetNode<TextureRect>("VBoxContainer/bottom_bar/HBoxContainer/logo");
		line = GetNode<Label>("VBoxContainer/bottom_bar/HBoxContainer/line");
		level = GetNode<Label>("VBoxContainer/top_bar/Level"); 

		var sceneFilePath = GetParent().GetParent().SceneFilePath;
		var match = Regex.Match(sceneFilePath, @"res://scenes/(?<sceneType>\w+)_(?<sceneNumber>\d+).tscn");
		if (match.Success)
		{
			var sceneType = match.Groups["sceneType"].Value;
			var sceneNumber = match.Groups["sceneNumber"].Value;

			if (sceneType == "level")
			{
				sceneType = "Level";
			}
			else if (sceneType == "phase")
			{
				sceneType = "Phase";
			}

			level.Text = $"{sceneType}: {sceneNumber}";
		}

		if (sceneFilePath != "res://scenes/phase_1.tscn")
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
