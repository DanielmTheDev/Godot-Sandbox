using Godot;

namespace Sandbox.Players;

public partial class Attacking : Node
{
    public void Attack1Hitbox()
    {
        GD.Print("hitbox spawned");
    }

    public void AreaEntered(Area2D area)
    {
        GD.Print("area entered");
    }
}