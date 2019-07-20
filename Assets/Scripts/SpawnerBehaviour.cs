using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerBehaviour : MonoBehaviour
{
    [SerializeField]
    private float _timer;
    [SerializeField]
    private GameObject[] _enemies;
    //set an ammount of waves and an ammount of enemies on each wave, when a wave ends there is a delay until the next starts
    void Start()
    {
        _timer = Time.time + 5;
    }

    
    void Update()
    {
        if(_timer <= Time.time)
        {
            //instatiate an enemy
            _timer = Time.time + 5;
            Instantiate(_enemies[0],transform.position,Quaternion.identity);
        }
    }
}
