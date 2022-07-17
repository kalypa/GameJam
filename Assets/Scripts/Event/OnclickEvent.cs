using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;
public class OnclickEvent : MonoSingleton<OnclickEvent>
{
    public GameObject shopBackGround = null;
    public GameObject tutorialField;
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
    public bool isStart = false;
    public bool isRestart = false;
    public AudioSource ButtonaudioSource;
    public AudioClip buyClip;
    public AudioClip buttonSound;
    public GameObject quitPanel;
    public GameObject weaponButton;
    public GameObject goldButton;
    public bool isLeft = false;
    public bool isRight = false;
    public Sprite grayWeapon;
    public Sprite grayGold;
    public Sprite originWeapon;
    public Sprite originGold;
    public void Start()
    {
        Player.Instance.goldText.text = GameManager.Instance.playerData.playerMoney.ToString();
        Player.Instance.lobbyPlayer.gameObject.SetActive(true);
        gameTitle.SetActive(true);
        startButton.SetActive(true);
        settingButton.SetActive(true);
        shopButton.SetActive(true);
        settingPanel.SetActive(false);
        scorePanel.SetActive(false);
    }
    void Update()
    {
        Player.Instance.goldText.text = GameManager.Instance.playerData.playerMoney.ToString();
        if(Shop.activeSelf == true)
        {
            if(weaponCt.activeSelf == false)
            {
                weaponButton.GetComponent<Image>().sprite = grayWeapon;
                goldButton.GetComponent<Image>().sprite = originGold;
            }
            else if(statusCt.activeSelf == false)
            {
                goldButton.GetComponent<Image>().sprite = grayGold;
                weaponButton.GetComponent<Image>().sprite = originWeapon;
            }
        }
    }
    public void OnClickStart()
    {
        Player.Instance.backGroundSound.clip = Player.Instance.backGroundClip[0];
        Player.Instance.backGroundSound.Play();
        ButtonaudioSource.PlayOneShot(buttonSound);
        Player.Instance.IdleState();
        Player.Instance.transform.position = new Vector3(0, -3, -8);
        isStart = true;
        Player.Instance.lobbyPlayer.gameObject.SetActive(false);
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
        ButtonaudioSource.PlayOneShot(buttonSound);
        settingPanel.SetActive(true);
        
        
    }
    public void OnCloseSetting()
    {
        ButtonaudioSource.PlayOneShot(buttonSound);
        settingPanel.SetActive(false);
    }
    public void OnClickQuit()
    {
        ButtonaudioSource.PlayOneShot(buttonSound);
        quitPanel.SetActive(true);
    }
    public void OnClickYes()
    {
        ButtonaudioSource.PlayOneShot(buttonSound);
        Application.Quit();
    }
    public void OnClickNo()
    {
        ButtonaudioSource.PlayOneShot(buttonSound);
        quitPanel.SetActive(false);
    }
    public void OnClickShop()
    {
        Player.Instance.lobbyPlayer.gameObject.SetActive(false);
        gameTitle.SetActive(false);
        startButton.SetActive(false);
        settingButton.SetActive(false);
        shopButton.SetActive(false);
        shopBackGround.SetActive(true);
        ButtonaudioSource.PlayOneShot(buttonSound);
        Player.Instance.lobbyPlayer.gameObject.SetActive(false);
        Shop.SetActive(true);
        Time.timeScale = 0;
    }
    public void OnclickReload()
    {
        Player.Instance.lobbyPlayer.gameObject.SetActive(true);
        gameTitle.SetActive(true);
        startButton.SetActive(true);
        settingButton.SetActive(true);
        shopButton.SetActive(true);
        shopBackGround.SetActive(false);
        ButtonaudioSource.PlayOneShot(buttonSound);
        Player.Instance.lobbyPlayer.gameObject.SetActive(true);
        Shop.SetActive(false);
        Time.timeScale = 1;
    }
    public void OnClickStatus()
    {
        ButtonaudioSource.PlayOneShot(buttonSound);
        statusCt.SetActive(true);
        weaponCt.SetActive(false);
    }
    public void OnClickWeapon()
    {
        ButtonaudioSource.PlayOneShot(buttonSound);
        statusCt.SetActive(false);
        weaponCt.SetActive(true);
    }
    public void OnClickRetry()
    {
        Player.Instance.isFirstStart = false;
        ButtonaudioSource.PlayOneShot(buttonSound);
        isRestart = true;
        for (int i = 0; i < Player.Instance.backGroundSprite.Length; i++)
        {
            Player.Instance.backGroundSprite[i].gameObject.SetActive(true);
            Player.Instance.backGroundSprite[i].color = new Color(1, 1, 1, 1);
            Player.Instance.backGroundSound.Stop();
            Player.Instance.backGroundSound.clip = Player.Instance.backGroundClip[0];
            Player.Instance.backGroundSound.Play();
        }
        isStart = true;
        Player.Instance.bossCount = 0;
        Player.Instance.isGameOver = false;
        StaminaBar.Instance.Spd = 0.05f;
        StaminaBar.Instance.staminaBar.value = 0;
        Player.Instance.slashCount = 0;
        TowerSpawner.Instance.count = 0;
        TowerSpawner.Instance.height = 2.8f;
        gameOverPanel.SetActive(false);
        Score.Instance.score = 0;
        for (int i = 0; i < TowerSpawner.Instance.towerList.Count; i++)
        {
            PoolManager.ReturnObject(TowerSpawner.Instance.towerList[i]);
            TowerSpawner.Instance.towerList[i].GetComponentInChildren<SlashDir>().transform.eulerAngles = new Vector3(0, 0, 0);
        }
        TowerSpawner.Instance.towerList.Clear();
        ReturnBoss();
        OnClickStart();
    }
    public void OnClickLobby()
    {
        Player.Instance.isFirstStart = false;
        ButtonaudioSource.PlayOneShot(buttonSound);
        for (int i = 0; i < Player.Instance.backGroundSprite.Length; i++)
        {
            Player.Instance.backGroundSprite[i].gameObject.SetActive(true);
            Player.Instance.backGroundSprite[i].color = new Color(1, 1, 1, 1);
            Player.Instance.backGroundSound.Stop();
            Player.Instance.backGroundSound.clip = Player.Instance.backGroundClip[0];
            Player.Instance.backGroundSound.Play();
        }
        Player.Instance.IdleState();
        Player.Instance.transform.position = new Vector3(0, -3, 5);
        Player.Instance.animator.SetBool("isStart", true);
        isStart = false;
        TowerSpawner.Instance.height = 2.8f;
        Player.Instance.isGameOver = false;
        Player.Instance.bossCount = 0;
        StaminaBar.Instance.Spd = 0.05f;
        StaminaBar.Instance.staminaBar.value = 0;
        Player.Instance.lobbyPlayer.gameObject.SetActive(true);
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
            TowerSpawner.Instance.towerList[i].GetComponentInChildren<SlashDir>().transform.eulerAngles = new Vector3(0, 0, 0);
        }
        TowerSpawner.Instance.towerList.Clear();
        ReturnBoss();
    }
    public void ReturnBoss()
    {
        ButtonaudioSource.PlayOneShot(buttonSound);
        if (TowerSpawner.Instance.bossList.Count != 0)
        {
            TowerSpawner.Instance.hp = 5;
            Player.Instance.slashBossCount = 0;
            for (int i = 0; i < 5; i++)
            {
                Player.Instance.slashDirs[i].gameObject.SetActive(true);
                Player.Instance.randomDir = Random.Range(1, 100);
                if (Player.Instance.randomDir < 50)
                {
                    Player.Instance.slashDirs[i].transform.eulerAngles = new Vector3(0, 0, 90);
                }
                else
                {
                    Player.Instance.slashDirs[i].transform.eulerAngles = new Vector3(0, 0, 0);
                }
            }
            PoolManager.ReturnBoss(TowerSpawner.Instance.bossList[0]);
            for (int i = 0; i < TowerSpawner.Instance.bossList[0].GetComponentsInChildren<SlashDir>().Length; i++)
            {
                TowerSpawner.Instance.bossList[0].GetComponentsInChildren<SlashDir>()[i].transform.eulerAngles = new Vector3(0, 0, 0);
            }
            TowerSpawner.Instance.bossList.Remove(TowerSpawner.Instance.bossList[0]);
        }
    }
}
