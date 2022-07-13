using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class Player : MonoSingleton<Player>
{
    [SerializeField]
    private Slider staminaBar; //스태미나(체력) 바
    [SerializeField]
    private StaminaBar stamina; //스태미나 바 코드 가져오기 위한 선언
    [SerializeField]
    private TowerSpawner towerSpawner; // 타워스포너를 가져오기 위한 선언
    [SerializeField]
    private AtkEffect atkEffect; //공격 이펙트
    [SerializeField]
    private CameraShake cameraShake; //카메라 흔들림
    [SerializeField]
    private Boss boss; //보스룸
    DragManager dragManager; //스와이프
    public Animator animator; //애니메이션
    public List<SlashDir> slashList = new List<SlashDir>(); //베야할 방향 리스트
    public SlashDir slashDir; //벨 방향
    public SlashDir[] slashDirs; //벨 방향 배열
    public int slashCount = 0; //벤 횟수
    public int slashBossCount = 0; //보스를 벤 횟수
    public int bossCount = 0;
    public float randomDir;
    public GameObject panel = null;
    public bool isGameOver = false;
    public Text ScoreText;
    public Text HighScoreText;
    public Text goldText;
    private float randomGold;
    [SerializeField]
    private Image shopPlayer;
    [SerializeField]
    private Sprite shopPlayer1;
    [SerializeField]
    private Sprite shopPlayer2;
    [SerializeField]
    private Sprite shopPlayer3;
    [SerializeField]
    private Sprite shopPlayer4;
    public Image lobbyPlayer;
    [SerializeField]
    private SpriteRenderer[] backGroundSprite;
    void Awake()
    {
        dragManager = GetComponent<DragManager>();
        animator = GetComponent<Animator>();
        dragManager.setOnSwipeDetected(MyOnSwipeDetected);
        CheckUse();
    }
    void Update()
    {
        CheckUse();
        BackGroundChange();
    }
    void MyOnSwipeDetected(Vector3 swipeDirection) // 스와이프 함수
    {
        if(isGameOver == false)
        {
            if(OnclickEvent.Instance.isStart == true)
            {
                if (panel.activeSelf == false)
                {
                    if (swipeDirection.x != 0 || swipeDirection.y != 0) // x좌표 혹은 y좌표가 0이 아니라면
                    {
                        if (Mathf.Abs(swipeDirection.x) > Mathf.Abs(swipeDirection.y)) //x의 절댓값이 y의 절댓값보다 크다면
                        {
                            if (slashCount != 50 + 50 * bossCount)
                            {
                                slashDir = towerSpawner.towerList[0].GetComponentInChildren<SlashDir>();
                                if (slashDir.transform.eulerAngles != new Vector3(0, 0, 0))
                                {
                                    Atk();
                                }
                                else
                                {
                                    Dead();
                                    GameOverDelay();
                                }
                            }
                            else if (slashCount == 50 + 50 * bossCount)
                            {
                                if (slashDirs[slashBossCount].transform.eulerAngles != new Vector3(0, 0, 0))
                                {
                                    AtkBoss();
                                }
                                else
                                {
                                    Dead();
                                    GameOverDelay();
                                }
                            }
                        }
                        else if (Mathf.Abs(swipeDirection.y) > Mathf.Abs(swipeDirection.x))//y의 절댓값이 x의 절댓값보다 크다면 
                        {
                            if (slashCount != 50 + 50 * bossCount)
                            {
                                slashDir = towerSpawner.towerList[0].GetComponentInChildren<SlashDir>();
                                if (slashDir.transform.eulerAngles == new Vector3(0, 0, 0))
                                {
                                    Atk();
                                }
                                else
                                {
                                    Dead();
                                    GameOverDelay();
                                }
                            }
                            else if (slashCount == 50 + 50 * bossCount)
                            {
                                if (slashDirs[slashBossCount].transform.eulerAngles == new Vector3(0, 0, 0))
                                {
                                    AtkBoss();
                                }
                                else
                                {
                                    Dead();
                                    GameOverDelay();
                                }
                            }
                        }
                    }
                }
            }
        }
    }
    void Atk()
    {
        randomGold = Random.Range(1, 100);
        if(randomGold <= 40)
        {
            AddGold();
        }
        AnimationState();
        AtkTower();
        atkEffect.Effect();
        cameraShake.Shake();
        staminaBar.value -= 1;
        if(slashCount == 50 + 50 * bossCount)
        {
            towerSpawner.SpawnBoss();
        }
    }

    void AtkBoss()
    {
        AddGold();
        --towerSpawner.hp;
        AnimationState();
        atkEffect.Effect();
        cameraShake.Shake();
        staminaBar.value -= 1;
        stamina.Spd += 0.001f;
        slashDirs[slashBossCount].gameObject.SetActive(false);
        slashBossCount++;
        if (towerSpawner.hp <= 0)
        {
            Handheld.Vibrate();
            PoolManager.ReturnBoss(towerSpawner.bossList[0]);
            for(int i = 0; i < towerSpawner.bossList[0].GetComponentsInChildren<SlashDir>().Length; i++)
            {
                towerSpawner.bossList[0].GetComponentsInChildren<SlashDir>()[i].transform.eulerAngles = new Vector3(0, 0, 0);
            }
            towerSpawner.bossList.Remove(towerSpawner.bossList[0]);
            towerSpawner.hp = 5;
            slashBossCount = 0;
            for(int i = 0; i < 5; i++)
            {
                slashDirs[i].gameObject.SetActive(true);
                randomDir = Random.Range(1, 100);
                if (randomDir < 50)
                {
                    slashDirs[i].transform.eulerAngles = new Vector3(0, 0, 90);
                }
                else
                {
                    slashDirs[i].transform.eulerAngles = new Vector3(0, 0, 0);
                }
            }
            bossCount++;
            if(GameManager.Instance.playerData.itemUseData[0] == true || GameManager.Instance.playerData.itemUseData[1])
            {
                slashCount += 1;
                ++Score.Instance.score;
            }
        }
    }
    public void GameOver()
    {
        if (GameManager.Instance.playerData.highScore < Score.Instance.score)
        {
            GameManager.Instance.playerData.highScore = Score.Instance.score;
            GameManager.Instance.Save();
            HighScoreText.text = "최고 층 : " + GameManager.Instance.playerData.highScore.ToString() + "F";
            ScoreText.text = "현재 층 : " + Score.Instance.score.ToString() + "F";
            panel.SetActive(true);
        }
        HighScoreText.text = "최고 층 : " + GameManager.Instance.playerData.highScore.ToString() + "F";
        ScoreText.text = "현재 층 : " + Score.Instance.score.ToString() + "F";
        panel.SetActive(true);
    }
    public void Dead()
    {
        animator.SetBool("isStart", false);
        isGameOver = true;
        DeadState();
    }
    public void GameOverDelay()
    {
        Invoke("GameOver", 1f);
    }
    void AddGold()
    {
        GameManager.Instance.playerData.playerMoney++;
        goldText.text = GameManager.Instance.playerData.playerMoney.ToString();
        var gold = PoolManager.GetGold();
        Gold.Instance.GetGold();
        StartCoroutine(CoinPooling(gold));
    }
    public void CheckUse()
    {
        Image thisImg = shopPlayer.GetComponent<Image>();
        Image lobbyImg = lobbyPlayer.GetComponent<Image>();
        if (GameManager.Instance.playerData.itemUseData[0] == true)
        {
            if(isGameOver != true)
            {
                lobbyImg.sprite = shopPlayer1;
            }
            thisImg.sprite = shopPlayer1;
        }
        else if (GameManager.Instance.playerData.itemUseData[1] == true)
        {
            if (isGameOver != true)
            {
                lobbyImg.sprite = shopPlayer2;
            }
            thisImg.sprite = shopPlayer2;
        }
        else if (GameManager.Instance.playerData.itemUseData[2] == true)
        {
            if (isGameOver != true)
            {
                lobbyImg.sprite = shopPlayer3;
            }
            thisImg.sprite = shopPlayer3;
        }
        else if (GameManager.Instance.playerData.itemUseData[3] == true)
        {
            if (isGameOver != true)
            {
                lobbyImg.sprite = shopPlayer4;
            }
            thisImg.sprite = shopPlayer4;
        }
    }
    void AnimationState()
    {
        if (GameManager.Instance.playerData.itemUseData[0] == true)
        {
            animator.SetTrigger("isAtk");
        }
        else if (GameManager.Instance.playerData.itemUseData[1] == true)
        {
            animator.SetTrigger("isAtk2");
        }
        else if (GameManager.Instance.playerData.itemUseData[2] == true)
        {
            animator.SetTrigger("isAtk3");
        }
        else if (GameManager.Instance.playerData.itemUseData[3] == true)
        {
            animator.SetTrigger("isAtk4");
        }
    }
    void DeadState()
    {
        if (GameManager.Instance.playerData.itemUseData[0] == true)
        {
            animator.SetBool("isAtkIdle", false);
            animator.Play("Dead");
        }
        else if (GameManager.Instance.playerData.itemUseData[1] == true)
        {
            animator.SetBool("isAtk2Idle", false);
            animator.Play("Dead2");
        }
        else if (GameManager.Instance.playerData.itemUseData[2] == true)
        {
            animator.SetBool("isAtk3Idle", false);
            animator.Play("Dead3");
        }
        else if (GameManager.Instance.playerData.itemUseData[3] == true)
        {
            animator.SetBool("isAtk4Idle", false);
            animator.Play("Dead4");
        }
    }
    void AtkTower()
    {
        if (GameManager.Instance.playerData.itemUseData[0] == true)
        {
            ++slashCount;
            Score.Instance.score += 1;
            PoolManager.ReturnObject(towerSpawner.towerList[0]);
            TowerSpawner.Instance.towerList[0].GetComponentInChildren<SlashDir>().transform.eulerAngles = new Vector3(0, 0, 0);
            towerSpawner.towerList.Remove(towerSpawner.towerList[0]);
            --towerSpawner.count;
            if (slashCount != 50 + 50 * bossCount)
            {
                randomDir = Random.Range(1, 100);
                var tower = PoolManager.GetObject();
                slashDir = tower.GetComponentInChildren<SlashDir>();
                towerSpawner.towerList.Add(tower);
                if (randomDir < 50)
                {
                    slashDir.transform.eulerAngles = new Vector3(0, 0, 90); // 타워 방향 가로로
                }
                tower.transform.position = new Vector3(towerSpawner.towerList[towerSpawner.count].transform.position.x, towerSpawner.towerList[towerSpawner.count].transform.position.y + 4);
                ++towerSpawner.count;
            }
            stamina.Spd += 0.001f;
        }
        else if (GameManager.Instance.playerData.itemUseData[1] == true)
        {
            ++slashCount;
            Score.Instance.score += 1;
            PoolManager.ReturnObject(towerSpawner.towerList[0]);
            TowerSpawner.Instance.towerList[0].GetComponentInChildren<SlashDir>().transform.eulerAngles = new Vector3(0, 0, 0);
            towerSpawner.towerList.Remove(towerSpawner.towerList[0]);
            --towerSpawner.count;
            if (slashCount != 50 + 50 * bossCount)
            {
                randomDir = Random.Range(1, 100);
                var tower = PoolManager.GetObject();
                slashDir = tower.GetComponentInChildren<SlashDir>();
                towerSpawner.towerList.Add(tower);
                if (randomDir < 50)
                {
                    slashDir.transform.eulerAngles = new Vector3(0, 0, 90); // 타워 방향 가로로
                }
                tower.transform.position = new Vector3(towerSpawner.towerList[towerSpawner.count].transform.position.x, towerSpawner.towerList[towerSpawner.count].transform.position.y + 4);
                ++towerSpawner.count;
            }
            stamina.Spd += 0.0008f;
        }
        else if(GameManager.Instance.playerData.itemUseData[2] == true)
        {
            for (int i = 0; i < 2; i++)
            {
                ++slashCount;
                Score.Instance.score += 1;
                PoolManager.ReturnObject(towerSpawner.towerList[0]);
                TowerSpawner.Instance.towerList[0].GetComponentInChildren<SlashDir>().transform.eulerAngles = new Vector3(0, 0, 0);
                towerSpawner.towerList.Remove(towerSpawner.towerList[0]);
                --towerSpawner.count;
                if (slashCount != 50 + 50 * bossCount)
                {
                    randomDir = Random.Range(1, 100);
                    var tower = PoolManager.GetObject();
                    slashDir = tower.GetComponentInChildren<SlashDir>();
                    towerSpawner.towerList.Add(tower);
                    if (randomDir < 50)
                    {
                        slashDir.transform.eulerAngles = new Vector3(0, 0, 90); // 타워 방향 가로로
                    }
                    tower.transform.position = new Vector3(towerSpawner.towerList[towerSpawner.count].transform.position.x, towerSpawner.towerList[towerSpawner.count].transform.position.y + 4);
                    ++towerSpawner.count;

                }
            }
            stamina.Spd += 0.001f;
        }
        else if (GameManager.Instance.playerData.itemUseData[3] == true)
        {
            for (int i = 0; i < 5; i++)
            {
                ++slashCount;
                Score.Instance.score += 1;
                PoolManager.ReturnObject(towerSpawner.towerList[0]);
                TowerSpawner.Instance.towerList[0].GetComponentInChildren<SlashDir>().transform.eulerAngles = new Vector3(0, 0, 0);
                towerSpawner.towerList.Remove(towerSpawner.towerList[0]);
                --towerSpawner.count;
                if (slashCount != 50 + 50 * bossCount)
                {
                    randomDir = Random.Range(1, 100);
                    var tower = PoolManager.GetObject();
                    slashDir = tower.GetComponentInChildren<SlashDir>();
                    towerSpawner.towerList.Add(tower);
                    if (randomDir < 50)
                    {
                        slashDir.transform.eulerAngles = new Vector3(0, 0, 90); // 타워 방향 가로로
                    }
                    tower.transform.position = new Vector3(towerSpawner.towerList[towerSpawner.count].transform.position.x, towerSpawner.towerList[towerSpawner.count].transform.position.y + 4);
                    ++towerSpawner.count;
                }
            }
            stamina.Spd += 0.002f;
        }
    }
    public void IdleState()
    {
        if (GameManager.Instance.playerData.itemUseData[0] == true)
        {
            animator.Play("Atk Idle");
        }
        else if (GameManager.Instance.playerData.itemUseData[1] == true)
        {
            animator.Play("Atk2 Idle");
        }
        else if (GameManager.Instance.playerData.itemUseData[2] == true)
        {
            animator.Play("Atk3 Idle");
        }
        else if (GameManager.Instance.playerData.itemUseData[3] == true)
        {
            animator.Play("Atk4 Idle");
        }
    }
    public void BackGroundChange()
    {
        if(slashCount >= 100)
        {
            backGroundSprite[0].DOFade(0f, 1.5f);
            backGroundSprite[1].gameObject.SetActive(true);
        }
        else if(slashCount >= 300)
        {
            backGroundSprite[1].DOFade(0f, 1.5f);
            backGroundSprite[2].gameObject.SetActive(true);
        }
        else if(slashCount >= 500)
        {
            backGroundSprite[2].DOFade(0f, 1.5f);
            backGroundSprite[3].gameObject.SetActive(true);
        }
        else if(slashCount >= 1000)
        {
            backGroundSprite[3].DOFade(0f, 1.5f);
            backGroundSprite[4].gameObject.SetActive(true);
        }
    }
    IEnumerator CoinPooling(Gold gold)
    {
        yield return new WaitForSeconds(0.2f);
        PoolManager.ReturnGold(gold);
    }
}
