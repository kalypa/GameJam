using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ShopItemClass:MonoBehaviour
{

}

[System.Serializable]
public struct ShopItem
{
    //�ش� �������� ������
    public GameObject itemPrefab;
    //�ش� �������� �̸�
    public string itemName;
    //�ش� �������� ����
    public int itemmoney;
    //�ش� �������� ������
    public Sprite itemSprite;
}
