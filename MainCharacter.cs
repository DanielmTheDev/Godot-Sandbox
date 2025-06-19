using Godot;

namespace Sandbox;

public partial class MainCharacter : CharacterBody2D
{
    private AnimationPlayer _animPlayer;
    private Sprite2D _sprite;
    private static bool _isAttacking;
    private const float Gravity = 1000f;

    private const float MoveSpeed = 200f;

    public override void _Ready()
    {
        _sprite = GetNode<Sprite2D>("Sprite2D");
        _animPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
        _animPlayer.AnimationFinished += OnAnimationFinished;
    }

    public override void _PhysicsProcess(double delta)
    {
        ProcessAttack();
        ProcessMovement(delta);
    }

    public void Attack1Hitbox()
    {
        GD.Print("hitbox spawned");
    }

    public void AreaEntered(Area2D area)
    {
        GD.Print("area entered");
    }

    private void OnAnimationFinished(StringName animName)
    {
        if (animName == "attack1")
        {
            _isAttacking = false;
            _animPlayer.Play("idle");
        }
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
        _sprite.FlipH = GetOrientation(x);
        SetMovementAnimation(x);
        Velocity = new Vector2(x, y);
        MoveAndSlide();
    }

    private void SetMovementAnimation(float x)
    {
        if (x != 0)
        {
            _animPlayer.Play("run");
        }

        if (x == 0 && !_isAttacking)
        {
            _animPlayer.Play("idle");
        }
    }

    private bool GetOrientation(float x)
        => x switch
        {
            > 0 => false,
            < 0 => true,
            _ => _sprite.FlipH
        };

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