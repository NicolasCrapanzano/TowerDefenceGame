﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    //Health
    [SerializeField]
    private Text _healthTxt,_waveDisplay;
    [SerializeField]
    private Image _healthBar;
    private int _maxHealth;
    //
    [SerializeField]
    private int _actualCoins,_realCoins,_totalWaves,_spawners,_actualWave;
    [SerializeField]
    private Text _coinDisplay,_youwin;
    [SerializeField] private Animator _coinDisplayMove;
    private EnemyBehaviour[] _enemiesLeft;
    private bool _almostEnd=false,_dontEnterAgain=false;
    private float _endTimer, _coinTimer;
    void Start()
    {

        _realCoins = 0;
        _actualCoins = 0;
        _coinDisplayMove = _coinDisplay.GetComponentInParent<Animator>();

    }
    private void Update()
    {

        if(_almostEnd == true && _endTimer <= Time.time)
        {
            Progress();
        }
        if(Input.GetKey(KeyCode.R)) //desaparecer vida enemigos?
        {
            
        }
        if (_actualCoins != _realCoins && _coinTimer <= Time.time)
        {
            UpdateCoins();
            
        }
        
       
    }
    private void GetPlayerHealth(int health)
    {
        _maxHealth = health;
        HealthDisplay(health);
    }
    private void UpdateCoins()
    {

        if (_actualCoins < _realCoins)
        {
            _actualCoins++;
        }
        else if (_actualCoins > _realCoins)
        {
            _actualCoins--;
            
        }
        //modify text
        _coinTimer = Time.time + 0.3f;
        _coinDisplay.text = "Coins : " + _actualCoins;
    }
    private void CoinsCounter(int coins)
    {

        _realCoins = _realCoins + coins;
        
    }

    public int HowManyCoins()
    {
        return _realCoins;
    }

    public void LevelObjective(int waves,int spawners)//recieve data from the wave manager
    {
        _totalWaves = waves;
        _spawners = spawners;
        _actualWave = 1 * _spawners;
        _waveDisplay.text = "Wave : " + _actualWave / _spawners + " / " + _totalWaves;
    }
    private void Progress(bool end=false)//recieve data from the spawner
    {
        if (_actualWave < _totalWaves * _spawners)
        {
            _actualWave++;
            _waveDisplay.text = "Wave : " + _actualWave / _spawners + " / " + _totalWaves;
        }
        if(_actualWave >= _totalWaves *_spawners)
        {
            //you win m8
            _enemiesLeft = FindObjectsOfType<EnemyBehaviour>();

            if (_enemiesLeft.Length <= 0 && end == true)
            {
                //send the ammount of money you won and save it
                
                _youwin.gameObject.SetActive(true);
                if (_dontEnterAgain == false)
                {
                    StartCoroutine(Wait());
                }
            }else
            {
                _almostEnd = true;
                _endTimer = Time.time + 2;
            }
        }
    }
    private IEnumerator Wait()
    {
        _dontEnterAgain = true;
        yield return new WaitForSeconds(3);
        SaveLoadSystem.SaveGold(_realCoins, 1);
        Cursor.visible = true;
        SceneManager.LoadScene(0);
        _realCoins = 0;
    }
    private void HealthDisplay(int h) // show health in the UI
    {
        float _playerHealth = (float)h / _maxHealth;
        Debug.Log(_playerHealth);
        _healthTxt.text = h + "\r\n" + "-" + "\r\n" + _maxHealth;
        _healthBar.fillAmount = _playerHealth;
    }
}
