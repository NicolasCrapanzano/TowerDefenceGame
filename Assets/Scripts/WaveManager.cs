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
    private SpawnerBehaviour[] _spawnerList;
    private GameManager _gm;
    void Start()
    {
        _gm = FindObjectOfType<GameManager>();

        switch (_actualLevel)
        {
            case 1:
                _totalEnemies = 30;
                _totalWaves = 3;
                _activeSpawners = 2;
                FindSpawners();
                _gm.LevelObjective(_totalWaves,_activeSpawners);
                break;
            case 2:
                _totalEnemies = 60;
                _totalWaves = 5;
                _activeSpawners = 3;
                FindSpawners();
                _gm.LevelObjective(_totalWaves, _activeSpawners);
                break;
        }
    }
    private void FindSpawners()
    {
        SpawnerBehaviour save1;
        _spawnerList = FindObjectsOfType<SpawnerBehaviour>();
        for (int i = 0; i < _activeSpawners; i++)
        {
            int change = Random.Range(i+1 , _spawnerList.Length);
            save1 = _spawnerList[change];
            _spawnerList[change] = _spawnerList[i];
            _spawnerList[i] = save1;
        }

        for (int i = 0;i < _spawnerList.Length; i++)
        {
            bool _hasTimer = false,_isActive = false;
            
            if (i < _activeSpawners)
            {
                _isActive = true;
                if (i == 0)
                {
                    _hasTimer = true;
                }
            }
            _spawnerList[i].StartSpawner(_actualLevel, _isActive, _totalEnemies / _activeSpawners, _totalWaves, _hasTimer);
        }
    }
    

}
