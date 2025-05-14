using Godot;
public partial class Admin : CharacterBody2D
{
	// Nodes
	private AnimatedSprite2D animator;
	private PackedScene packet_bomb_scene;
	private Timer spawnTimer;

	// Variables
	private RandomNumberGenerator rng = new RandomNumberGenerator();


	public override void _Ready()
	//	Spawns packet bombs every 5 seconds
	{
		// Admin "Spawn" Animation
		animator = GetNode<AnimatedSprite2D>("animator");
		animator.AnimationFinished += OnAnimationFinished;
		// Timer
		packet_bomb_scene = GD.Load<PackedScene>("res://scenes/packet_bomb.tscn");
		spawnTimer = new Timer();
		spawnTimer.WaitTime = 5.0f;
		spawnTimer.Autostart = true;
		spawnTimer.OneShot = false;
		spawnTimer.Connect("timeout", Callable.From(OnSpawnTimerTimeOut));
		AddChild(spawnTimer);
	}

	private void OnSpawnTimerTimeOut()
	//	Spawn 15 packet bombs within the level,
	//	changing the boundaries due to severe level
	//	differences.
	{
		for (int i = 0; i < 15; i++)
		{
			if (GetParent().SceneFilePath == "res://scenes/level_3.tscn")
			{
				SpawnPacketBomb(-1495, 4500, -150, -125);
			}
			else
			{
				SpawnPacketBomb(-3500, 3500, -1000, -999);
			}
		}

		animator.Play("spawn");
	}

	private void OnAnimationFinished()
	{
		animator.Play("idle");
	}

	public void SpawnPacketBomb(float minX, float maxX, float minY, float maxY)
	//	Randomize spawn location between defined boundaries.
	{
		rng.Randomize();
		float randomX = rng.RandfRange(minX, maxX);
		float randomY = rng.RandfRange(minY, maxY);

		var packet_bomb = packet_bomb_scene.Instantiate() as CharacterBody2D;
		packet_bomb.Position = new Vector2(Position.X + randomX, Position.Y + randomY);

		GetParent().CallDeferred("add_child", packet_bomb);
	}
}