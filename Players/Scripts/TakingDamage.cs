using Godot;

namespace Sandbox.Players.Scripts;

public partial class TakingDamage : Node
{
    [Export] public MainCharacter MainCharacter = null!;
    private GpuParticles2D _bloodEmitter = null!;

    public override void _Ready() => _bloodEmitter = GetNode<GpuParticles2D>("../BloodEmitter");

    public void GetHit(Area2D area)
    {
        MainCharacter.SwitchState(States.StateName.Dead);
        _bloodEmitter.Restart();
    }
}