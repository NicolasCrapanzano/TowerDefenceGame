using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerBehaviour : MonoBehaviour
{
    [SerializeField]
    private float _timer,_endTimer;
    [SerializeField]
    private GameObject[] _enemies;
    private GameManager _gm;
    private bool _isActive = false;
    private int _enemiesTotal, _enemiesLeft,_actualWave,_totalWaves,_difficulty;
    //set an ammount of waves and an ammount of enemies on each wave, when a wave ends there is a delay until the next starts


    public void StartSpawner(int dif, bool active,int enemiesAmmount,int waves, float spRate = 5)
    {
        if (active == true)
        {
            _gm = FindObjectOfType<GameManager>();
            _difficulty = dif;
            _actualWave = 1;
            _isActive = active;
            _enemiesTotal = enemiesAmmount;
            _enemiesLeft = _enemiesTotal / waves;
            _totalWaves = waves;
            _timer = Time.time + spRate;
        }else
        {
            gameObject.SetActive(false);
        }
    }
    void Update()
    {
        if (_isActive == true && _timer <= Time.time && _enemiesLeft > 0)
        {
            //instatiate an enemy

            _timer = Time.time + 2;
            Instantiate(_enemies[0], transform.position, Quaternion.identity);
            _enemiesLeft--;

        }
        else if (_enemiesLeft <= 0 && _actualWave < _totalWaves)
        {
            _enemiesLeft = _enemiesTotal / _totalWaves;
            _actualWave++;
            _gm.SendMessage("Progress",false);
            Debug.Log(name + "Progress + 1");
            //display time until next wave
            _timer = Time.time + 5;
        } else if (_difficulty != 0&&_endTimer <= Time.time&& _actualWave >= _totalWaves && _enemiesLeft <= 0) //you won
        {
            _endTimer = Time.time + 3;
            _gm.SendMessage("Progress",true);
        }
    }
}
