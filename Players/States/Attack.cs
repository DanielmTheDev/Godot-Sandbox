using Godot;

namespace Sandbox.Players.States;

public sealed class Attack : State
{
    private readonly AnimationPlayer _animPlayer;
    private readonly Scripts.MainCharacter _character;

    public Attack(AnimationPlayer animPlayer, Scripts.MainCharacter character)
    {
        _animPlayer = animPlayer;
        _character = character;
        _animPlayer.AnimationFinished += OnAnimationFinished;
    }

    public override void Enter(Scripts.MainCharacter character)
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