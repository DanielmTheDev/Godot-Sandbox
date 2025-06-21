using System.Collections.Generic;
using Godot;
using Sandbox.Players.Controls;
using Sandbox.Players.States;

namespace Sandbox.Players.Scripts;

public partial class MainCharacter : CharacterBody2D
{
    [Export]
    public string ControlsPrefix = "p1_";

    private AnimationPlayer _animPlayer = null!;
    private Sprite2D _sprite = null!;
    private InputProfile _controls = null!;

    private Dictionary<StateName, State> _states = null!;
    private State _currentState = null!;

    public override void _Ready()
    {
        _sprite = GetNode<Sprite2D>("Sprite2D");
        _animPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
        _controls = new InputProfile(ControlsPrefix);

        _states = new Dictionary<StateName, State>
        {
            { StateName.Idle, new Idle(_animPlayer, _controls) },
            { StateName.Running, new Run(_animPlayer, _sprite, _controls) },
            { StateName.Attacking, new Attack(_animPlayer, this) },
            { StateName.Jumping, new Jump(_animPlayer, _controls) }
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