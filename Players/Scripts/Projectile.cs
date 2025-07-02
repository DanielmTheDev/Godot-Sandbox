using System;
using Godot;

namespace Sandbox.Players.Scripts;

public partial class Projectile : Area2D
{
    [Export] public float ChargeTime = 1f;
    [Export] public float Speed = 200f;
    [Export] public Vector2 Direction = Vector2.Right;

    public event Action? OnLaunched;

    private float _timeElapsed;
    private bool _launched;
    private AudioStreamPlayer2D _launchPlayer = null!;

    public override void _Ready()
    {
        Scale = Vector2.Zero;
        _launchPlayer = GetNode<AudioStreamPlayer2D>("AudioStreamPlayer2D");
        GetNode<Timer>("Timer").Timeout += QueueFree;
    }

    public override void _PhysicsProcess(double delta)
    {
        if (_launched)
        {
            Move(delta);
        }
        else
        {
            Grow(delta);

            var timeToLaunch = _timeElapsed >= ChargeTime;
            if (timeToLaunch)
            {
                Launch();
            }
        }
    }

    private void Launch()
    {
        _launched = true;
        _launchPlayer.Play();
        OnLaunched?.Invoke();
    }

    private void Grow(double delta)
    {
        _timeElapsed += (float)delta;
        Scale = Vector2.One * Mathf.Clamp(_timeElapsed / ChargeTime, 0, 1);
    }

    private void Move(double delta) => Position += Direction * Speed * (float)delta;
}
