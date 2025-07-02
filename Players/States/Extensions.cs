using System.Collections.Generic;
using System.Linq;

namespace Sandbox.Players.States;

public static class Extensions
{
    public static State GetByName(this List<State> states, StateName name) => states.Single(state => state.StateName == name);
}