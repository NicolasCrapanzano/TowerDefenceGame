﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunControl : MonoBehaviour
{
    [SerializeField]
    private float offset,_timeBtwShots,_shotSpd,_shotExtraRotation;
    [SerializeField]
    private GameObject _bullet,_cannonHole;
    private Quaternion _bulletRot;
    private Vector3 _rot;
    void Start()
    {
        
    }


    void Update()
    {
        GunControler();
    }
    private void GunControler()
    {
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ + offset);
        if(Input.GetMouseButtonDown(0)&&_timeBtwShots <= Time.time)
        {
            _rot = transform.rotation.eulerAngles + new Vector3(0, 0, Random.Range(-_shotExtraRotation, _shotExtraRotation));//set the rotation that the bullet is going to move
            _bulletRot = Quaternion.Euler(_rot);
            _timeBtwShots = Time.time + _shotSpd;
            Instantiate(_bullet,_cannonHole.transform.position,_bulletRot);
        }
    }
   
}