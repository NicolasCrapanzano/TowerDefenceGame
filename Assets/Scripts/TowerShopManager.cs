using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerShopManager : MonoBehaviour
{
    private GameManager _gm;
    private GameObject[] _towers;
    private CursorBehaviour _cursor;
    private int[] _towerCost = { 0,2,5,7 };
    private int _coinsTemporal;
    public bool _hasTowerAlready = false;
    void Start()
    {
        _gm = FindObjectOfType<GameManager>();
        _cursor = FindObjectOfType<CursorBehaviour>();
    }

    public void BuyTower(int id)
    {
        _coinsTemporal = _gm.HowManyCoins();
        switch (id)
        {
            case 1:
                if(_coinsTemporal >= _towerCost[id])
                {
                    Debug.Log("tower" + id);
                    //put the tower inside the cursor (rotate with R)
                    _cursor.SendMessage("NewTower", id);
                    //disccount money from the "wallet"
                    _hasTowerAlready = true;
                    int disccount = _towerCost[id];
                    _gm.SendMessage("CoinsCounter",-disccount);
                }else
                {
                    Debug.Log("no money m8");
                }
                break;
            case 2:

                break;
            case 3:

                break;
        }
    }
}
