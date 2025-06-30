using Godot;

namespace Sandbox.Players.Scripts;

public partial class Projectile : Area2D
{
    [Export] public Vector2 Velocity = new(1, 0);

    public override void _PhysicsProcess(double delta)
    {
        Position += Velocity * (float)delta;
    }
}