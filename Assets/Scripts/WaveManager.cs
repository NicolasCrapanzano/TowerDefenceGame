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
    private int _totalEnemies, _totalWaves, _actualLevel, _activeSpawners;
    private SpawnerBehaviour[] _spawnerList;
    void Start()
    {
        _actualLevel = 1;
        switch (_actualLevel)
        {
            case 1:
                _totalEnemies = 30;
                _totalWaves = 3;
                _activeSpawners = 2;
                FindSpawners();
                break;
        }
    }
    private void FindSpawners()
        {
            _spawnerList = FindObjectsOfType<SpawnerBehaviour>();
            
            for (int i = 0;i <_activeSpawners; i++)
            {
                _spawnerList[i].gameObject.SetActive(true);
                _spawnerList[i].StartSpawner(true);
            }
        }
    
    void Update()
    {
        
    }
}
