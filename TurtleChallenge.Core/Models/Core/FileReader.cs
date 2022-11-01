using CsvHelper;
using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurtleChallenge.Utils;

namespace TurtleChallenge.Core.Models;

/// <summary>
/// Class responsible for reading files across the project.
/// </summary>
public class FileReader
{
    public string FileLocation { get; set; }

    public FileReader(string fileLocation) => FileLocation = fileLocation;

    /// <summary>
    /// Reads an .ini file from a given location.
    /// </summary>
    /// <param name="fileLocation"></param>
    /// <returns></returns>
    public string INI(string section, string key)
    {
        INIFileReader iniFile = new INIFileReader(FileLocation);

        return iniFile.IniReadValue(section, key);
    }

    /// <summary>
    /// Reads a .csv file from a given location.
    /// </summary>
    /// <typeparam name="TRecordType">The type of each record read from the file.</typeparam>
    /// <param name="fileLocation"></param>
    /// <returns></returns>
    public IEnumerable<string> CSV()
    {
        var records = new List<string>();

        using (var reader = new StreamReader(FileLocation))
        {
            var line = reader.ReadLine() ?? "";
            var values = line.Split(',');

            records.AddRange(values);
        }

        return records;
    }
}