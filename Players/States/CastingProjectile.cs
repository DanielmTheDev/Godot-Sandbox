using Godot;
using Sandbox.Players.Scripts;

namespace Sandbox.Players.States;

public class CastingProjectile : State
{
    public override StateName StateName => StateName.CastingProjectile;
    private readonly PackedScene _projectile;
    private readonly MainCharacter _character;
    private readonly AudioStreamPlayer2D _incantationPlayer;
    private readonly Marker2D _spawnPoint;

    public CastingProjectile(PackedScene projectile, MainCharacter character) : base(character)
    {
        _projectile = projectile;
        _character = character;
        _incantationPlayer = character.GetNode<AudioStreamPlayer2D>("Sounds/Incantation");
        _spawnPoint = character.GetNode<Marker2D>("Visuals/ProjectileSpawnPoint");
    }

    protected override void OnEnter()
    {
        var instance = Cast(_character);
        instance.OnLaunched += OnLaunched;
    }

    private Projectile Cast(MainCharacter character)
    {
        _incantationPlayer.Play();
        var instance = _projectile.Instantiate<Projectile>();
        instance.Position = _spawnPoint.GlobalPosition;
        instance.Direction = character.GetNode<Node2D>("Visuals").Scale.X < 0 ? Vector2.Left : Vector2.Right;
        instance.AddToGroup(character.Player.ToString());
        character.GetTree().CurrentScene.AddChild(instance);
        return instance;
    }

    private void OnLaunched()
    {
        _incantationPlayer.Stop();
        if (_character.CurrentState.StateName != StateName.Dead)
        {
            _character.SwitchState(StateName.Idle);
        }
    }
}