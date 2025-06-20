using Godot;
using Sandbox.Players.Controls;

namespace Sandbox.Players.States;

public sealed class Jump : State
{
    private const float JumpVelocity = -400f;
    private readonly AnimationPlayer _animPlayer;
    private const float MoveSpeed = 200f;

    public Jump(AnimationPlayer animPlayer)
    {
        _animPlayer = animPlayer;
    }

    public override void Enter(Scripts.MainCharacter character)
    {
        character.Velocity = new Vector2(character.Velocity.X, JumpVelocity);
        _animPlayer.Play("up");
    }

    public override void Update(Scripts.MainCharacter character, double delta)
    {
        if (character.IsOnFloor())
        {
            character.SwitchState(StateName.Idle);
        }

        var x = Movement.GetX();
        if (character.Velocity.Y > 0)
        {
            _animPlayer.Play("down");
        }

        character.Velocity = new Vector2(x * MoveSpeed, character.Velocity.Y);
    }
}