using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurtleChallenge.Core.Interfaces;

namespace TurtleChallenge.Core.Models
{
    public class Board : GameObject
    {
        public Board(ICoordinate size) {
            Size = size;
            Tag = ObjectTypes.Board.ToString();
            IsVisible = false;
        }

        public Board()
        {
            Size = new Coordinate(Defaults.BoardSize);
            Tag = ObjectTypes.Board.ToString();
            IsVisible = false;

        }

        public override string UI()
        {
            return " ";
        }
    }
}
