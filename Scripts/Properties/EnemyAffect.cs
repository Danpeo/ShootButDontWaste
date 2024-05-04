using Platformer.Scripts.Entities.Enemies;

namespace Platformer.Scripts.Properties;

public static class EnemyAffect
{
    public static void Damage(IEnemy enemy, int damage)
    {
        Health enemyHealth = enemy.Health;
        enemyHealth.Reduce(damage);
    }
}