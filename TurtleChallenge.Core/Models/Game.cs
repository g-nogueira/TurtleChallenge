using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using TurtleChallenge.Core.Interfaces;

namespace TurtleChallenge.Core.Models;
public class Game
{
    public readonly GameManager gameManager = new();
    public readonly UI UIInstance = UI.GetInstance();
    private readonly IGameSettings _gameSettings;
    private MatchMoves? _matchMoves;

    public Game(IGameSettings gameSettings) => _gameSettings = gameSettings;


    public static Game GetInstance(IGameSettings gameSettings)
    {
        return new Game(gameSettings);
    }

    public void StartMatch(MatchMoves matchMoves)
    {
        gameManager.Reset();
        _matchMoves = matchMoves;

        var board = new Board(_gameSettings.BoardSize);
        var mines = _gameSettings.Mines.Select(position => new Mine(position));
        var exit = new Exit(_gameSettings.ExitPosition);
        var turtle = new Turtle(_gameSettings.StartPosition, _gameSettings.StartDirection);

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
        var cornerTL = new Vector2(0, board.Size.Y - 1);
        var cornerTR = new Vector2(board.Size.X - 1, board.Size.Y - 1);
        var cornerBL = new Vector2(0, 0);
        var cornerBR = new Vector2(board.Size.X - 1, 0);

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

        UIInstance.RenderBoard((Board)board, gameManager.GameObjects);
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

    public void HitWall(string message = "Be careful!")
    {
        UIInstance.GameMessage(message);
    }

    public void AskToRestart()
    {
        var parsed = bool.TryParse(UIInstance.Prompt("Do you want to restart? (true): ", breakLineBefore: true), out var restart);
        
        if ((parsed && restart || !parsed) && _matchMoves != null)
        {
            StartMatch(_matchMoves);
        }

    }


}
