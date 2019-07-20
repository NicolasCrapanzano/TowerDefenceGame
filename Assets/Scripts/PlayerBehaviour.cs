using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField]
    private int _health;
    private GameManager _gm;
    void Start()
    {
        _health = 3;
        _gm = FindObjectOfType<GameManager>();
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
