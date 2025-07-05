using Godot;
using Sandbox.Players.Scripts;

namespace Sandbox.Players.States;

public sealed class Attack : State
{
    public override StateName StateName => StateName.Attacking;
    private readonly AnimationPlayer _animPlayer;
    private readonly MainCharacter _character;

    public Attack(AnimationPlayer animPlayer, MainCharacter character) : base(character)
    {
        _animPlayer = animPlayer;
        _character = character;
    }


    protected override void OnEnter()
    {
        _animPlayer.AnimationFinished += OnAnimationFinished;
        _animPlayer.Play("attack1");
    }

    protected override void OnExit() => _animPlayer.AnimationFinished -= OnAnimationFinished;

    private void OnAnimationFinished(StringName animName)
    {
        if (animName == "attack1")
        {
            _character.SwitchState(StateName.Idle);
        }
    }
}