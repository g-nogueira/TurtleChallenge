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
        public Vector2 CellSize { get; } = new Vector2(4,1);
        /// <summary>
        /// The offset for each cell, given the size of the top and left walls.
        /// </summary>
        public Vector2 CellOffset { get; } = new Vector2(1,1);

        public Board(IVector2 size) {
            Size = size;
            Tag = ObjectTypes.Board.ToString();
            IsVisible = false;
        }

        public Board()
        {
            Size = new Vector2(Defaults.BoardSize);
            Tag = ObjectTypes.Board.ToString();
            IsVisible = false;

        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            AddTopWall(sb);
            AddCells(sb);
            AddBottomWall(sb);

            return sb.ToString();
        }

        private void AddCells(StringBuilder sb)
        {
            for (int y = 0; y < Size.Y; y++)
            {
                sb.Append("│");
                for (int x = 0; x < Size.X; x++)
                {
                    sb.Append(new string(' ', CellSize.X) + "│");
                }
                sb.AppendLine();
            }
        }

        private void AddBottomWall(StringBuilder sb)
        {
            var bottomWallCell = new string('─', CellSize.X);

            sb.Append("└");
            for (int x = 0; x < Size.X; x++)
            {
                sb.Append(bottomWallCell + (x + 1 == Size.X ? "" : "┴"));
            }
            sb.AppendLine("┘");
        }

        private void AddTopWall(StringBuilder sb)
        {
            var topWallCell = new string('─', CellSize.X);

            sb.Append("┌");
            for (int x = 0; x < Size.X; x++)
            {
                sb.Append(topWallCell + (x + 1 == Size.X ? "" : "┬"));
            }
            sb.AppendLine("┐");
        }
    }
}
