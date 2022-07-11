using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ShopItemClass:MonoBehaviour
{

}

[System.Serializable]
public struct ShopItem
{
    //해당 아이템의 프리팹
    public GameObject itemPrefab;
    //해당 아이템의 이름
    public string itemName;
    //해당 아이템의 가격
    public int itemmoney;
    //해당 아이템의 아이콘
    public Sprite itemSprite;
}
