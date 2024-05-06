using DVar.RandCreds;
using Godot;
using Platformer.Scripts.Effects;

namespace Platformer.Scripts.Utils;

public static class GodotNodeExtensions
{
    public static void PlayDefaultAnimation(this Node node, string animatedSpritePath = "AnimatedSprite2D")
    {
        var animatedSprite = node.GetNode<AnimatedSprite2D>(animatedSpritePath);
        animatedSprite.Play("Default");
    }

    public static TNode Autoload<TNode>(this Node node, string name) where TNode : class =>
        node.GetNode<TNode>($"/root/{name}");

    public static void FrameFreeze(this Node node, float frameFreezeDuration = 0.5f, float frameFreezeTiemScale = 0.05f)
    {
        node.Autoload<FrameFreeze>("FrameFreeze")
            .Activate(frameFreezeTiemScale, frameFreezeDuration);
    }

    public static void ApplyGravity(this CharacterBody2D characterBody, double delta)
    {
        Vector2 currVelocity = characterBody.Velocity;
        currVelocity.Y += World.GetGravity() * (float)delta;
        characterBody.Velocity = currVelocity;
    }

    public static void Stun(this CharacterBody2D characterBody, float stunDistanceX = 40f, float stunDistanceY = -180f)
    {
        int direction = RandGen.Boolean() ? -1 : 1;
        characterBody.Velocity = new Vector2(direction * stunDistanceX, stunDistanceY);
    }
}