namespace Sandbox.Players;

public abstract class State
{
    public virtual void Enter(MainCharacter character) {}
    public virtual void Exit(MainCharacter character) {}
    public virtual void Update(MainCharacter character, double delta) {}
}