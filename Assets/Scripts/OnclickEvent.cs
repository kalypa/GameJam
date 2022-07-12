using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OnclickEvent : MonoBehaviour
{
    public GameObject gameTitle = null; // GameTitle이 들어갈 빈 오브젝트
    public GameObject startButton = null; // 스타트버튼
    public GameObject quitButton = null;
    public GameObject settingButton = null;
    public GameObject shopButton = null;
    public GameObject settingPanel = null;
    public GameObject staminaBar = null;
    public GameObject Shop = null;
    public GameObject weaponCt = null;
    public GameObject statusCt = null;
    public void Start()
    {
        gameTitle.SetActive(true);
        startButton.SetActive(true);
        settingButton.SetActive(true);
        shopButton.SetActive(true);
        settingPanel.SetActive(false);  

    }



    public void OnClickStart()
    {
        gameTitle.SetActive(false);
        startButton.SetActive(false);
        settingButton.SetActive(false);
        shopButton.SetActive(false);
        staminaBar.SetActive(true);
        TowerSpawner.Instance.SpawnFirst();
        TowerSpawner.Instance.Spawn();

    }
    public void OnClickSetting()
    {
        settingPanel.SetActive(true);
        
        
    }
    public void OnCloseSetting()
    {
        settingPanel.SetActive(false);
    }
    public void OnClickQuit()
    {
    #if UNITY_STANDALONE
        Application.Quit();
    #endif
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #endif

    }
    public void OnClickShop()
    {
        Shop.SetActive(true);
        Time.timeScale = 0;
    }
    public void OnclickReload()
    {
        Shop.SetActive(false);
        Time.timeScale = 1;
    }
    public void OnClickStatus()
    {
        statusCt.SetActive(true);
        weaponCt.SetActive(false);
    }
    public void OnClickWeapon()
    {
        statusCt.SetActive(false);
        weaponCt.SetActive(true);
    }
}
