using Godot;
using Platformer.Scripts.Entities;

namespace Platformer.Scripts.Properties;

public partial class CanDamageOnTouch : Area2D
{
	[Export] public int Damage { get; set; } = 1;

	public override void _Ready()
	{
		BodyEntered += body =>
		{
			if (body is Player player)
			{
				AffectPlayer.Damage(player, Damage);
			}
		};
	}
}
