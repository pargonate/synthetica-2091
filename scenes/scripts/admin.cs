using Godot;
using System;
using System.Threading.Tasks;

public partial class Admin : CharacterBody2D
{
	private AnimatedSprite2D animator;
	private PackedScene packet_bomb_scene;
	private RandomNumberGenerator rng = new RandomNumberGenerator();
	private Timer spawnTimer;

	public override void _Ready()
	{
		animator = GetNode<AnimatedSprite2D>("animator");
		animator.AnimationFinished += OnAnimationFinished;
		packet_bomb_scene = GD.Load<PackedScene>("res://scenes/packet_bomb.tscn");
		spawnTimer = new Timer();
		spawnTimer.WaitTime = 5.0f;
		spawnTimer.Autostart = true;
		spawnTimer.OneShot = false;
		spawnTimer.Connect("timeout", Callable.From(OnSpawnTimerTimeOut));
		AddChild(spawnTimer);
	}

	private void OnSpawnTimerTimeOut()
	{
		for (int i = 0; i < 15; i++)
		{
			if (GetParent().SceneFilePath == "res://scenes/level_3.tscn")
			{
				SpawnPacketBomb(-1495, 17200, -150, -125);
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
	{
		rng.Randomize();
		float randomX = rng.RandfRange(minX, maxX);
		float randomY = rng.RandfRange(minY, maxY);

		var packet_bomb = packet_bomb_scene.Instantiate() as CharacterBody2D;
		packet_bomb.Position = new Vector2(Position.X + randomX, Position.Y + randomY);

		GetParent().CallDeferred("add_child", packet_bomb);
	}
}