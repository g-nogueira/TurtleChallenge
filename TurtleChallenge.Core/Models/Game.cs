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

        #region Create Board pieces
        // One step of the Turtle should be the size of one cell + it's wall (CellSize + 4).
        var moveStep = new Vector2(board.CellSize.X + 1, board.CellSize.Y);

        var mines = _gameSettings.Mines.Select(position => new Mine(GetPosititionGivenCellSize(board, position)));
        var exit = new Exit(GetPosititionGivenCellSize(board, _gameSettings.ExitCoordinates));
        var turtle = new Turtle(GetPosititionGivenCellSize(board, _gameSettings.StartCoodinates), _gameSettings.StartDirection) { MoveStepSize = moveStep };
        #endregion

        #region Add each one to GameManager
        gameManager.AddObject(board);   // Create Board
        gameManager.AddObject(mines);   // Create Mines
        gameManager.AddObject(exit);    // Create Exit
        gameManager.AddObject(turtle);  // Create Turtle 
        #endregion

        #region Listen for Turtle position & collision changes
        // Start listening for events
        turtle.CollisionEnter += OnTurtleCollided;
        turtle.PositionChange += OnTurtleMoved;
        turtle.RotationChange += OnTurtleTurner;
        #endregion

        RedrawBoard(true);

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

    /// <summary>
    /// Calculates the initial 2D spatial position of a GameObject, given the Board cell sizes.
    /// </summary>
    /// <param name="board"></param>
    /// <param name="currentPosition">The initial position, not counting for Board cell sizes.</param>
    /// <returns></returns>
    private IVector2 GetPosititionGivenCellSize(Board board, IVector2 currentPosition)
    {
        var halfCellSize = board.CellSize / 2;
        var cellSize = board.CellSize;

        // Turtle Position * (CellSize + WallR) + WallL + HalfCell
        var x = currentPosition.X * (cellSize.X + 1) + 1 + halfCellSize.X;
        // Given the Board doesn't have horizontal separations, we don't need to keep that in mind, as seen above.
        // Turtle Position * CellSize + TopWall
        var y = currentPosition.Y * cellSize.Y + 1;

        return new Vector2(x, y);


    }

    /// <summary>
    /// Reacts for a change in the Turtle's orientation (not sexual, though. It is free to be what it wants).
    /// </summary>
    /// <param name="e"></param>
    private void OnTurtleTurner(GameObjectEvent e)
    {
        var board = gameManager.GameObjects.FirstOrDefault(obj => obj.Tag?.ToString() == ObjectTypes.Board.ToString());
        var turtle = e.GameObject;

        if (board == null || turtle == null) return;

        RedrawBoard(true);
    }

    /// <summary>
    /// Reacts for a change in the Turtle's 2D position.
    /// </summary>
    /// <param name="e"></param>
    private void OnTurtleMoved(GameObjectEvent e)
    {
        var board = gameManager.GameObjects.FirstOrDefault(obj => obj.Tag?.ToString() == ObjectTypes.Board.ToString());
        var turtle = e.GameObject;

        if (board == null || turtle == null) return;

        RedrawBoard(true);

        #region Get Board corners
        // Get board corner coordinates
        var cornerTL = new Vector2(0, board.Size.Y - 1);
        var cornerTR = new Vector2(board.Size.X - 1, board.Size.Y - 1);
        var cornerBL = new Vector2(0, 0);
        var cornerBR = new Vector2(board.Size.X - 1, 0);
        #endregion

        #region Get board walls
        // Get board limits
        var isNearWallT = turtle.Position.Y == cornerTL.Y;
        var isNearWallR = turtle.Position.X == cornerTR.X;
        var isNearWallB = turtle.Position.Y == cornerBL.Y;
        var isNearWallL = turtle.Position.X == cornerTL.X;

        var isOnWallT = turtle.Position.Y <= board.Position.Y;
        var isOnWallR = turtle.Position.X >= board.Position.X + board.Size.X * (((Board)board).CellSize.X + 1) + 1;
        var isOnWallB = turtle.Position.Y >= board.Position.Y + board.Size.Y * (((Board)board).CellSize.Y) + 1;
        var isOnWallL = turtle.Position.X <= board.Position.X;
        #endregion

        // Is the Turtle outside the walls?
        if (isOnWallT || isOnWallR || isOnWallB || isOnWallL)
        {
            LoseGame("Ops.. You hit the wall. You're dead.");
        }
        //// Is the Turtle near any wall?
        //if (isNearWallB || isNearWallT || isNearWallR || isNearWallL)
        //{
        //    HitWall("!Be careful with the wall!");
        //}
    }

    /// <summary>
    /// Reacts for the Turtle's collision with other GameObjects.
    /// </summary>
    /// <param name="e"></param>
    public void OnTurtleCollided(CollisionEvent e)
    {
        RedrawBoard(true);

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

    /// <summary>
    /// Clears and draws the whole CLI again, with each GameObject in it.
    /// </summary>
    /// <param name="sleep"></param>
    public void RedrawBoard(bool sleep = false)
    {
        //var board = gameManager.GameObjects.FirstOrDefault(obj => obj.Tag?.ToString() == ObjectTypes.Board.ToString());

        //if (board == null) return;

        //UIInstance.RenderBoard((Board)board, gameManager.GameObjects);
        //Thread.Sleep(1000);

        foreach (var obj in gameManager.GameObjects)
        {
            obj.Render(UIInstance);
        }

        if (sleep)
        {
            Thread.Sleep(1000);
        }
    }

    public void WinGame(string message = "You Win!")
    {
        UIInstance.InGameMessage(message);
        AskToRestart();
    }

    public void LoseGame(string message = "You Lose!")
    {
        UIInstance.InGameMessage(message);
        AskToRestart();

    }

    public void HitWall(string message = "Be careful!")
    {
        UIInstance.InGameMessage(message);
    }

    /// <summary>
    /// Asks if the user wants to start a new game.
    /// </summary>
    public void AskToRestart()
    {
        var parsed = bool.TryParse(UIInstance.Prompt("Do you want to restart? (true): ", breakLineBefore: true), out var restart);
        
        if ((parsed && restart || !parsed) && _matchMoves != null)
        {
            StartMatch(_matchMoves);
        }

    }


}
