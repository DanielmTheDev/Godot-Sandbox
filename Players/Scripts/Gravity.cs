using Godot;

namespace Sandbox.Players.Scripts;

public partial class Gravity : Node
{
    [Export] public MainCharacter MainCharacter = null!;

    private const float GravityForce = 1000f;

    public override void _PhysicsProcess(double delta)
    {
        MainCharacter.Velocity = new Vector2(MainCharacter.Velocity.X, GetYMovement(delta));
        MainCharacter.MoveAndSlide();
    }

    private float GetYMovement(double delta)
        => MainCharacter.Velocity.Y + (!MainCharacter.IsOnFloor()
            ? GravityForce * (float)delta
            : 0);
}