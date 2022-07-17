using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//아이템 데이터 구조체
[System.Serializable]
public struct ItemData
{

    //이름
    public string name;
    //컨셉 설명
    [TextArea(3, 3)]
    public string explain;
    //효과
    public string effect;
    //아이콘
    public Sprite Icon;
}


public class ShopManager : MonoBehaviour
{
    //패널
    [SerializeField]
    private RawImage panelIn;
    //제목
    [SerializeField]
    private TextMeshProUGUI title;
    //효과
    [SerializeField]
    private TextMeshProUGUI effect;
    //설정
    [SerializeField]
    private TextMeshProUGUI explain;
    //가격 텍스트
    [SerializeField]
    private TextMeshProUGUI sellTextpro;
    //아이콘 이미지
    [SerializeField]
    private Image iconImage;

    //버튼 스프라이트 뭉치
    [SerializeField]
    private Sprite[] buttonSprite;
    //버튼 배열
    [SerializeField]
    private Button[] itemButton;
    //아이템 가격
    [SerializeField]
    private int[] itemMoney;

    //아이템 데이터 배열
    [SerializeField]
    public ItemData[] itemData;

    //HP레벨 업
    [SerializeField]
    private TextMeshProUGUI hpLevel;
    //두 번째 사진 
    [SerializeField]
    private TextMeshProUGUI goldHP;

    [SerializeField]
    private StaminaBar staminaBar;
    public bool clear = false;

    public int playerStatusCoin = 0;

    void Start()
    {
        if (GameManager.Instance.playerData.itemBuyData[0] == false)
        {
            GameManager.Instance.playerData.itemUseData[0] = true;
        }
        GameManager.Instance.playerData.itemBuyData[0] = true;
        ShopUpdate();

    }

    public void SetExplain(int index)
    {
        panelIn.gameObject.SetActive(true);
        GameManager.Instance.Save();
        ReloadExplain(index);
    }

    public void ReloadExplain(int index)
    {
        title.text = itemData[index].name;
        effect.text = itemData[index].effect;
        explain.text = itemData[index].explain;
        sellTextpro.text = itemMoney[index].ToString();
        iconImage.sprite = itemData[index].Icon;
        GameManager.Instance.Save();
    }

    public void FalseExplain()
    {
        panelIn.gameObject.SetActive(false);
    }

    public void ShopUpdate()
    {
        for (int i = 0; i < GameManager.Instance.playerData.itemBuyData.Length; i++)
        {
            itemButton[i].image.sprite = buttonSprite[0];
            if (GameManager.Instance.playerData.itemBuyData[i] == true && GameManager.Instance.playerData.itemUseData[i] == false)
            {
                itemButton[i].image.sprite = buttonSprite[1];
            }
            if (GameManager.Instance.playerData.itemUseData[i] == true)
            {
                itemButton[i].image.sprite = buttonSprite[2];
            }

        }
        playerStatusCoin = GameManager.Instance.playerData.statusLevel + GameManager.Instance.playerData.statusLevel / 2;
        hpLevel.text = "Gold Lv." + GameManager.Instance.playerData.statusLevel;
        goldHP.text = playerStatusCoin.ToString();
        GameManager.Instance.Save();
    }

    public void Buy(int itemIndex)
    {
        if (GameManager.Instance.playerData.itemBuyData[itemIndex] == false)
        {
            if (GameManager.Instance.playerData.playerMoney >= itemMoney[itemIndex])
            {
                Player.Instance.goldText.text = GameManager.Instance.playerData.playerMoney.ToString();
                OnclickEvent.Instance.ButtonaudioSource.PlayOneShot(OnclickEvent.Instance.buyClip);
                GameManager.Instance.playerData.playerMoney -= itemMoney[itemIndex];
                GameManager.Instance.playerData.itemBuyData[itemIndex] = true;
                GameManager.Instance.Save();
                ShopUpdate();
            }
            else { }
        }
    }

    public void Use(int itemIndex)
    {
        if (GameManager.Instance.playerData.itemBuyData[itemIndex] == true && GameManager.Instance.playerData.itemUseData[itemIndex] == false && clear == false)
        {
            for (int i = 0; i < GameManager.Instance.playerData.itemUseData.Length; i++)
            {
                GameManager.Instance.playerData.itemUseData[i] = false;
            }
            Debug.Log(GameManager.Instance.playerData.itemUseData[0]);
            GameManager.Instance.playerData.itemUseData[itemIndex] = true;
            Debug.Log(GameManager.Instance.playerData.itemUseData[itemIndex]);
        }
        GameManager.Instance.Save();
        clear = false;
        ShopUpdate();
    }

    public void Clear(int itemIndex)
    {

        if (GameManager.Instance.playerData.itemUseData[itemIndex] == true && GameManager.Instance.playerData.itemBuyData[itemIndex] == true)
        {
            clear = true;
            for (int i = 0; i < GameManager.Instance.playerData.itemUseData.Length; i++)
            {
                GameManager.Instance.playerData.itemUseData[i] = false;
            }
            GameManager.Instance.playerData.itemUseData[0] = true;
            GameManager.Instance.Save();
            ShopUpdate();
        }
    }

    public void LevelUpStatus()
    {
        if (GameManager.Instance.playerData.playerMoney >= playerStatusCoin)
        {
            Player.Instance.goldText.text = GameManager.Instance.playerData.playerMoney.ToString();
            OnclickEvent.Instance.ButtonaudioSource.PlayOneShot(OnclickEvent.Instance.buyClip);
            GameManager.Instance.playerData.playerMoney -= playerStatusCoin;
            GameManager.Instance.playerData.statusLevel += 1;
            GameManager.Instance.Save();
            GameManager.Instance.playerData.value += 1;
            ShopUpdate();
        }
        else { }
    }

}

