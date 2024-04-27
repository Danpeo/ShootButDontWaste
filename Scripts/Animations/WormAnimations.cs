using System.Collections.Generic;
using Platformer.Scripts.Utils.Types;

namespace Platformer.Scripts.Animations;

public enum WormAnim
{
    Idle,
    Attack,
    Move,
    AgroMove
}

public class WormAnimation : ValueObject
{
    public string Name { get; }

    public WormAnimation(WormAnim animation)
    {
        Name = animation switch
        {
            WormAnim.Idle => "Idle",
            WormAnim.Attack => "Attack",
            WormAnim.Move => "Move",
            WormAnim.AgroMove => "AgroMove",
            _ => "Idle"
        };
    }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Name;
    }
}