using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    [SerializeField]
    private float _speed;
    private EnemyBehaviour _enemy;
    void Start()
    {
        Destroy(this.gameObject,2);
    }


    void Update()
    {
        transform.Translate(Vector2.up * _speed * Time.deltaTime);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision);
        if (collision.gameObject.CompareTag("Enemy"))
        {
            _enemy = collision.gameObject.GetComponent<EnemyBehaviour>();
            _enemy.SendMessage("RecieveDamage", 1);
            Destroy(this.gameObject);
        }
    }
}
