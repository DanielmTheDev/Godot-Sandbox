using Godot;

namespace Sandbox.Players;

public sealed class AttackState : State
{
    private readonly AnimationPlayer _animPlayer;
    private readonly MainCharacter _character;

    public AttackState(AnimationPlayer animPlayer, MainCharacter character)
    {
        _animPlayer = animPlayer;
        _character = character;
        _animPlayer.AnimationFinished += OnAnimationFinished;
    }

    public override void Enter(MainCharacter character)
    {
        GD.Print("attacking");
        _animPlayer.Play("attack1");
    }

    private void OnAnimationFinished(StringName animName)
    {
        if (animName == "attack1")
        {
            _character.SwitchState(StateName.Idle);
        }
    }
}