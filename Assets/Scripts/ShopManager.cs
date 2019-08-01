using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    [SerializeField]
    private Text _displayCoins;
    private bool test=true;

    // SKILLS LEVELS
    private int[] _statsLevel = new int[] {0,0,0,0 }; //each slot is a different stat level order : { health, damage, spd, scatter }

    // PLAYER STATS
    private int[] _buyDamage = new int[] {1,2,3 }; // 3 "levels" lvl0 = 1 dmg ; lvl1 = 2dmg ; lvl2 = 3dmg ;
    private int[] _damageCost = new int[] { 10, 20, 30 };

    private int[] _buyHealth = new int[] {3,4,5 }; // 3 "levels" lvl0 = 3 hp ; lvl1 = 4 hp ; lvl2 = 5 hp ;
    private int[] _healthCost = new int[] { 15, 25, 35 };

    private float[] _buyAtkSpd = new float[] {2f,1.5f,1f,0.75f }; // 4 "levels" lvl0 = 2 spd ; lvl1 = 1.5 spd ; lvl2 = 1 spd ; lvl3 = 0.75 spd;
    private int[] _atkSpdCost = new int[] { 10, 20, 30 };

    private float[] _buyScatter = new float[] {8f,7f,6f,5f }; //  5 "Levels" lvl0 = 8f ; lvl1 = 7f ; lvl2 = 6 ; lvl3 = 5 ; lvl4 = 5 ;
    private int[] _scatterCost = new int[] { 12, 24, 36 };

    // WORLD STATS
    private int _playerCoins;

    public void Shop()
    {

    }
    public void HowManyCoins()
    {

        _playerCoins = SaveLoadSystem.LoadGold();
        _displayCoins.text = "Your Gold : " + _playerCoins;

    }
    private void LoadData()
    {
        //ask the load system for the _statsLevel saves
        if(test==false)
        {
            //load the stuff
        }else //there isnt any save then default values are 0
        {
            Debug.Log("No file detectd, loading default values. . .");
            SaveLoadSystem.SavePlayerData(_buyHealth[_statsLevel[0]],_buyDamage[_statsLevel[1]],_buyAtkSpd[_statsLevel[2]],_buyScatter[_statsLevel[3]]); //order health damage spd scatter
            
        }
        
    }
    public void SaveData()
    {
        SaveLoadSystem.SavePlayerData(_buyHealth[_statsLevel[0]], _buyDamage[_statsLevel[1]], _buyAtkSpd[_statsLevel[2]], _buyScatter[_statsLevel[3]]);
    }
    
    public void SaveGold()
    {
        SaveLoadSystem.SaveGold(_playerCoins);
    }

    public void SaveSkillLevel()
    {
        SaveLoadSystem.SaveSkillsLevels(_statsLevel);
    }
    //PROVISIONAL !!!!!!!!!
    public void firstlvl()
    {
        LoadData();
    }
}
