using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorBehaviour : MonoBehaviour
{
    private Vector2 _mousePos;
    void Start()
    {
        //Cursor.visible = false;
        //check if the mouse leaved the screen and re set this !!
    }

    
    void Update()
    {
        _mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        this.transform.position = _mousePos;
    }

}
