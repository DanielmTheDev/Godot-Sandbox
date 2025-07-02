using Godot;
using Sandbox.Players.Scripts;

namespace Sandbox.Players.States;

public class CastingProjectile : State
{
    private readonly PackedScene _projectile;
    private readonly MainCharacter _mainCharacter;
    private readonly AudioStreamPlayer2D _incantationPlayer;
    private readonly Marker2D _spawnPoint;

    public CastingProjectile(PackedScene projectile, MainCharacter mainCharacter)
    {
        _projectile = projectile;
        _mainCharacter = mainCharacter;
        _incantationPlayer = mainCharacter.GetNode<AudioStreamPlayer2D>("Sounds/Incantation");
        _spawnPoint = mainCharacter.GetNode<Marker2D>("Visuals/ProjectileSpawnPoint");
    }

    public override void Enter(MainCharacter character)
    {
        _incantationPlayer.Play();
        var instance = _projectile.Instantiate<Projectile>();
        instance.Position = _spawnPoint.GlobalPosition;
        instance.Direction = character.GetNode<Node2D>("Visuals").Scale.X < 0 ? Vector2.Left : Vector2.Right;
        character.GetTree().CurrentScene.AddChild(instance);
        instance.OnLaunched += OnLaunched;
    }

    private void OnLaunched()
    {
        _incantationPlayer.Stop();
        _mainCharacter.SwitchState(StateName.Idle);
    }
}