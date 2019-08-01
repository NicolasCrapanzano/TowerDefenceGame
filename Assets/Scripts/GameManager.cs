using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _UIHealth;
    [SerializeField]
    private int _coins,_totalWaves,_spawners,_actualWave;
    [SerializeField]
    private Text _coinDisplay,_youwin;
    private EnemyBehaviour[] _enemiesLeft;
    private bool _almostEnd=false;
    private float _endTimer;
    void Start()
    {
        _coins = 0;
    }
    private void Update()
    {
        if(_almostEnd == true && _endTimer <= Time.time)
        {
            Progress();
        }

    }

    private void CoinsCounter(int coins)
    {

        _coins = _coins + coins;
        _coinDisplay.text = "Coins : " + _coins;
    }

    public int HowManyCoins()
    {
        return _coins;
    }

    public void LevelObjective(int waves,int spawners)//recieve data from the wave manager
    {
        _totalWaves = waves;
        _spawners = spawners;
        _actualWave = 1 * _spawners;
    }
    private void Progress(bool end=false)//recieve data from the spawner
    {
        if (_actualWave < _totalWaves * _spawners)
        {
            _actualWave++;
        }
        if(_actualWave >= _totalWaves *_spawners)
        {
            //you win m8
            _enemiesLeft = FindObjectsOfType<EnemyBehaviour>();
            Debug.Log(_enemiesLeft);
            if (_enemiesLeft.Length <= 0 && end == true)
            {
                
                _youwin.gameObject.SetActive(true);
                StartCoroutine(Wait());
            }else
            {
                _almostEnd = true;
                _endTimer = Time.time + 2;
            }
        }
    }
    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(3);
        Cursor.visible = true;
        SceneManager.LoadScene(1);
    }
    private void HealthDisplay(int h) // show health in the UI
    {
        int _playerHealth = h;
        switch (_playerHealth)
        {
            case 3:
                _UIHealth[2].SetActive(true);
                _UIHealth[1].SetActive(true);
                _UIHealth[0].SetActive(true);
                break;
            case 2:
                _UIHealth[2].SetActive(false);
                _UIHealth[1].SetActive(true);
                _UIHealth[0].SetActive(true);
                break;
            case 1:
                _UIHealth[2].SetActive(false);
                _UIHealth[1].SetActive(false);
                _UIHealth[0].SetActive(true);
                break;
            case 0:
                _UIHealth[2].SetActive(false);
                _UIHealth[1].SetActive(false);
                _UIHealth[0].SetActive(false);
                break;
        }
    }
}
