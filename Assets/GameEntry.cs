using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEntry : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ExcelUtil.GetTeamNamesFromCsv(Application.dataPath + "/teamNames.csv");
    }
}
