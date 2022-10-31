using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using TurtleChallenge.Core.Interfaces;

namespace TurtleChallenge.Core.Models
{
    public class Game
    {
        private GameManager gameManager = new();
        public readonly UI UIInstance = UI.GetInstance();
        private IGameSettings _gameSettings;
        private MatchMoves _matchMoves;
        public Game() { }

        public void StartMatch(IGameSettings gameSettings, MatchMoves matchMoves)
        {
            gameManager.Reset();
            _gameSettings = gameSettings;
            _matchMoves = matchMoves;

            var board = new Board(gameSettings.BoardSize);
            var mines = gameSettings.Mines.Select(position => new Mine(position));
            var exit = new Exit(gameSettings.ExitPosition);
            var turtle = new Turtle(gameSettings.StartPosition, gameSettings.StartDirection);

            // Create Board
            gameManager.AddObject(board);
            // Create Mines
            gameManager.AddObject(mines);
            // Create Exit
            gameManager.AddObject(exit);
            // Create Turtle
            gameManager.AddObject(turtle);

            RedrawBoard();

            // Start listening for events
            turtle.CollisionEnter += OnTurtleCollided;
            turtle.PositionChange += OnTurtleMoved;
            turtle.RotationChange += OnTurtleTurner;

            // Start moving the Turtle
            foreach (var move in matchMoves.Moves)
            {
                switch (move)
                {
                    case MoveType.R:
                        turtle.Rotate();
                        break;
                    case MoveType.M:
                        turtle.Move();
                        break;
                    default:
                        break;
                }
            }
        }

        private void OnTurtleTurner(GameObjectEvent e)
        {
            var board = gameManager.GameObjects.FirstOrDefault(obj => obj.Tag?.ToString() == ObjectTypes.Board.ToString());
            var turtle = e.GameObject;

            if (board == null || turtle == null) return;

            RedrawBoard();
        }

        private void OnTurtleMoved(GameObjectEvent e)
        {
            var board = gameManager.GameObjects.FirstOrDefault(obj => obj.Tag?.ToString() == ObjectTypes.Board.ToString());
            var turtle = e.GameObject;

            if (board == null || turtle == null) return;

            // Get board corner coordinates
            var cornerTL = new Coordinate(0, board.Size.Y - 1);
            var cornerTR = new Coordinate(board.Size.X - 1, board.Size.Y - 1);
            var cornerBL = new Coordinate(0,0);
            var cornerBR = new Coordinate(board.Size.X - 1, 0);

            // Get board limits
            var isWallT = turtle.Position.Y == cornerTL.Y;
            var isWallR = turtle.Position.X == cornerTR.X;
            var isWallB = turtle.Position.Y == cornerBL.Y;
            var isWallL = turtle.Position.X == cornerTL.X;

            RedrawBoard();

            // Is the Turtle outside the walls?
            if (false)
            {
                LoseGame("You fell! You're dead!");
            }
            // Is the Turtle near any wall?
            if (isWallB || isWallT || isWallR || isWallL)
            {
                HitWall();
            }
        }

        public void OnTurtleCollided(CollisionEvent e)
        {
            if (!Enum.TryParse<ObjectTypes>(e.Target.Tag, true, out var objectType))
                return;

            switch (objectType)
            {
                case ObjectTypes.Default:
                    break;
                case ObjectTypes.Mine:
                    LoseGame("You Lose!");
                    break;
                case ObjectTypes.Exit:
                    WinGame("You Won!");
                    break;
                case ObjectTypes.Board:
                    break;
                default:
                    break;
            }
        }

        public void RedrawBoard()
        {
            var board = gameManager.GameObjects.FirstOrDefault(obj => obj.Tag?.ToString() == ObjectTypes.Board.ToString());

            if (board == null) return;

            UIInstance.DisplayBoard((Board)board, gameManager.GameObjects);
            Thread.Sleep(1000);
        }

        public void WinGame(string message = "You Win!")
        {
            UIInstance.GameMessage(message);
            AskToRestart();
        }

        public void LoseGame(string message = "You Lose!")
        {
            UIInstance.GameMessage(message);
            AskToRestart();

        }

        public void HitWall(string message = "You will fall if you go one step the wrong way!")
        {
            UIInstance.GameMessage(message);
        }

        public void AskToRestart()
        {
            bool.TryParse(UIInstance.Prompt("Do you want to restart? (false): ", false), out var restart);

            if (restart && _gameSettings != null && _matchMoves != null) {
                StartMatch(_gameSettings, _matchMoves);
            }

        }

        public static Game GetInstance()
        {
            return new Game();
        }

    }
}
