using Godot;
using Sandbox.Players.Scripts;

namespace Sandbox.Players.States;

public class Dead : State
{
    public override StateName StateName => StateName.Dead;
    private readonly AnimationPlayer _animPlayer;

    public Dead(AnimationPlayer animPlayer) => _animPlayer = animPlayer;

    public override void Enter(MainCharacter character) => _animPlayer.Play("death");
}