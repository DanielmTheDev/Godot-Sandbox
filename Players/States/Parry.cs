using Godot;
using Sandbox.Players.Scripts;

namespace Sandbox.Players.States;

public class Parry : State
{
    public override StateName StateName => StateName.Parrying;
    private readonly AnimationPlayer _animPlayer;
    private readonly MainCharacter _character;
    private readonly AudioStreamPlayer2D _streamSound;

    public Parry(AnimationPlayer animPlayer, MainCharacter character) : base(character)
    {
        _animPlayer = animPlayer;
        _character = character;
        _streamSound = character.GetNode<AudioStreamPlayer2D>("Sounds/Parry");
    }

    public override void Enter()
    {
        _animPlayer.AnimationFinished += OnAnimationFinished;
        _animPlayer.Play("parry");
    }

    public override void Exit() => _animPlayer.AnimationFinished -= OnAnimationFinished;

    public override void GetHit(Area2D area) => _streamSound.Play();

    private void OnAnimationFinished(StringName animName)
    {
        if (animName == "parry")
        {
            _character.SwitchState(StateName.Idle);
        }
    }
}