using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnerBehaviour : MonoBehaviour
{

    private enum SpawnState { SPAWNING, WAITING, COUNTING };


    private int _nextWave = 0;
    private bool _isInfinite = false; //set true if you want the level to loop

    [SerializeField] private Wave[] _waves; //normal wave list
    [SerializeField] private Wave[] _extraWaves; //if you want other type of enemy in the same wave set them here
    [SerializeField] private GameManager _gm;
    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private float _timeBtwWaves = 5f, _waveCountdown, _searchCountdown=1f;

    private SpawnState state = SpawnState.COUNTING;
    
    public void SetWaves(Wave[] _normalwaves, Wave[] _extrawaves)
    {
        _waves[0] = _normalwaves[0];
        _waves[1] = _normalwaves[1];
        _waves[2] = _normalwaves[2];

        _extraWaves[0] = _extrawaves[0];
        _extraWaves[1] = _extrawaves[1];
        _extraWaves[2] = _extrawaves[2];

    }
    private void Start()
    {
        _gm = FindObjectOfType<GameManager>();
        _gm.LevelObjective(_waves.Length);

        if(_spawnPoints.Length == 0)
        {
            Debug.LogError("No spawners detected");
        }
        _waveCountdown = _timeBtwWaves;
    }

    private void Update()
    {

        if(state == SpawnState.WAITING)//check if there is any enemy left
        {
            if(!EnemyIsAlive())
            {
                //start new round
                
                WaveCompleted();
                return;
            }else
            {
                return;
            }
        }

        if(_waveCountdown <= 0)
        {
            if(state != SpawnState.SPAWNING)
            {
                StartCoroutine(SpawnWave(_waves[_nextWave]));
                if(_extraWaves.Length > 0 && _nextWave < _extraWaves.Length )
                {
                    if (_extraWaves[_nextWave] != null)
                    {
                        StartCoroutine(SpawnWave(_extraWaves[_nextWave]));
                    }
                }
            }
        }else
        {
            _waveCountdown -= Time.deltaTime;
        }
    }

    void WaveCompleted()
    {
        Debug.Log("Wave Completed");

        state = SpawnState.COUNTING;
        _waveCountdown = _timeBtwWaves;
        _gm.UpdateWaveDisplay();

        if (_nextWave + 1 > _waves.Length - 1)
        {
            Debug.Log("All waves completed ! !" + "wave: " + (_nextWave + 1) + " / " + _waves.Length + "Ending level . . .");
            
            if (_isInfinite == true) //if the level is infinite this is going to start all over
            {
                _nextWave = 0;
            }else //else is going to end and send to the main menu
            {
                _gm.SendMessage("Progress", true);
            }
        }
        else
        {
            
            _nextWave++;
            Debug.Log("wave: " + (_nextWave + 1) + " / " + _waves.Length);
        }
        
    }

    private bool EnemyIsAlive()
    {
        _searchCountdown -= Time.deltaTime;

        if(_searchCountdown <= 0f)
        {
            _searchCountdown = 1f;
            if (GameObject.FindGameObjectWithTag("Enemy") == null)
            {
                return false;
            }
        }
        return true;
    }

    IEnumerator SpawnWave(Wave _wave,Wave _extraWave = null)
    {
        Debug.Log("Spawning Wave: " + _wave.GetName());
        state = SpawnState.SPAWNING;

        for (int i = 0; i < _wave.GetAmmount(); i++)
        {
            SpawnEnemy(_wave.GetType());
            if(_extraWave != null)
            {
                SpawnEnemy(_extraWave.GetType());
            }
            yield return new WaitForSeconds(1f/_wave.GetRate());
        }

        state = SpawnState.WAITING;

        yield break;
    }

    void SpawnEnemy (Transform _enemy)
    {
        Debug.Log("Spawning enemy: " + _enemy.name);

        Transform _sp = _spawnPoints[Random.Range(0, _spawnPoints.Length)];
        Instantiate(_enemy, _sp.transform.position, Quaternion.identity);

    }
}
