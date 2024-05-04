using Godot;

namespace Platformer.Scripts.Utils;

public static class NodeExtensions
{
    public static void PlayDefaultAnimation(this Node node, string animatedSpritePath = "AnimatedSprite2D")
    {
        var animatedSprite = node.GetNode<AnimatedSprite2D>(animatedSpritePath);
        animatedSprite.Play("Default");
    }

    public static TNode Autoload<TNode>(this Node node, string name) where TNode : class =>
        node.GetNode<TNode>($"/root/{name}");

    public static void ApplyGravity(this CharacterBody2D characterBody, double delta)
    {
        Vector2 currVelocity = characterBody.Velocity;
        currVelocity.Y += World.GetGravity() * (float)delta;
        characterBody.Velocity = currVelocity;
    }
}