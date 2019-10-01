using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnerBehaviour : MonoBehaviour
{
    [SerializeField] Text _timeLeftDisplay;
    [SerializeField]
    private float _timer, _endTimer, _maxTime;
    [SerializeField]
    private GameObject[] _enemies;

    private int _counter=0;
    private string[] _enemiesList;
    private int[] _spawnPoint1, _spawnPoint2, _spawnPoint3, _spawnPoint4, _spawnPoint5;
    private int[] _wichPoint;

    [SerializeField]
    private GameObject[] _spawnPoints;
    private GameManager _gm;
    private bool _betweenWaves=false;
    private int _enemiesTotal, _enemiesLeft,_actualWave,_totalWaves,_difficulty,_actualEnemySpawnAmmount;
    //set an ammount of waves and an ammount of enemies on each wave, when a wave ends there is a delay until the next starts


    public void StartSpawner(int dif, int enemiesAmmount,int waves, float spRate = 5)
    {
        
        
            _gm = FindObjectOfType<GameManager>();
            _difficulty = dif;
            _actualWave = 1;
            _enemiesTotal = enemiesAmmount;
            
            _enemiesLeft = _enemiesTotal / waves;
             Debug.Log(_enemiesLeft);
            _totalWaves = waves;
            _maxTime = spRate;
            _timer = _maxTime;

            DistributeEnemies();
        //_enemiesList = EnemiesWavesList.Instance.ThisWaveEnemies(dif);
    }

    void Update()
    {
        if(_timer > 0) //move the timer to a separate script!!!
        {
            _timer -= Time.deltaTime;

            if (_betweenWaves == true)
            {
                _timeLeftDisplay.text = "Next Wave In : " + (int)_timer;
                //show timer text
            }
        }
        if ( _timer <= 0 && _enemiesLeft > 0) // && _actualEnemySpawnAmmount * _spawnPoints.Length < _enemiesTotal
        {
            //instatiate an enemy
            _betweenWaves = false;
            _timeLeftDisplay.text = " ";
            _timer = _maxTime;
            for (int i = 0; i <_spawnPoints.Length ; i++) //this spawns  1 enemy per cycle
            {
                if (i == 0 ) // spawn point 1
                {
                    Instantiate(_enemies[_spawnPoint1[_wichPoint[i]]], _spawnPoints[i].transform.position, Quaternion.identity);
                    _wichPoint[i]++;
                    _enemiesLeft--;
                }
                else if(i == 1) //spawn point 2
                {
                    Debug.Log(_wichPoint[i] + _actualEnemySpawnAmmount);
                    Instantiate(_enemies[_spawnPoint2[_wichPoint[i]]], _spawnPoints[i].transform.position, Quaternion.identity);
                    _wichPoint[i]++;
                    _enemiesLeft--;
                    
                }else if(i == 2)
                {
                    Instantiate(_enemies[_spawnPoint3[_wichPoint[i]]], _spawnPoints[i].transform.position, Quaternion.identity);
                    _enemiesLeft--;
                    _wichPoint[i]++;
                }
                else if (i == 3)
                {
                    Instantiate(_enemies[_spawnPoint4[_wichPoint[i]]], _spawnPoints[i].transform.position, Quaternion.identity);
                    _enemiesLeft--;
                    _wichPoint[i]++;
                }

                Debug.Log(_enemiesLeft);
                _actualEnemySpawnAmmount++;
            }

        }
        else if (_enemiesLeft <= 0 && _actualWave < _totalWaves)
        {

            _betweenWaves = true;
            _enemiesLeft = _enemiesTotal / _totalWaves;
            _actualWave++;
            if(_difficulty > 0)
            {
                _gm.SendMessage("Progress", false);
            }
            
            //display time until next wave
            _timer = 10;

        } else if (_difficulty != 0 && _endTimer <= Time.time&& _actualWave >= _totalWaves && _enemiesLeft <= 0) //you won
        {

            _endTimer = Time.time + 5;
            _gm.SendMessage("Progress",true);

        }
        
    }

    private void DistributeEnemies() //recieve a char array and divides its contents on the ammount of spawn points for the level, fill and an array with values until reaching '/' symbol, then change to another array
    {
        _enemiesList = EnemiesWavesList.Instance.ThisWaveEnemies(_difficulty);
        _wichPoint = new int[_spawnPoints.Length];
        for (int i = 0; i < _enemiesList.Length; i++)
        {
            string stringTemporal = _enemiesList[i];
            for (int a = 0; a < stringTemporal.Length ; a++)
            {
                if(_spawnPoint1 == null) // START checking if the list its empty, if it is create a new one
                {
                    _spawnPoint1 = new int[stringTemporal.Length];
                }else if(_spawnPoint2 == null)
                {
                    _spawnPoint2 = new int[stringTemporal.Length];
                }
                else if (_spawnPoint3 == null)
                {
                    _spawnPoint3 = new int[stringTemporal.Length];
                }
                else if (_spawnPoint4 == null)
                {
                    _spawnPoint4 = new int[stringTemporal.Length];
                }
                else if (_spawnPoint5 == null)
                {
                    _spawnPoint5 = new int[stringTemporal.Length];
                }

                if(stringTemporal[a] == 'e')
                {
                    _spawnPoint1[a] = -1; //this means that in that wave the spawner does nothing
                }
                else
                {
                    _spawnPoint1[a] = (int)stringTemporal[a] - 48;
                }
                
            }
            
        }
    }
}
