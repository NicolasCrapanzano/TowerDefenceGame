using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunControl : MonoBehaviour
{
    [SerializeField] private float _maxTimer, _shotScatter;
    [SerializeField] private Image _nextShotDisplay;
    private float _timer, offset = -90;
    private bool _canShoot=true;
    [SerializeField] private GameObject _bullet, _cannonHole;
    private Quaternion _bulletRot;
    private Vector3 _rot;
    private Animator _anim;
    public void Load(float spd, float scatter)
    {
        _maxTimer = spd;
        _shotScatter = scatter;
        _anim = GetComponentInChildren<Animator>();
    }
    void Update()
    {
        if(_timer > 0)
        {
            _timer -= Time.deltaTime;
            _nextShotDisplay.fillAmount = _timer / _maxTimer;
        }
        GunControler();
    }
    private void GunControler()
    {
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ + offset);
        if (Input.GetMouseButtonDown(0)&& _canShoot == true && _timer <= 0)
        {
            _anim.Play("BowShot");
            _rot = transform.rotation.eulerAngles + new Vector3(0, 0, Random.Range(-_shotScatter, _shotScatter));//set the rotation that the bullet is going to move
            _bulletRot = Quaternion.Euler(_rot);
            _timer = _maxTimer;
            Instantiate(_bullet, _cannonHole.transform.position, _bulletRot);
        }
    }
    private void OnShop(bool canshoot)
    {
        _canShoot = canshoot;
    }
}
