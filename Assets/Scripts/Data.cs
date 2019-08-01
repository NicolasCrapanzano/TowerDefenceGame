using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Data 
{
    //player data
    private int _health, _damage;
    private float _shotSpd, _shotScatter;
    //world data
    private int _money, _lastLevel;
    //tower data
    private string data;

    public void PlayerData(int health, int damage, float shotspd, float shotscatter)
    {
        _health = health;
        _damage = damage;
        _shotSpd = shotspd;
        _shotScatter = shotscatter;
        
    }
}
