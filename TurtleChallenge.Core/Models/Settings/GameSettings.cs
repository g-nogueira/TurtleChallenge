using System.Collections;
using TurtleChallenge.Core.Interfaces;
using TurtleChallenge.Utils;

namespace TurtleChallenge.Core.Models;
/// <summary>
/// Class representing a GameSettings.ini file.
/// </summary>
public class GameSettings : IGameSettings
{

    #region Private Variables
    private IVector2 startPosition = new Vector2(0, 0);
    private IVector2 exitPosition = new Vector2(1, 1);
    private IVector2 boardSize = new Vector2(5, 5);
    #endregion

    #region Properties
    public string FileLocation { get; }
    public IVector2 StartCoodinates { get => startPosition.Clone(); }
    public IVector2 ExitCoordinates { get => exitPosition.Clone(); }
    public IVector2 BoardSize { get => boardSize.Clone(); }
    public Direction StartDirection { get; }
    public ICollection<IVector2> Mines { get; }
    #endregion

    public GameSettings(string fileLocation = Defaults.GameSettingsLocation)
    {
        var fileReader = new FileReader(fileLocation);

        // Read file values
        var fileValues = new Dictionary<string, string>()
        {
            {"StartPositionX",  fileReader.INI("Game Coordinates", "StartPositionX")},
            {"StartPositionY",  fileReader.INI("Game Coordinates", "StartPositionY")},
            {"ExitX",           fileReader.INI("Game Coordinates", "ExitX")},
            {"ExitY",           fileReader.INI("Game Coordinates", "ExitY")},
            {"StartDir",        fileReader.INI("Game Coordinates", "StartDir")},
            {"MinesXY",         fileReader.INI("Game Coordinates", "MinesXY")},
            {"BoardX",          fileReader.INI("Board", "BoardX")},
            {"BoardY",          fileReader.INI("Board", "BoardY")}
        };

        // Initialize instance attributes
        FileLocation    = fileLocation;
        StartDirection  = Enum.TryParse<Direction>(fileValues["StartDir"], ignoreCase: true, out var startDirection) ? startDirection : Direction.North;
        Mines           = fileValues["MinesXY"].Split(' ').Select(coord => (IVector2)new Vector2(coord)).ToList();
        startPosition   = new Vector2(fileValues["StartPositionX"], fileValues["StartPositionY"]);
        exitPosition    = new Vector2(fileValues["ExitX"], fileValues["ExitY"]);
        boardSize       = new Vector2(fileValues["BoardX"], fileValues["BoardY"]);
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
