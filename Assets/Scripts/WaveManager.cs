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
    private Wave[] waveList;
    private Wave[] extraWaveList;
    [SerializeField] private Transform[] _TEMPORALENEMYLIST;
    void Start()
    {
        _gm = FindObjectOfType<GameManager>();
        waveList = new Wave[3];
        extraWaveList = new Wave[3];
        switch (_actualLevel)//send the spawner parameter based on the level
        {
            case 1:
                
                Wave wave = new Wave();
                wave.SetName("Wave1");
                wave.SetType(_TEMPORALENEMYLIST[0]);
                wave.SetAmmountAndRate(3,2f);                
                waveList[0] = wave;

                Wave wave1 = new Wave();
                wave1.SetName("Wave2");
                wave1.SetType(_TEMPORALENEMYLIST[1]);
                wave1.SetAmmountAndRate(2, 3f);                
                waveList[1] = wave1;

                Wave wave2 = new Wave();
                wave2.SetName("Wave3");
                wave2.SetType(_TEMPORALENEMYLIST[2]);
                wave2.SetAmmountAndRate(1, 4f);                
                waveList[2] = wave2;


                Wave extraWave = new Wave();
                extraWave.SetName("ExtraWave1");
                extraWave.SetType(_TEMPORALENEMYLIST[1]);
                extraWave.SetAmmountAndRate(1,7f);
                extraWaveList[0] = extraWave;

                Wave extraWave1 = new Wave();
                extraWave1.SetName("ExtraWave1");
                extraWave1.SetType(_TEMPORALENEMYLIST[1]);
                extraWave1.SetAmmountAndRate(2, 7f);
                extraWaveList[1] = extraWave1;

                Wave extraWave2 = new Wave();
                extraWave2.SetName("ExtraWave1");
                extraWave2.SetType(_TEMPORALENEMYLIST[1]);
                extraWave2.SetAmmountAndRate(2, 10f);
                extraWaveList[2] = extraWave2;
                FindSpawners();
                break;
            case 2:
             
                FindSpawners();

                break;
        }
    }
    private void FindSpawners()
    {
        
        _spawner = FindObjectOfType<SpawnerBehaviour>();
        _spawner.SetWaves(waveList,extraWaveList);
        //_spawner.StartSpawner(_actualLevel, _totalEnemies, _totalWaves);

    }

}
