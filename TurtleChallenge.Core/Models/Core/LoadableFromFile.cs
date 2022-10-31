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
public abstract class LoadableFromFile<T>
{
    public string FileLocation { get; set; }

    public LoadableFromFile(string fileLocation)
    {
        FileLocation = fileLocation;
    }

    public LoadableFromFile() { }

    /// <summary>
    /// Reads an .ini file from a given location.
    /// </summary>
    /// <param name="fileLocation"></param>
    /// <returns></returns>
    public virtual string ReadIniValue(string section, string key)
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
    public virtual IEnumerable<string> ReadCSVFile(string fileLocation)
    {
        var records = new List<string>();

        using (var reader = new StreamReader(fileLocation))
        {
            var line = reader.ReadLine() ?? "";
            var values = line.Split(',');

            records.AddRange(values);
        }

        return records;
    }

    public virtual bool IsIniLineValid(string key, IEnumerable<string> value)
    {
        return true;
    }

    public virtual bool IsCSVLineValid(string key, string value)
    {
        return true;
    }
}