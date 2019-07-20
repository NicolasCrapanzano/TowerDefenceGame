using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    private Transform _target;
    private PlayerBehaviour _player;
    private GameManager _gm;
    private Rigidbody2D _rb;
    [SerializeField]
    private float _speed,_atkDelay,_timer;
    private bool _move,_playerReached;
    [SerializeField]
    private int _health;
    void Start()
    {
        _player = FindObjectOfType<PlayerBehaviour>();
        _target = _player.GetComponent<Transform>();
        _rb = GetComponent<Rigidbody2D>();
        _gm = FindObjectOfType<GameManager>();
        _move = true;
        _playerReached = false;
        _health = 1;
    }


    void Update()
    {
        if (_move == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, _target.position, _speed * Time.deltaTime); //go to the position of the player
        }
        if(_playerReached == true && _timer <= Time.time) //this prevents that the enemies dont detect the collision w the player
        {
            _timer = Time.time + 0.5f;
            _rb.WakeUp();
            Debug.Log("Wake up!");
        }
    }


    private void OnCollisionStay2D(Collision2D other)
    {
        Debug.Log(other);
        if(other.gameObject.CompareTag("Player")) //stop & attack
        {
            if(_playerReached == false)
            {
                _player = other.gameObject.GetComponent<PlayerBehaviour>();
                _playerReached = true;
                _atkDelay = Time.time + 0.5f;
                Attacking();
               
            }else
            {
                Attacking();
            }   
            _move = false;
            Debug.Log("stop");
            
        }

    }

    private void RecieveDamage(int dmg)
    {
        //Debug.Log(name + "recieved " + dmg + " damage");
        _health -= dmg;
        if (_health <= 0)
        {
            _gm.SendMessage("CoinsCounter", 1);//send coins to the gamemanager
            Debug.Log("He need some milk");
            Destroy(this.gameObject);
        }
    }

    private void Attacking()
    {        
        if(_atkDelay <= Time.time)
        {
            _atkDelay = Time.time + 2;
            _player.SendMessage("RecieveDamage", 1);
        }
    }
    
}
