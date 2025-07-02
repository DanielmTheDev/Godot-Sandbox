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

    private List<State> _states = null!;
    public State CurrentState = null!;
    private Node2D _visuals = null!;

    public override void _Ready()
    {
        _animPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
        _controls = new InputProfile(ControlsPrefix);
        _visuals = GetNode<Node2D>("Visuals");

        _states =
        [
            new Idle(_animPlayer, _controls),
            new Run(_animPlayer, _visuals, _controls),
            new Attack(_animPlayer, this),
            new Jump(_animPlayer, _controls),
            new Parry(_animPlayer, this),
            new Dead(_animPlayer),
            new CastingProjectile(Projectile, this)
        ];
        CurrentState = _states.GetByName(StateName.Idle);
        SwitchState(StateName.Idle);
    }

    public void SwitchState(StateName name)
    {
        CurrentState.Exit(this);
        CurrentState = _states.GetByName(name);
        CurrentState.Enter(this);
    }

    public override void _PhysicsProcess(double delta) => CurrentState.Update(this, delta);
}