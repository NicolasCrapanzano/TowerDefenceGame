using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    //need total ammount of enemies for the level
    //ammount of working spawners
    //array of all the spawners
    //ammount of waves
    //divide total enemies for each wave and each spawner
    private int _totalEnemies, _totalWaves, _activeSpawners,_alreadyActivatedSpawners;
    public static int _actualLevel;
    private SpawnerBehaviour _spawner;
    private GameManager _gm;
    void Start()
    {
        _gm = FindObjectOfType<GameManager>();

        switch (_actualLevel)//send the spawner parameter based on the level
        {
            case 1:
                _totalEnemies = 7;
                _totalWaves = 3;               
                FindSpawners();
                _gm.LevelObjective(_totalWaves);
                break;
            case 2:
                _totalEnemies = 17;
                _totalWaves = 5;                
                FindSpawners();
                _gm.LevelObjective(_totalWaves);
                break;
        }
    }
    private void FindSpawners()
    {
        
        _spawner = FindObjectOfType<SpawnerBehaviour>();
        _spawner.StartSpawner(_actualLevel, _totalEnemies, _totalWaves);

    }

}
