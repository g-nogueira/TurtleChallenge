namespace TurtleChallenge.Utils;
public static class SettingsFileExtensions
{
    public static KeyValuePair<string, IEnumerable<string>> AsKeyValue(this string s)
    {
        return new KeyValuePair<string, IEnumerable<string>>(s.Split(' ')[0], s.Split(' ').Skip(1));
    }
    
}
