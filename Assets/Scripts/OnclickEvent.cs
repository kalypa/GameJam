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
    public GameObject scorePanel = null;
    public GameObject gameOverPanel = null;
    public void Start()
    {
        gameTitle.SetActive(true);
        startButton.SetActive(true);
        settingButton.SetActive(true);
        shopButton.SetActive(true);
        settingPanel.SetActive(false);
        scorePanel.SetActive(false);
    }



    public void OnClickStart()
    {
        gameTitle.SetActive(false);
        startButton.SetActive(false);
        settingButton.SetActive(false);
        shopButton.SetActive(false);
        staminaBar.SetActive(true);
        scorePanel.SetActive(true);
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
    public void OnClickRetry()
    {
        Player.Instance.animator.SetTrigger("isStart");
        Player.Instance.isGameOver = false;
        StaminaBar.Instance.Spd = 0.05f;
        Player.Instance.slashCount = 0;
        TowerSpawner.Instance.count = 0;
        TowerSpawner.Instance.height = 2.8f;
        gameOverPanel.SetActive(false);
        Score.Instance.score = 0;
        for (int i = 0; i < TowerSpawner.Instance.towerList.Count; i++)
        {
            PoolManager.ReturnObject(TowerSpawner.Instance.towerList[i]);
        }
            TowerSpawner.Instance.towerList.Clear();
        if(TowerSpawner.Instance.bossList.Count != 0)
        {
            PoolManager.ReturnBoss(TowerSpawner.Instance.bossList[0]);
            TowerSpawner.Instance.bossList.Remove(TowerSpawner.Instance.bossList[0]);
        }
        OnClickStart();
    }
    public void OnClickLobby()
    {
        Player.Instance.isGameOver = false;
        StaminaBar.Instance.Spd = 0.05f;
        Player.Instance.animator.SetTrigger("isStart");
        gameOverPanel.SetActive(false);
        gameTitle.SetActive(true);
        startButton.SetActive(true);
        settingButton.SetActive(true);
        shopButton.SetActive(true);
        settingPanel.SetActive(false);
        scorePanel.SetActive(false);
        staminaBar.SetActive(false);
        Player.Instance.slashCount = 0;
        TowerSpawner.Instance.count = 0;
        Score.Instance.score = 0;
        for (int i = 0; i < TowerSpawner.Instance.towerList.Count; i++)
        {
            PoolManager.ReturnObject(TowerSpawner.Instance.towerList[i]);
        }
        TowerSpawner.Instance.towerList.Clear();
        if (TowerSpawner.Instance.bossList.Count != 0)
        {
            PoolManager.ReturnBoss(TowerSpawner.Instance.bossList[0]);
            TowerSpawner.Instance.bossList.Remove(TowerSpawner.Instance.bossList[0]);
        }
    }
}
