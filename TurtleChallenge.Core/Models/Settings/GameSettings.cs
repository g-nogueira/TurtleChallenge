using TurtleChallenge.Core.Interfaces;

namespace TurtleChallenge.Core.Models;
public class GameSettings : LoadableFromFile<GameSettings>, IGameSettings, IUserInterface
{

    public ICoordinate StartPosition { get; set; } = new Coordinate(0, 0);
    public Direction StartDirection { get; set; } = Direction.North;
    public ICollection<ICoordinate> Mines { get; set; } = new List<ICoordinate>();
    public ICoordinate ExitPosition { get; set; } = new Coordinate(1, 1);
    public ICoordinate BoardSize { get; set; } = new Coordinate(5, 5);

    public GameSettings(string fileLocation)
    {
        FileLocation = fileLocation;
    }

    /// <summary>
    /// Returns an Instance of <see cref="GameSettings"/> given a settings file.
    /// </summary>
    /// <param name="fileLocation"></param>
    /// <returns></returns>
    public static GameSettings GetInstance(string fileLocation)
    {
        var instance = new GameSettings(fileLocation ?? Defaults.GameSettingsLocation);
        var MinesXY = instance.ReadIniValue("Game Coordinates", "MinesXY").Split(' ');

        instance.Mines = MinesXY.Select(coord => (ICoordinate)new Coordinate(coord)).ToList();
        instance.StartPosition = new Coordinate($"{instance.ReadIniValue("Game Coordinates", "StartPositionX")}, {instance.ReadIniValue("Game Coordinates", "StartPositionY")}");
        instance.ExitPosition = new Coordinate($"{instance.ReadIniValue("Game Coordinates", "ExitX")}, {instance.ReadIniValue("Game Coordinates", "ExitY")}");
        instance.StartDirection = Enum.Parse<Direction>(instance.ReadIniValue("Game Coordinates", "StartDir"), ignoreCase: true);
        instance.BoardSize = new Coordinate($"{instance.ReadIniValue("Board", "BoardX")}, {instance.ReadIniValue("Board", "BoardY")}");

        return instance;
    }
}
