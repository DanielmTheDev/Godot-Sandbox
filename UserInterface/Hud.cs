using Godot;

namespace Sandbox.UserInterface;

public partial class Hud : CanvasLayer
{
    public void AdjustPlayer1Stamina(int currentStamina, int maxStamina)
    {
        var progressBar = GetNode<ProgressBar>("StaminaPlayer1");
        progressBar.SetMax(maxStamina);
        progressBar.SetValue(currentStamina);
    }

    public void AdjustPlayer2Stamina(int currentStamina, int maxStamina)
    {
        var progressBar = GetNode<ProgressBar>("StaminaPlayer2");
        progressBar.SetMax(maxStamina);
        progressBar.SetValue(currentStamina);
    }
}