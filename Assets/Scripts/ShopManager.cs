using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    [SerializeField]
    private Text _displayCoins;
    private bool test=true;
    private GameObject[] buttons;
    // SKILLS LEVELS
    private int[] _statsLevel = new int[] {0,0,0,0}; //each slot is a different stat level order : { health, damage, spd, scatter }

    // PLAYER STATS

    private int[] _buyHealth = new int[] {3,4,5}; // 3 "levels" lvl0 = 3 hp ; lvl1 = 4 hp ; lvl2 = 5 hp ;
    private int[] _healthCost = new int[] { 15, 25, 35 };

    private int[] _buyDamage = new int[] { 1, 2, 3 }; // 3 "levels" lvl0 = 1 dmg ; lvl1 = 2dmg ; lvl2 = 3dmg ;
    private int[] _damageCost = new int[] { 10, 20, 30 };

    private float[] _buyAtkSpd = new float[] {2f,1.5f,1f,0.75f }; // 4 "levels" lvl0 = 2 spd ; lvl1 = 1.5 spd ; lvl2 = 1 spd ; lvl3 = 0.75 spd;
    private int[] _atkSpdCost = new int[] { 13, 26, 39,42 };

    private float[] _buyScatter = new float[] {8f,7f,6f,5f }; //  5 "Levels" lvl0 = 8f ; lvl1 = 7f ; lvl2 = 6 ; lvl3 = 5 ; lvl4 = 5 ;
    private int[] _scatterCost = new int[] { 12, 24, 36,47 };

    // WORLD STATS
    private int _playerCoins;

    public void Shop(int id)
    {
        Debug.Log("nice choise ");
        switch(id)
        {
            case 0:

                if (_statsLevel[id] < (_buyHealth.Length - 1) && _playerCoins >= _healthCost[_statsLevel[id]])
                { 
                    
                    _playerCoins = _playerCoins - _healthCost[_statsLevel[id]];
                    _displayCoins.text = "Your Gold : " + _playerCoins;
                    _statsLevel[id]++;
                    Debug.Log("thanks ! come back later");
                    UpdateShop();
                }else if (_statsLevel[id] >= (_buyHealth.Length - 1))
                {
                    Debug.Log("You already are in the max level");
                }else
                {
                    Debug.Log("You dont have the money for that buddy");
                }
                break;
            case 1:

                if (_statsLevel[id] < (_buyDamage.Length - 1) && _playerCoins >= _damageCost[_statsLevel[id]])
                {

                    _playerCoins = _playerCoins - _damageCost[_statsLevel[id]];
                    _displayCoins.text = "Your Gold : " + _playerCoins;
                    _statsLevel[id]++;
                    Debug.Log("thanks ! come back later");
                    UpdateShop();
                }
                else if (_statsLevel[id] >= (_buyDamage.Length - 1))
                {
                    Debug.Log("You already are in the max level");
                }
                else
                {
                    Debug.Log("You dont have the money for that buddy");
                }
                break;
            case 2:

                if (_statsLevel[id] < (_buyAtkSpd.Length - 1) && _playerCoins >= _atkSpdCost[_statsLevel[id]])
                {

                    _playerCoins = _playerCoins - _atkSpdCost[_statsLevel[id]];
                    _displayCoins.text = "Your Gold : " + _playerCoins;
                    _statsLevel[id]++;
                    Debug.Log("thanks ! come back later");
                    UpdateShop();
                }
                else if (_statsLevel[id] >= (_buyAtkSpd.Length - 1))
                {
                    Debug.Log("You already are in the max level");
                }
                else
                {
                    Debug.Log("You dont have the money for that buddy");
                }
                break;
            case 3:

                if (_statsLevel[id] < (_buyScatter.Length - 1) && _playerCoins >= _scatterCost[_statsLevel[id]])
                {

                    _playerCoins = _playerCoins - _scatterCost[_statsLevel[id]];
                    _displayCoins.text = "Your Gold : " + _playerCoins;
                    _statsLevel[id]++;
                    Debug.Log("thanks ! come back later");
                    UpdateShop();
                }
                else if (_statsLevel[id] >= (_buyScatter.Length - 1))
                {
                    Debug.Log("You already are in the max level");
                }
                else
                {
                    Debug.Log("You dont have the money for that buddy");
                }
                break;
        }
    }
    public void UpdateShop()
    {
        if (buttons == null)
        {
            buttons = GameObject.FindGameObjectsWithTag("StoreButton");
        }
        //first get an array with all the buttons
        for(int i=0;i < buttons.Length;i++)
        {
            Text tempButton = buttons[i].GetComponentInChildren<Text>();
            if(i == 0)
            {
                tempButton.text ="Health : "+ (1+_buyHealth[_statsLevel[i]])  + "\r\n" + "Cost : " + _healthCost[_statsLevel[i]];
            }else if(i == 1)
            {
                tempButton.text = "Damage : " + (1 + _buyDamage[_statsLevel[i]]) + "\r\n" + "Cost : " + _damageCost[_statsLevel[i]];
            }else if(i == 2)
            {
                tempButton.text = "Atk Spd : " +_buyAtkSpd[_statsLevel[i]] + "\r\n" + "Cost : " + _atkSpdCost[_statsLevel[i]];
            }
        }
        //do a for to go through the array
        //change the Cost text
        //change the Skill text
    }
    public void LoadCoins()
    {

        _playerCoins = SaveLoadSystem.LoadGold();
        _displayCoins.text = "Your Gold : " + _playerCoins;

    }
    public void LoadSkills()
    {

        _statsLevel = SaveLoadSystem.LoadSkillsLevels();
        
    }
    public void SaveData()
    {
        
        SaveLoadSystem.SavePlayerData(_buyHealth[_statsLevel[0]], _buyDamage[_statsLevel[1]], _buyAtkSpd[_statsLevel[2]], _buyScatter[_statsLevel[3]]);
    }
    
    public void SaveGold()
    {
        SaveLoadSystem.SaveGold(_playerCoins,0);
    }

    public void SaveSkillLevel()
    {
        SaveLoadSystem.SaveSkillsLevels(_statsLevel);
    }

}
