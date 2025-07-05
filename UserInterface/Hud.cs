using Godot;
using Sandbox.Players.Scripts;

namespace Sandbox.UserInterface;

public partial class Hud : CanvasLayer
{
    private MainCharacter _playerOne = null!;
    private MainCharacter _playerTwo = null!;

    public override void _Ready()
    {
        _playerOne = GetNode<MainCharacter>("../MainCharacter");
        _playerTwo = GetNode<MainCharacter>("../MainCharacter2");
        _playerOne.Connect("StaminaChanged", Callable.From<int, int>(AdjustPlayer1Stamina));
        _playerTwo.Connect("StaminaChanged", Callable.From<int, int>(AdjustPlayer2Stamina));
    }

    private void AdjustPlayer1Stamina(int currentStamina, int maxStamina)
    {
        var progressBar = GetNode<ProgressBar>("StaminaPlayer1");
        progressBar.SetMax(maxStamina);
        progressBar.SetValue(currentStamina);
    }

    private void AdjustPlayer2Stamina(int currentStamina, int maxStamina)
    {
        var progressBar = GetNode<ProgressBar>("StaminaPlayer2");
        progressBar.SetMax(maxStamina);
        progressBar.SetValue(currentStamina);
    }
}