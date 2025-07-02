using Godot;
using Sandbox.Players.Scripts;

namespace Sandbox.Players.States;

public abstract class State
{
    protected readonly MainCharacter Character;

    public State(MainCharacter character) => Character = character;

    public abstract StateName StateName { get; }
    public virtual void Enter() {}
    public virtual void Exit() {}
    public virtual void Update(double delta) {}
    public virtual void GetHit(Area2D area) => DefaultHitResponse.Handle(Character, area);
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