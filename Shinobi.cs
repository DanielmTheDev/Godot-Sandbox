using System;
using Godot;

namespace Sandbox;

public partial class Shinobi : AnimatedSprite2D
{
    private const double AttackDuration = 1.2f;
    private double _currentAttackElapsed;

    [Export]
    public int Speed { get; set; } = 400; // How fast the player will move (pixels/sec).

    [Signal]
    public delegate void HitEventHandler();

    public Vector2 ScreenSize; // Size of the game window.

    // We also specified this function name in PascalCase in the editor's connection window.
    private void OnBodyEntered(Node2D body)
    {
        Hide(); // Player disappears after being hit.
        EmitSignal(SignalName.Hit);
        // Must be deferred as we can't change physics properties on a physics callback.
        GetNode<CollisionShape2D>("CollisionShape2D").SetDeferred(CollisionShape2D.PropertyName.Disabled, true);
    }

    public override void _Ready()
    {
        ScreenSize = GetViewportRect().Size;
        Hide();
        Start(new Vector2(ScreenSize.X / 2, ScreenSize.Y / 2));
    }

    public void Start(Vector2 position)
    {
        Position = position;
        Show();
        GetNode<CollisionShape2D>("CollisionShape2D").Disabled = false;
    }

    public override void _Process(double delta)
    {
        if (IsAttacking(delta))
        {
            _currentAttackElapsed += delta;
            Animation = "attack1";
        }
        else
        {
            _currentAttackElapsed = 0;
            ProcessMovement(delta);
        }
    }

    private bool IsAttacking(double delta)
        => Input.IsActionPressed("attack1") || IsCurrentlyInAttackAnimation(delta);

    private bool IsCurrentlyInAttackAnimation(double delta)
    {
        var animationStarted = _currentAttackElapsed > 0;
        var animationInProgress = _currentAttackElapsed + delta < AttackDuration;
        return animationStarted && animationInProgress;
    }

    private void ProcessMovement(double delta)
    {
        var velocity = Vector2.Zero; // The player's movement vector.

        if (Input.IsActionPressed("move_right"))
        {
            velocity.X += 1;
        }

        if (Input.IsActionPressed("move_left"))
        {
            velocity.X -= 1;
        }

        if (Input.IsActionPressed("move_down"))
        {
            velocity.Y += 1;
        }

        if (Input.IsActionPressed("move_up"))
        {
            velocity.Y -= 1;
        }


        if (velocity.X != 0)
        {
            Animation = "run";
            FlipH = velocity.X < 0;
        }
        else if (velocity.Y != 0)
        {
            Animation = "run";
        }

        if (velocity.Length() > 0)
        {
            velocity = velocity.Normalized() * Speed;
        }
        else
        {
            Animation = "idle";
        }
        Play();

        Position += velocity * (float)delta;
        Position = new Vector2(
            x: Mathf.Clamp(Position.X, 0, ScreenSize.X),
            y: Mathf.Clamp(Position.Y, 0, ScreenSize.Y)
        );
    }
}