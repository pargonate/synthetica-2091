using Godot;
using System;
using System.Threading.Tasks;

public partial class StorylineTrigger : Area2D
{
	[ExportGroup("Storyline Properties")]
	[Export(PropertyHint.Enum, "phase,level")]
	public string sceneType { get; set; } = "phase";

	[Export]
	public int sceneNumber { get; set; } = 1;

	[Export]
	public int line {get; set; } = 0;
	private Storyline storyline;
	private UI ui;
	private AudioStreamPlayer2D audioPlayer;

	public override void _Ready()
	{
		audioPlayer = GetNode<AudioStreamPlayer2D>("AudioStreamPlayer2D");
		storyline = GetNode<Storyline>("/root/Storyline");
	}


	private void _on_body_entered(Node body)
	{
		if (body is CharacterBody2D character)
		{
			if (character is Percy player)
			{
				StopSpeech();
				if (sceneType == "phase")
				{
					player.disable();
					player.slowFall();
				}
				ui = player.GetNode<UI>("UI");
				var (classification, speaker, text) = storyline.Play($"{sceneType}_{sceneNumber}", line);
				ui.ShowLine(speaker, text);
				audioPlayer.Stream = GD.Load<AudioStream>($"res://audio/{sceneType}_{sceneNumber}/{line}.ogg");
				if (line >= 5 && sceneType == "phase" && sceneNumber == 1) 
				{
					var id = ui.GetNode<Label>("VBoxContainer/top_bar/Player");
					id.Text = "Percy";
				}
				audioPlayer.Play();
			}
		}
	}

	private void StopSpeech()
	{
		foreach (Node node in GetTree().GetNodesInGroup("speech"))
		{
			if (node is AudioStreamPlayer2D player)
			{
				player.Stop();
				player.Stream = null;
				player.Seek(-1);
			}
		}
	}
}
