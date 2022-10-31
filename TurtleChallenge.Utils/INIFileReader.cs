using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;


namespace TurtleChallenge.Utils;

/// <summary>
/// Reader for files of type INI. Note that this shouldn't be used in real applications. We are using it just for the fun of it.
/// For more info, see <see href="https://gist.github.com/Sn0wCrack/5891612"/> and <see href="https://stackoverflow.com/questions/7336185/what-is-the-purpose-of-getprivateprofilestring"/>.
/// </summary>
public class INIFileReader
{
    public string path { get; private set; }

    [DllImport("kernel32")]
    private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
    [DllImport("kernel32")]
    private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

    public INIFileReader(string INIPath)
    {
        path = INIPath;
    }
    public void IniWriteValue(string Section, string Key, string Value)
    {
        WritePrivateProfileString(Section, Key, Value, this.path);
    }

    public string IniReadValue(string Section, string Key)
    {
        StringBuilder temp = new StringBuilder(255);
        int i = GetPrivateProfileString(Section, Key, "", temp, 255, this.path);
        return temp.ToString();
    }
}
