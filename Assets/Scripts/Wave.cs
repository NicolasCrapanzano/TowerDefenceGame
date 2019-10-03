using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Wave
{
    private string name;//name of enemy
    private Transform enemy;//the enemy that is going to spawn
    private int count;
    private float rate; //the rate of enemies spawning

    public void SetName(string _name)
    {
        name = _name;
    }
    public void SetType(Transform _enemy)
    {
        enemy = _enemy;
    }
    public void SetAmmountAndRate (int _count,float _rate)
    {
        count = _count;
        rate = _rate;
    }


    public string GetName()
    {
        return name;
    }
    public Transform GetType()
    {
        return enemy;
    }
    public int GetAmmount()
    {
        return count;
    }
    public float GetRate()
    {
        return rate;
    }
}
