using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerShopManager : MonoBehaviour
{
    private GameManager _gm;
    private GameObject[] _towers;
    private int[] _towerCost = {2,5,7 };
    private int _coinsTemporal;
    void Start()
    {
        _gm = FindObjectOfType<GameManager>();   
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void BuyTower(int id)
    {
        _coinsTemporal = _gm.HowManyCoins();
        switch (id)
        {
            case 0:
                if(_coinsTemporal >= _towerCost[id])
                {
                    //put the tower inside the cursor (rotate with R)
                    //
                }
                break;
            case 1:
                break;
            case 2:
                break;
        }
    }
}
