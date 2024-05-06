using System;
using Godot;

namespace Platformer.Scripts.Entities.Areas;

public partial class SpotArea : Area2D
{
	public Action? OnSpotted { get; set; }
	public Action? OnLosed { get; set; }
	public Player? Player { get; set; }

	public override void _Ready()
	{
		BodyEntered += body =>
		{
			if (body is Player player)
			{
				Player = player;
				OnSpotted?.Invoke();
			}
		};

		BodyExited += body =>
		{
			if (body is Player)
			{
				Player = null;
				OnLosed?.Invoke();
			}
		};
	}
}
