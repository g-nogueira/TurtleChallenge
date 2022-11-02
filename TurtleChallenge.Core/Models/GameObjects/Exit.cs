using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurtleChallenge.Core.Interfaces;

namespace TurtleChallenge.Core.Models;
public class Exit : GameObject
{
    public Exit(IVector2 position)
    {
        Position = position;
        Tag = ObjectTypes.Exit.ToString();
        Color = ConsoleColor.Red;
    }

    public override string ToString()
    {
        return "♥";
    }
}
