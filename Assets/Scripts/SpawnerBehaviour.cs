using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerBehaviour : MonoBehaviour
{
    [SerializeField]
    private float _timer;
    [SerializeField]
    private GameObject[] _enemies;
    private bool _isActive = false;
    //set an ammount of waves and an ammount of enemies on each wave, when a wave ends there is a delay until the next starts
   

    public void StartSpawner(bool active,float spRate= 5)
    {
        _isActive = active;
        _timer = Time.time + spRate;
    }
    void Update()
    {
        if(_isActive == true && _timer <= Time.time)
        {
            //instatiate an enemy
            _timer = Time.time + 5;
            Instantiate(_enemies[Random.Range(0,_enemies.Length)],transform.position,Quaternion.identity);
        }
    }
}
