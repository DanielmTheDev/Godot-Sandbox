using Godot;

namespace Sandbox.Players.Scripts;

public partial class TakingDamage : Node
{
    [Export] public MainCharacter MainCharacter = null!;

    public void GetHit(Area2D area) => MainCharacter.SwitchState(States.StateName.Dead);
}