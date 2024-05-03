namespace Platformer.Scripts.Properties.Interfaces;

public interface IThrowable
{
    void Pickup();
    void Drop();
    void Throw();
    bool IsPlayerInArea();
    bool IsHeld { get; }
}