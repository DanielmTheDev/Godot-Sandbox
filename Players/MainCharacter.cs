using System.Collections.Generic;
using Godot;

namespace Sandbox.Players;

public enum StateName
{
    Idle,
    Attacking,
    Running
}

public partial class MainCharacter : CharacterBody2D
{
    private AnimationPlayer _animPlayer = null!;
    private Sprite2D _sprite = null!;

    private Dictionary<StateName, State> _states = null!;
    private State _currentState = null!;

    public override void _Ready()
    {
        _sprite = GetNode<Sprite2D>("Sprite2D");
        _animPlayer = GetNode<AnimationPlayer>("AnimationPlayer");

        _states = new Dictionary<StateName, State>
        {
            { StateName.Idle, new IdleState(_animPlayer) },
            { StateName.Running, new RunningState(_animPlayer, _sprite) },
            { StateName.Attacking, new AttackState(_animPlayer, this) }
        };

        SwitchState(StateName.Idle);
    }

    public void SwitchState(StateName name)
    {
        _currentState?.Exit(this);
        _currentState = _states[name];
        _currentState.Enter(this);
    }

    public override void _PhysicsProcess(double delta) => _currentState.Update(this, delta);
}