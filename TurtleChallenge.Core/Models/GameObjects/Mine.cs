using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurtleChallenge.Core.Interfaces;

namespace TurtleChallenge.Core.Models;
public class Mine : GameObject
{
    public Mine(IVector2 position)
    {
        Position = position;
        Tag = ObjectTypes.Mine.ToString();
        Color = ConsoleColor.DarkYellow;
    }

    public override string ToString()
    {
        return "Δ";
    }
}
