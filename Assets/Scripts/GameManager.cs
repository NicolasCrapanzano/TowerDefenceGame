using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _UIHealth;
    private int _coins;
    [SerializeField]
    private Text _coinDisplay;
    void Start()
    {
        _coins = 0;
    }

    
    void Update()
    {
        
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
