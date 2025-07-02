namespace Sandbox.Players;

public enum Player
{
    Unknown,
    One,
    Two
}

public static class PlayerExtensions
{
    public static string ControlsPrefix(this Player player)
    {
        return player switch
        {
            Player.One => "p1_",
            Player.Two => "p2_",
            _ => string.Empty
        };
    }
}