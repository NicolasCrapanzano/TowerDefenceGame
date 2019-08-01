using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField]
    private int _health,_damage;
    private float _shotSpd, _shotScatter;
    private GameManager _gm;

    void Start()
    {
        
        _gm = FindObjectOfType<GameManager>();

        SavedStats();
    }
    private void SavedStats()
    {
        //call save sistem and take data
        float[] stats;
        stats = SaveLoadSystem.LoadPlayerData();
        Debug.Log("Player recieved " + stats[0] + " " + stats[1] + " " + stats[2] + " " + stats[3] + " " + " stats. . .");
        _health = (int)stats[0];
        _damage = (int)stats[1];
        _shotSpd = stats[2];
        _shotScatter = stats[3];

        BulletBehaviour.Load(_damage);
        FindObjectOfType<GunControl>().Load(_shotSpd, _shotScatter);
    }

    void Update()
    {
        
    }
    private void RecieveDamage(int dmg)
    {
        
        Debug.Log("recieved " + dmg + " damage");
        _health -= dmg;
        _gm.SendMessage("HealthDisplay", _health);
        if(_health <= 0)
        {
            Debug.Log(" W A S T E D");
        }
    }
}
