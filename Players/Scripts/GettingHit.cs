using Godot;

namespace Sandbox.Players.Scripts;

public partial class GettingHit : Node
{
    [Export] public MainCharacter MainCharacter = null!;

    public void GetHit(Area2D area) => MainCharacter.CurrentState.GetHit(MainCharacter, area);
}