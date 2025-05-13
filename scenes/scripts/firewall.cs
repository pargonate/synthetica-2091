using Godot;

public partial class Firewall : CharacterBody2D
{
	// Variables
	public float MoveSpeedLevel1 { get; set; } = 425.0f;
	public float MoveSpeedLevel2 { get; set; } = 315.0f;
	public float MoveSpeedLevel3 { get; set; } = 470.0f;
	private float universalMove;


	public override void _Ready()
	//	With lots of testing, the admin's speed needs
	//	to be adjusted based on the level as certain 
	//	obstacles take longer to overcome.
	{
		if (GetParent().SceneFilePath == "res://scenes/level_1.tscn")
		{
			universalMove = MoveSpeedLevel1;
		}
		else if (GetParent().SceneFilePath == "res://scenes/level_2.tscn")
		{
			universalMove = MoveSpeedLevel2;
		}
		else if (GetParent().SceneFilePath == "res://scenes/level_3.tscn")
		{
			universalMove = MoveSpeedLevel3;
		}
	}

	private void _on_area_2d_body_entered(Node body)
	{
		if (body is CharacterBody2D character)
		{
			if (character is Percy player)
			{
				player.Kill();
			}
		}
	}

	public override void _Process(double delta)
	{
		Vector2 velocity = Velocity;

		if (Position.X < 16100.0f)
		{
			velocity.X = universalMove;
		}
		else
		{
			velocity.X = 0.0f;
		}

		Velocity = velocity;
		MoveAndSlide();
	}
}
