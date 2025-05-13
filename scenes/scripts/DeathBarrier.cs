using Godot;
using System;

public partial class DeathBarrier : Area2D
{
	private void _on_body_entered(Node body)
	{
		if (body is CharacterBody2D character) 
		{
			if (character is Percy player)
			{
				player.kill();
			}
		}
	}
}
