using TurtleChallenge.Core.Interfaces;

namespace TurtleChallenge.Core.Models;
public class GameSettings : LoadableFromFile<GameSettings>, IGameSettings
{
    private ICoordinate startPosition = new Coordinate(0, 0);
    private ICoordinate exitPosition = new Coordinate(1, 1);
    private ICoordinate boardSize = new Coordinate(5, 5);

    public ICoordinate StartPosition { get => startPosition.Clone(); }
    public ICoordinate ExitPosition { get => exitPosition.Clone(); }
    public ICoordinate BoardSize { get => boardSize.Clone(); }
    public Direction StartDirection { get; } = Direction.North;
    public ICollection<ICoordinate> Mines { get; } = new List<ICoordinate>();
    public GameSettings(string fileLocation)
    {
        FileLocation = fileLocation;

        var MinesXY = ReadIniValue("Game Coordinates", "MinesXY").Split(' ');

        Mines = MinesXY.Select(coord => (ICoordinate)new Coordinate(coord)).ToList();
        startPosition = new Coordinate($"{ReadIniValue("Game Coordinates", "StartPositionX")}, {ReadIniValue("Game Coordinates", "StartPositionY")}");
        exitPosition = new Coordinate($"{ReadIniValue("Game Coordinates", "ExitX")}, {ReadIniValue("Game Coordinates", "ExitY")}");
        StartDirection = Enum.Parse<Direction>(ReadIniValue("Game Coordinates", "StartDir"), ignoreCase: true);
        boardSize = new Coordinate($"{ReadIniValue("Board", "BoardX")}, {ReadIniValue("Board", "BoardY")}");
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
