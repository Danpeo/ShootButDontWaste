using System;

namespace Platformer.Scripts.Properties.Interfaces;

public interface IHittableEnemy
{
    void Hit();
    void OnHeatlhDamaged(Action action);
}