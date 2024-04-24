using Platformer.Scripts.Entities;

namespace Platformer.Scripts.Properties;

public static class AffectPlayer
{
    public static void Damage(Player player, int damage)
    {
        Ammo playerAmmo = player.Ammo;
        playerAmmo.ReduceByDamage(damage);
    }
}