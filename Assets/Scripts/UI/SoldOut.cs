using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldOut : MonoBehaviour
{
    [SerializeField]
    private GameObject[] soldOut;
    void Update()
    {
        if(OnclickEvent.Instance.Shop.activeSelf == true)
        {
            BuyWeapon();
        }
    }
    void BuyWeapon()
    {
        if(GameManager.Instance.playerData.itemBuyData[1] == true)
        {
            soldOut[0].SetActive(true);
        }
        else if (GameManager.Instance.playerData.itemBuyData[2] == true)
        {
            soldOut[1].SetActive(true);
        }
        else if (GameManager.Instance.playerData.itemBuyData[3] == true)
        {
            soldOut[2].SetActive(true);
        }
    }
}
