using Godot;

public partial class StorylineTrigger : Area2D
{
	// Nodes
	private Storyline storyline;
	private UI ui;
	private AudioStreamPlayer2D audioPlayer;

	// Variables
	[ExportGroup("Storyline Properties")]
	[Export(PropertyHint.Enum, "phase,level")]
	public string SceneType { get; set; } = "phase";

	[Export]
	public int SceneNumber { get; set; } = 1;

	[Export]
	public int Line {get; set; } = 0;

	public override void _Ready()
	{
		audioPlayer = GetNode<AudioStreamPlayer2D>("AudioStreamPlayer2D");
		storyline = GetNode<Storyline>("/root/Storyline");
	}


	private void _on_body_entered(Node body)
	//	Once Percy enters the area, we call the
	//	UI to show the line, which is fetched
	//	from the singleton storyline. Then we play
	//	audio in parallel. 
	{
		if (body is CharacterBody2D character)
		{
			if (character is Percy player)
			{
				StopSpeech();
				if (SceneType == "phase")
				{
					player.DisableMoving();
					player.SlowFall();
				}
				ui = player.GetNode<UI>("UI");
				var (classification, speaker, text) = storyline.Play($"{SceneType}_{SceneNumber}", Line);
				ui.ShowLine(speaker, text);
				audioPlayer.Stream = GD.Load<AudioStream>($"res://audio/{SceneType}_{SceneNumber}/{Line}.ogg");
				if (Line >= 5 && SceneType == "phase" && SceneNumber == 1) 
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
