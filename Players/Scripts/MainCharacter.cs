using System.Collections.Generic;
using Godot;
using Sandbox.Players.Controls;
using Sandbox.Players.States;

namespace Sandbox.Players.Scripts;

public partial class MainCharacter : CharacterBody2D
{
    [Export]
    public PackedScene Projectile = null!;
    [Export]
    public Player Player;

    private AnimationPlayer _animPlayer = null!;
    private InputProfile _controls = null!;

    private List<State> _states = null!;
    public State CurrentState = null!;
    private Node2D _visuals = null!;

    public override void _Ready()
    {
        _animPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
        _controls = new InputProfile(Player);
        _visuals = GetNode<Node2D>("Visuals");

        _states =
        [
            new Idle(_animPlayer, _controls, this),
            new Run(_animPlayer, _visuals, _controls, this),
            new Attack(_animPlayer, this), // subcribe to event here and emit signal here?
            new Jump(_animPlayer, _controls, this),
            new Parry(_animPlayer, this),
            new Dead(_animPlayer, this),
            new CastingProjectile(Projectile, this)
        ];
        CurrentState = _states.GetByName(StateName.Idle);
        SwitchState(StateName.Idle);
    }

    public void SwitchState(StateName name)
    {
        CurrentState.Exit();
        CurrentState = _states.GetByName(name);
        CurrentState.Enter();
    }

    public override void _PhysicsProcess(double delta) => CurrentState.Update(delta);
}