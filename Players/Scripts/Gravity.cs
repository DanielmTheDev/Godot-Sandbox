using Godot;

namespace Sandbox.Players.Scripts;

public partial class Gravity : Node
{
	[Export] public MainCharacter MainCharacter = null!;

	private const float GravityForce = 1000f;

	public override void _PhysicsProcess(double delta)
	{
		var force = MainCharacter.Velocity.Y + GravityForce * (float)delta;
		MainCharacter.Velocity = new Vector2(MainCharacter.Velocity.X, force);
		MainCharacter.MoveAndSlide();
	}
}
