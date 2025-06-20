namespace Sandbox.Players.States;

public abstract class State
{
    public virtual void Enter(Scripts.MainCharacter character) {}
    public virtual void Exit(Scripts.MainCharacter character) {}
    public virtual void Update(Scripts.MainCharacter character, double delta) {}
}