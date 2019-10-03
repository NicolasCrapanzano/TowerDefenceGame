using System.Collections;
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
    private int _actualCoins,_realCoins,_totalWaves,_actualWave;
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

        if(Input.GetKey(KeyCode.Escape)) //pause menu
        {
            //go to main menu
            Cursor.visible = true;
            SceneManager.LoadScene(0);
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

    public void LevelObjective(int waves)//recieve the total ammount of waves for that level  || what to do when infinite ?
    {
        _totalWaves = waves;
        _actualWave = 1 ;
        _waveDisplay.text = "Wave : " + _actualWave + " / " + _totalWaves;
    }

    public void UpdateWaveDisplay()//update the display of the actual and the total waves
    {
        if (_actualWave < _totalWaves)
        {
            _actualWave++;
            _waveDisplay.text = "Wave : " + _actualWave + " / " + _totalWaves;
        }
    }
    private void Progress(bool end=false)//recieve data from the spawner
    {
        if(end == true)
        {
            //send the ammount of money you won and save it  
            _youwin.gameObject.SetActive(true);
            if (_dontEnterAgain == false)
            {
                StartCoroutine(Wait());
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
