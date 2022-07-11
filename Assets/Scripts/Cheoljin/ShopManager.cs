using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoSingleton<ShopManager>
{
    [Header("사용 될 아이템")]
    public ShopItem[] shopItemArray;

    [Header("사용할 패널")]
    public GameObject panel;

    public GameObject shopScroll;

    public void ShopBuy()
    {

    }

    public void ShopUse()
    {
            
    }

    void ShopInstance(ShopItem item)
    {
        GameObject currentPanel = Instantiate(panel,shopScroll.transform.parent);
        currentPanel.GetComponents<Text>()[0].text = item.itemName;
        currentPanel.GetComponents<Text>()[1].text = item.itemmoney.ToString();
        currentPanel.GetComponent<Image>().sprite = item.itemSprite;
    }


}
