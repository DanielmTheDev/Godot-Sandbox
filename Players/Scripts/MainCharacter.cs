using System.Collections.Generic;
using Godot;
using Sandbox.Players.Controls;
using Sandbox.Players.States;

namespace Sandbox.Players.Scripts;

public partial class MainCharacter : CharacterBody2D
{
    [Signal]
    public delegate void PerformedAttackEventHandler(int currentStamina, int maxStamina);
    [Export]
    public PackedScene Projectile = null!;
    [Export]
    public Player Player;

    public State CurrentState = null!;

    private AnimationPlayer _animPlayer = null!;
    private InputProfile _controls = null!;
    private List<State> _states = null!;
    private Node2D _visuals = null!;
    private readonly Stats _stats = new();

    public override void _Ready()
    {
        _animPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
        _controls = new InputProfile(Player);
        _visuals = GetNode<Node2D>("Visuals");

        _states =
        [
            new Idle(_animPlayer, _controls, this),
            new Run(_animPlayer, _visuals, _controls, this),
            new Attack(_animPlayer, this),
            new Jump(_animPlayer, _controls, this),
            new Parry(_animPlayer, this),
            new Dead(_animPlayer, this),
            new CastingProjectile(Projectile, this)
        ];
        HookUpSignals();
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

    private void HookUpSignals()
    {
        _states.GetByName(StateName.Attacking).OnEntered += AdjustStamina;
    }

    private void AdjustStamina()
    {
        _stats.CurrentStamina -= 30;
        EmitSignal(SignalName.PerformedAttack, _stats.CurrentStamina, _stats.MaxStamina);
    }
}