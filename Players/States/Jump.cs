using Godot;

namespace Sandbox.Players.States;

public sealed class Jump : State
{
    private const float JumpVelocity = -400f;
    private readonly AnimationPlayer _animPlayer;

    public Jump(AnimationPlayer animPlayer)
    {
        _animPlayer = animPlayer;
    }

    public override void Enter(Scripts.MainCharacter character)
    {
        // Set initial upward velocity
        character.Velocity = new Vector2(character.Velocity.X, JumpVelocity);
        _animPlayer.Play("jump");
    }

    public override void Update(Scripts.MainCharacter character, double delta)
    {
        // Optional: horizontal air control
        var x = Input.IsActionPressed("move_right") ? 1 :
            Input.IsActionPressed("move_left") ? -1 : 0;

        character.Velocity = new Vector2(x * 200, character.Velocity.Y);

        // Transition when landing
        if (character.IsOnFloor())
            character.SwitchState(StateName.Idle);
    }
}