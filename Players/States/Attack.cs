using Godot;
using Sandbox.Players.Scripts;

namespace Sandbox.Players.States;

public sealed class Attack : State
{
    private readonly AnimationPlayer _animPlayer;
    private readonly MainCharacter _character;

    public Attack(AnimationPlayer animPlayer, MainCharacter character)
    {
        _animPlayer = animPlayer;
        _character = character;
        _animPlayer.AnimationFinished += OnAnimationFinished;
    }

    public override void Enter(MainCharacter character) => _animPlayer.Play("attack1");

    private void OnAnimationFinished(StringName animName)
    {
        if (animName == "attack1")
        {
            _character.SwitchState(StateName.Idle);
        }
    }
}