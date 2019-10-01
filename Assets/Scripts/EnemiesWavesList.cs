using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class  EnemiesWavesList : Singleton<EnemiesWavesList>
{
    private static EnemiesWavesList _instance;
    //arrays with the codes of the enemies that are going to spawn, classified for levels

    //LEVEL 0 || 2 spawn Points || 10 total
    private string[] _nextEnemylvl0 = {"000e","0000" };   //e means no spawn (empty)

    //LEVEL 1 || 3 spawn points ||
    private string[] _nextEnemylvl1 = {"0101" };
    public string[] ThisWaveEnemies(int whatlvl) //starts at 1
    {
        if(whatlvl == 1)
        {
            return _nextEnemylvl0;

        }else if(whatlvl == 2)
        {
            return _nextEnemylvl1;
        }
        else
        {
            Debug.Log("something went wrong . . .");
            return null;
        }
    }
}
