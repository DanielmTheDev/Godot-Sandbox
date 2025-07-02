using Godot;
using Sandbox.Players.Scripts;

namespace Sandbox.Players.States;

public abstract class State
{
    public abstract StateName StateName { get; }
    public virtual void Enter(MainCharacter character) {}
    public virtual void Exit(MainCharacter character) {}
    public virtual void Update(MainCharacter character, double delta) {}
    public virtual void GetHit(MainCharacter character, Area2D area) => DefaultHitResponse.Handle(character, area);
}

public static class DefaultHitResponse
{
    public static void Handle(MainCharacter character, Area2D incoming)
    {
        if (incoming.IsInGroup(character.Player.ToString()))
        {
            return;
        }

        var bloodEmitter = character.GetNode<GpuParticles2D>("BloodEmitter");
        bloodEmitter.Restart();
        character.SwitchState(StateName.Dead);
    }
}