using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Microsoft.VisualBasic;

public class ExcelUtil
{
    public static List<string> GetTeamNamesFromCsv(string filePath)
    {
        var fileData = System.IO.File.ReadAllText(filePath);
        var lines = fileData.Split('\n').ToList();
        foreach (var line in lines)
        {
            Debug.Log(line);
        }

        return null;
    }
}
