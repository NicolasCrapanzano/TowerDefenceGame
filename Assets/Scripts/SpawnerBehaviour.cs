using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnerBehaviour : MonoBehaviour
{
    [SerializeField] Text _timeLeftDisplay;
    [SerializeField]
    private float _timer,_endTimer,_maxTime;
    [SerializeField]
    private GameObject[] _enemies;
    private GameManager _gm;
    private bool _isActive = false,_betweenWaves=false;
    private int _enemiesTotal, _enemiesLeft,_actualWave,_totalWaves,_difficulty;
    //set an ammount of waves and an ammount of enemies on each wave, when a wave ends there is a delay until the next starts


    public void StartSpawner(int dif, bool active,int enemiesAmmount,int waves,bool counter , float spRate = 5)
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
            _maxTime = spRate;
            _timer = _maxTime;
        }else
        {
            Debug.Log("spawner desactivado");
            gameObject.SetActive(false);
        }
        if(counter == true)
        {

        }
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
        if (_isActive == true && _timer <= 0 && _enemiesLeft > 0)
        {
            //instatiate an enemy
            _betweenWaves = false;
            _timeLeftDisplay.text = " ";
            _timer = _maxTime;
            Instantiate(_enemies[0], transform.position, Quaternion.identity);
            _enemiesLeft--;

        }
        else if (_enemiesLeft <= 0 && _actualWave < _totalWaves)
        {
            _betweenWaves = true;
            _enemiesLeft = _enemiesTotal / _totalWaves;
            _actualWave++;
            _gm.SendMessage("Progress",false);

            //display time until next wave
            _timer = 10;
        } else if (_difficulty != 0 && _endTimer <= Time.time&& _actualWave >= _totalWaves && _enemiesLeft <= 0) //you won
        {
            _endTimer = Time.time + 5;
            _gm.SendMessage("Progress",true);
        }

    }
}
