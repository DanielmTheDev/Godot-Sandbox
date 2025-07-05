using System;

namespace Sandbox.Players;

internal class Stats
{
    public Stat Stamina { get; set; } = new();
}

public class Stat
{
    public event Action<Stat>? Changed;
    public int Max { get; set; } = 100;
    public int Current { get; private set; } = 100;

    public bool TrySpend(int amount)
    {
        if (Current < amount)
        {
            return false;
        }

        Current -= amount;
        Changed?.Invoke(this);
        return true;
    }
}