using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CoinPopUp : MonoBehaviour
{
    private Transform _transform;
    private Text _coinText;

    private void CreateObject(int reward)
    {

        _coinText = GetComponentInChildren<Text>();
        _coinText.text = "+ " + reward;
        _coinText.color = new Color(1,1,0,1);
        _transform = GetComponent<Transform>();

    }
    private void SetText(string txt)
    {
        _coinText = GetComponentInChildren<Text>();
        _coinText.text = txt;
        _coinText.color = new Color(1, 1, 0, 1);
        _transform = GetComponent<Transform>();
    }
    void Update()
    {
        _coinText.color = _coinText.color - new Color(0,0,0,0.015f);
        _transform.position = _transform.position +new Vector3(0,0.06f,0);
        if(_coinText.color.a <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
