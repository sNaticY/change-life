using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Microsoft.VisualBasic;
using Unity.Collections;
using Unity.Entities;

public class ExcelUtil
{
    public static List<string> GetTeamNamesFromCsv(string filePath)
    {
        var fileData = System.IO.File.ReadAllText(filePath);
        return fileData.Split('\n').ToList();
    }
    
}
