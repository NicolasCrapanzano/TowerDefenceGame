using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    [SerializeField]
    private float _speed;
    [SerializeField]
    private static int _damage = 1;
    private EnemyBehaviour _enemy;
    private Rigidbody2D _enemyRb;
    void Start()
    {
        Destroy(this.gameObject,2);
    }

    public static void Load(int dmg)
    {
        _damage = dmg;
    }
    void Update()
    {
        
        transform.Translate(Vector2.up * _speed * Time.deltaTime);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Enemy"))
        {

            _enemy = collision.gameObject.GetComponent<EnemyBehaviour>();
            _enemyRb = _enemy.GetComponent<Rigidbody2D>();
            _enemy.SendMessage("RecieveDamage", _damage);
            if (_enemy._isAlive == false)
            {
                
            }
            Destroy(this.gameObject);
        }
    }
}
