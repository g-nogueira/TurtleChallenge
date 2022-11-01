using TurtleChallenge.Core.Interfaces;

namespace TurtleChallenge.Core.Models;
public class GameSettings : LoadableFromFile<GameSettings>, IGameSettings
{
    private IVector2 startPosition = new Vector2(0, 0);
    private IVector2 exitPosition = new Vector2(1, 1);
    private IVector2 boardSize = new Vector2(5, 5);

    public IVector2 StartPosition { get => startPosition.Clone(); }
    public IVector2 ExitPosition { get => exitPosition.Clone(); }
    public IVector2 BoardSize { get => boardSize.Clone(); }
    public Direction StartDirection { get; } = Direction.North;
    public ICollection<IVector2> Mines { get; } = new List<IVector2>();
    public GameSettings(string fileLocation)
    {
        FileLocation = fileLocation;

        var MinesXY = ReadIniValue("Game Coordinates", "MinesXY").Split(' ');

        Mines = MinesXY.Select(coord => (IVector2)new Coordinate(coord)).ToList();
        startPosition = new Vector2($"{ReadIniValue("Game Coordinates", "StartPositionX")}, {ReadIniValue("Game Coordinates", "StartPositionY")}");
        exitPosition = new Vector2($"{ReadIniValue("Game Coordinates", "ExitX")}, {ReadIniValue("Game Coordinates", "ExitY")}");
        StartDirection = Enum.Parse<Direction>(ReadIniValue("Game Coordinates", "StartDir"), ignoreCase: true);
        boardSize = new Vector2($"{ReadIniValue("Board", "BoardX")}, {ReadIniValue("Board", "BoardY")}");
    }

    /// <summary>
    /// Returns an Instance of <see cref="GameSettings"/> given a settings file.
    /// </summary>
    /// <param name="fileLocation"></param>
    /// <returns></returns>
    public static GameSettings GetInstance(string fileLocation)
    {
        var instance = new GameSettings(fileLocation ?? Defaults.GameSettingsLocation);

        return instance;
    }
}
