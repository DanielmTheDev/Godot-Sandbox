using System.Collections.Generic;
using Godot;
using Sandbox.Players.Controls;
using Sandbox.Players.States;

namespace Sandbox.Players.Scripts;

public partial class MainCharacter : CharacterBody2D
{
    [Export]
    public string ControlsPrefix = "p1_";
    [Export]
    public PackedScene Projectile = null!;

    private AnimationPlayer _animPlayer = null!;
    private InputProfile _controls = null!;

    private Dictionary<StateName, State> _states = null!;
    public State CurrentState = null!;
    private Node2D _visuals = null!;

    public override void _Ready()
    {
        _animPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
        _controls = new InputProfile(ControlsPrefix);
        _visuals = GetNode<Node2D>("Visuals");

        _states = new Dictionary<StateName, State>
        {
            { StateName.Idle, new Idle(_animPlayer, _controls) },
            { StateName.Running, new Run(_animPlayer, _visuals, _controls) },
            { StateName.Attacking, new Attack(_animPlayer, this) },
            { StateName.Jumping, new Jump(_animPlayer, _controls) },
            { StateName.Parrying, new Parry(_animPlayer, this) },
            { StateName.Dead, new Dead(_animPlayer) },
            { StateName.CastingProjectile, new CastingProjectile(Projectile, this) }
        };
        CurrentState = _states[StateName.Idle];
        SwitchState(StateName.Idle);
    }

    public void SwitchState(StateName name)
    {
        CurrentState.Exit(this);
        CurrentState = _states[name];
        CurrentState.Enter(this);
    }

    public override void _PhysicsProcess(double delta) => CurrentState.Update(this, delta);
}