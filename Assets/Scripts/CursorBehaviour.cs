﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorBehaviour : MonoBehaviour
{
    private Rigidbody2D _rb;
    private Vector2 _mousePos;

    private bool _isTowerInHand = false,_inBuildZone=false;
    private GameObject _child;
    [SerializeField]
    private GameObject _torretest, _ancla;
    void Start()
    {

        _rb = GetComponent<Rigidbody2D>();

    }

    void Update()
    {
        _mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        this.transform.position = _mousePos;
        if (_isTowerInHand == true &&_inBuildZone == true && Input.GetMouseButtonDown(0))
        {

            _child.transform.parent = null;
            _isTowerInHand = false;

        }
        if (_isTowerInHand == true && Input.GetKeyDown(KeyCode.R))
        {
            _child.transform.rotation = Quaternion.Euler(_child.transform.eulerAngles + new Vector3(0,0,45));
        }
        if (_rb.IsSleeping()) //if mouse enter sleep mode wake up
        {

            _rb.WakeUp();

        }

        if (Cursor.visible == true) // if cursor went visible example: minimize de game and open again make the cursor invisible
        {

            Cursor.visible = false;

        }

    }

    private void NewTower(int id = 0)
    {
        Debug.Log("entered");
        if (id == 1)
        {
            //you have a tower now
            _isTowerInHand = true;
            Debug.Log("tower added");
            _child = Instantiate(_torretest, _ancla.transform.position, Quaternion.identity);
            _child.transform.SetParent(this.gameObject.transform);
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("BuildZone"))
        {
            _inBuildZone = true;
            Debug.Log("build : " + _inBuildZone);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        _inBuildZone = false;
        Debug.Log("build : " + _inBuildZone);
    }
}