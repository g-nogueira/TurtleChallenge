using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurtleChallenge.Core.Interfaces;

namespace TurtleChallenge.Core.Models;
public class Exit : GameObject
{
    public Exit(ICoordinate position)
    {
        Position = position;
        Tag = ObjectTypes.Exit.ToString();
    }

    public override string UI()
    {
        return "♥";
    }
}
