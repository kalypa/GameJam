using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopEvent : MonoBehaviour
{
    public GameObject weaponCategory;
    public GameObject statusCategory;
    public GameObject statusButton;
    public GameObject weaponButton;


    public void OnclickWeaponButton()
    {
        statusCategory.SetActive(false);
        weaponCategory.SetActive(true);
    }
    public void OnclickStatusButton()
    {
        weaponCategory.SetActive(false);
        statusCategory.SetActive(true);
    }
}
