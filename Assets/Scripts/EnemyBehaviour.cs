using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    private Transform _target;
    private PlayerBehaviour _player;
    private TowerBehaviour _tower;
    private GameManager _gm;
    private Rigidbody2D _rb;
    [SerializeField]
    private float _speed,_atkDelay,_timer;
    private bool _move, _somethingReached;
    [SerializeField]
    private int _health=1,_damage=1,_reward=1;
    void Start()
    {

        _player = FindObjectOfType<PlayerBehaviour>();
        _target = _player.GetComponent<Transform>();
        _rb = GetComponent<Rigidbody2D>();
        _gm = FindObjectOfType<GameManager>();
        _move = true;
        _somethingReached = false;
        
    }


    void Update()
    {
        if (_move == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, _target.position, _speed * Time.deltaTime); //go to the position of the player
        }
        if(_somethingReached == true && _timer <= Time.time) //this prevents that the enemies dont detect the collision w the player
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
            if(_somethingReached == false)
            {
                _player = other.gameObject.GetComponent<PlayerBehaviour>();
                _somethingReached = true;
                _atkDelay = Time.time + 0.5f;
                Attacking(0);
               
            }else
            {
                Attacking(0);
            }   
            _move = false;
            Debug.Log("stop");
            
        }

        if(other.gameObject.CompareTag("Wall"))
        {
            if(_somethingReached == false)
            {

                _tower = other.gameObject.GetComponent<TowerBehaviour>();
                _somethingReached = true;
                _atkDelay = Time.time + 0.5f;
                Attacking(1);

            }
            else
            {

                Attacking(1);

            }
            _move = false;
        }
    }

    private void RecieveDamage(int dmg)
    {
        //Debug.Log(name + "recieved " + dmg + " damage");
        _health -= dmg;
        if (_health <= 0)
        {
            _gm.SendMessage("CoinsCounter", _reward);//send coins to the gamemanager
            Debug.Log("He need some milk");
            Destroy(this.gameObject);
        }
    }
    private void StopAttack()
    {
        StartCoroutine(ContinueMove());
        
    }
    private IEnumerator ContinueMove()
    {
        yield return new WaitForSeconds(0.2f);
        Debug.Log(this.name + " Continue moving . . .");
        _move = true;
    }
    private void Attacking(int id) // id = 0 player -- id = 1 wall
    {        
        if(_atkDelay <= Time.time)
        {
            _atkDelay = Time.time + 2;
            if (id == 0)
            {
                _player.SendMessage("RecieveDamage", _damage);
            }
            else if(id == 1)
            {
                _tower.SendMessage("GetDamage",_damage);
            }
        }
    }
    
}
