using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EnemyBehaviour : MonoBehaviour
{
    private Transform _target;
    private PlayerBehaviour _player;
    private TowerBehaviour _tower;
    private GameManager _gm;
    private Rigidbody2D _rb;
    private SpriteRenderer _sp;
    private Animator _anim;
    [SerializeField] private Image _healthBar;
    [SerializeField] private float _speed, _atkDelay, _timer, _damagedColorTimer;
    [SerializeField] private int _health = 1, _damage = 1, _reward = 1;
    [SerializeField] private GameObject _deathParticle,_coinPopUp;
    [SerializeField] private Animator _coinDisplayMove;
    private GameObject _pop;
    private bool _move, _somethingReached, _damagedColor = false;
    public bool _isAlive = true;
    private int _maxHealth;
    void Start()
    {

        _player = FindObjectOfType<PlayerBehaviour>();
        _target = _player.GetComponent<Transform>();
        _rb = GetComponent<Rigidbody2D>();
        _gm = FindObjectOfType<GameManager>();
        _sp = GetComponentInChildren<SpriteRenderer>();
        _anim = GetComponentInChildren<Animator>();
        _coinDisplayMove = GameObject.Find("CoinsCanvas").GetComponent<Animator>();
        _move = true;
        _somethingReached = false;
        _maxHealth = _health;
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
        if(_damagedColor = true &&_damagedColorTimer <= Time.time)
        {
            _sp.color = new Color(255,255,255);
            _damagedColor = false;
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
        _sp.color = new Color(255, 0, 0);
        if (_damagedColor == false)
        {
            _damagedColor = true;
            _damagedColorTimer = Time.time + 0.2f;
        }
        //Debug.Log(name + "recieved " + dmg + " damage");
        _health -= dmg;
        _healthBar.fillAmount = (float)_health / _maxHealth;
        if (_health <= 0)
        {

            _isAlive = false;
            _pop = Instantiate(_coinPopUp,transform.position,Quaternion.identity);
            _pop.SendMessage("CreateObject", _reward);

            _gm.SendMessage("CoinsCounter", _reward);//send coins to the gamemanager
            Instantiate(_deathParticle,transform.position,Quaternion.identity);
            _move = false;
            _anim.Play("Enemy1Damaged");
            
            Destroy(this.gameObject,0.3f);
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
