using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerShopManager : MonoBehaviour
{
    [SerializeField] private SpriteRenderer BuildZone;
    [SerializeField]private GameObject _popUp;
    private GameManager _gm;
    [SerializeField]private GameObject[] _towers;
    private CursorBehaviour _cursor;
    private bool _shopUpToDate=false; //check if this is false and update the data of the shop
    private int[] _towerCost = { 0,2,5,7 }; //0 is null dont use for a tower !!
    private int[] _towerHealth = { 0,1,2,3 };
    private int _coinsTemporal;
    public bool _hasTowerAlready = false;
    void Start()
    {
        _gm = FindObjectOfType<GameManager>();
        _cursor = FindObjectOfType<CursorBehaviour>();
        if (_shopUpToDate == false)
        {
            UpdateShopData();
        }
    }

    public void BuyTower(int id)
    {
        
        _coinsTemporal = _gm.HowManyCoins();
        switch (id)
        {
            case 1:
                if(_coinsTemporal >= _towerCost[id])
                {
                    EnableBuildZone(true);
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
                    NotEnoughMoney();
                }
                break;
            case 2:

                break;
            case 3:

                break;
        }
    }
    private void UpdateShopData() //there is 2 Child GameObjects before the texts  
    {
        for(int i = 1;i < _towers.Length; i++)
        {
            Text _newTempText;
            _newTempText = _towers[i].transform.GetChild(0).transform.GetChild(0).transform.GetChild(1).GetComponent<Text>(); //this changes the coin ammount
            _newTempText.text ="-" + _towerCost[i].ToString() + " Coins";
            _newTempText = _towers[i].transform.GetChild(0).transform.GetChild(0).transform.GetChild(2).GetComponent<Text>(); //this changes the stats ammount
            _newTempText.text =_towerHealth[i].ToString() + " Hp";
        }

        _shopUpToDate = true;
    }
    private void NotEnoughMoney()
    {
        Transform cursorPos = _cursor.GetComponent<Transform>();
        GameObject pop = Instantiate(_popUp,cursorPos.position,Quaternion.identity);
        pop.SendMessage("SetText","Not enough Money");
    }
    public void EnableBuildZone(bool state)
    {

        BuildZone.enabled = state;

    }
}
