using Godot;

namespace Sandbox;

public partial class MainCharacter : CharacterBody2D
{
    private AnimationPlayer _animPlayer;
    private static bool _isAttacking;
    private const float Gravity = 1000f;

    private const float MoveSpeed = 200f;

    public override void _Ready()
    {
        _animPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
        _animPlayer.AnimationFinished += OnAnimationFinished;
    }

    private void OnAnimationFinished(StringName animName)
    {
        if (animName == "attack1")
        {
            _isAttacking = false;
            _animPlayer.Play("idle");
        }
    }

    public override void _PhysicsProcess(double delta)
    {
        ProcessAttack();
        ProcessMovement(delta);
    }

    private void ProcessAttack()
    {
        if (Input.IsActionJustPressed("attack1") && IsOnFloor())
        {
            _isAttacking = true;
            _animPlayer.Play("attack1");
        }
    }

    private void ProcessMovement(double delta)
    {
        var x = GetXMovement();
        var y = GetYMovement(delta);
        Velocity = new Vector2(x, y);
        MoveAndSlide();
    }

    private float GetYMovement(double delta)
        => Velocity.Y + (!IsOnFloor()
            ? Gravity * (float)delta
            : 0);

    private static float GetXMovement()
    {
        return _isAttacking
            ? 0
            : CalculateWalking();
    }

    private static float CalculateWalking()
    {
        var raw = Input.IsActionPressed("move_right")
            ? 1
            : Input.IsActionPressed("move_left")
                ? -1
                : 0;
        return raw * MoveSpeed;
    }
}