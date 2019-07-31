using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBehaviour : MonoBehaviour
{
    private int _health = 1; // health of the tower
    private EnemyBehaviour _enemyHittingThis;
    private SpriteRenderer _sr;
    private Color _nativeColor;
    void Start()
    {
        _sr = GetComponent<SpriteRenderer>();
        _nativeColor = _sr.color;
        gameObject.layer = 10;
    }

    void Update()
    {
        if(transform.parent == null)
        {
            gameObject.layer = 0;
        }
    }
    private void DoDamage()
    {

    }
    private void GetDamage(int dmg) //call this from the enemie to do damage to the tower
    {

        _health -= dmg;
        if (_health <= 0)
        {
            if(_enemyHittingThis != null)
            {
                _enemyHittingThis.SendMessage("StopAttack");
            }
            Destroy(this.gameObject);
        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            
            Debug.Log("i got an enemy");
            _enemyHittingThis = collision.gameObject.GetComponent<EnemyBehaviour>();
            Debug.Log(_enemyHittingThis);

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            _sr.color = _nativeColor;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _sr.color = new Color(_nativeColor.r, _nativeColor.g, _nativeColor.b, 0.3f);
    }
}
