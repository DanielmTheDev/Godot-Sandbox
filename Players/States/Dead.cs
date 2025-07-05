using Godot;
using Sandbox.Players.Scripts;

namespace Sandbox.Players.States;

public class Dead : State
{
    public override StateName StateName => StateName.Dead;
    private readonly AnimationPlayer _animPlayer;

    public Dead(AnimationPlayer animPlayer, MainCharacter character) : base(character) => _animPlayer = animPlayer;

    protected override void OnEnter() => _animPlayer.Play("death");
}