using Godot;

namespace Sandbox;

public partial class Main : Node
{
    // Don't forget to rebuild the project so the editor knows about the new export variable.

    [Export] public PackedScene MobScene { get; set; }

    private int _score;

    // public override void _Ready()
    // {
    //     NewGame();
    // }

    public void GameOver()
    {
        GetNode<AudioStreamPlayer>("Music").Stop();
        GetNode<AudioStreamPlayer>("DeathSound").Play();
        GetNode<Timer>("MobTimer").Stop();
        GetNode<Timer>("ScoreTimer").Stop();
        GetNode<Hud>("HUD").ShowGameOver();
    }

    public void NewGame()
    {
        GetNode<AudioStreamPlayer>("Music").Play();
        GetTree().CallGroup("mobs", Node.MethodName.QueueFree);
        _score = 0;

        var player = GetNode<Player>("Player");
        var startPosition = GetNode<Marker2D>("StartPosition");
        player.Start(startPosition.Position);

        GetNode<Timer>("StartTimer").Start();
        var hud = GetNode<Hud>("HUD");
        hud.UpdateScore(_score);
        hud.ShowMessage("Get Ready!");
    }

    private void OnScoreTimerTimeout()
    {
        _score++;
        GetNode<Hud>("HUD").UpdateScore(_score);
    }

    private void OnStartTimerTimeout()
    {
        GetNode<Timer>("MobTimer").Start();
        GetNode<Timer>("ScoreTimer").Start();
    }

    private void OnMobTimerTimeout()
    {
        var mob = MobScene.Instantiate<Mob>();

        var mobSpawnLocation = GetNode<PathFollow2D>("MobPath/MobSpawnLocation");
        mobSpawnLocation.ProgressRatio = GD.Randf();

        var direction = mobSpawnLocation.Rotation + Mathf.Pi / 2;

        mob.Position = mobSpawnLocation.Position;

        direction += (float)GD.RandRange(-Mathf.Pi / 4, Mathf.Pi / 4);
        mob.Rotation = direction;

        var velocity = new Vector2((float)GD.RandRange(150.0, 250.0), 0);
        mob.LinearVelocity = velocity.Rotated(direction);

        AddChild(mob);
    }
}