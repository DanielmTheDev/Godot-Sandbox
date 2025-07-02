using System.Collections.Generic;
using System.Linq;
using Sandbox.Players.States;

namespace Sandbox.Players.Scripts;

public static class StateListExtensions
{
    public static State GetByName(this List<State> states, StateName name) => states.Single(state => state.StateName == name);
}