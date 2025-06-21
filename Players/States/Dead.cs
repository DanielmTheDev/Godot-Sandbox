using Godot;
using Sandbox.Players.Scripts;

namespace Sandbox.Players.States;

public class Dead : State
{
    private readonly AnimationPlayer _animPlayer;

    public Dead(AnimationPlayer animPlayer) => _animPlayer = animPlayer;

    public override void Enter(MainCharacter character) => _animPlayer.Play("death");
}