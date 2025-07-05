using System;
using Godot;
using Sandbox.Players.Scripts;

namespace Sandbox.Players.States;

public abstract class State
{
    protected readonly MainCharacter Character;
    public abstract StateName StateName { get; }

    public State(MainCharacter character) => Character = character;

    public virtual void GetHit(Area2D area) => DefaultHitResponse.Handle(Character, area);
    public event Action<State>? OnEntered;
    public event Action<State>? OnExited;
    public event Action<State>? OnUpdated;

    public void Enter()
    {
        OnEntered?.Invoke(this);
        OnEnter();
    }

    public void Exit()
    {
        OnExited?.Invoke(this);
        OnExit();
    }

    public void Update(double delta)
    {
        OnUpdated?.Invoke(this);
        OnUpdate(delta);
    }

    protected virtual void OnEnter()
    {
    }

    protected virtual void OnExit()
    {
    }

    protected virtual void OnUpdate(double delta)
    {
    }
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