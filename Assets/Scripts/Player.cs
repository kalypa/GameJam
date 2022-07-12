using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    private SlashDir slashDir; //벨 방향
    public SlashDir[] slashDirs; //벨 방향 배열
    public int slashCount = 0; //벤 횟수
    public int slashBossCount = 0; //보스를 벤 횟수
    private int bossCount = 0;
    private float randomDir;
    public GameObject gameOverPanel;
    public GameObject panel = null;
    public bool isGameOver = false;
    void Awake()
    {
        dragManager = GetComponent<DragManager>();
        animator = GetComponent<Animator>();
        dragManager.setOnSwipeDetected(MyOnSwipeDetected);
    }
    private void Update()
    {
        Debug.Log(slashCount);
    }
    void MyOnSwipeDetected(Vector3 swipeDirection) // 스와이프 함수
    {
        if(isGameOver == false)
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
                                Invoke("GameOver", 1f);
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
                                Invoke("GameOver", 1f);
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
                                Invoke("GameOver", 1f);
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
                                Invoke("GameOver", 1f);
                            }
                        }
                    }
                }
            }
        }
    }
    void Atk()
    {
        ++slashCount;
        animator.SetTrigger("isAtk");
        PoolManager.ReturnObject(towerSpawner.towerList[0]);
        towerSpawner.towerList.Remove(towerSpawner.towerList[0]);
        atkEffect.Effect();
        cameraShake.Shake();
        Handheld.Vibrate();
        staminaBar.value -= 1;
        stamina.Spd += 0.01f;
        --towerSpawner.count;
        Score.Instance.score += 1;
        if(slashCount != 50 + 50 * bossCount)
        {
            var tower = PoolManager.GetObject();
            towerSpawner.towerList.Add(tower);
            tower.transform.position = new Vector3(towerSpawner.towerList[towerSpawner.count].transform.position.x, towerSpawner.towerList[towerSpawner.count].transform.position.y + 4);
            ++towerSpawner.count;
        }
        else
        {
            towerSpawner.SpawnBoss();
        }
    }

    void AtkBoss()
    {
        --towerSpawner.hp;
        animator.SetTrigger("isAtk");
        atkEffect.Effect();
        cameraShake.Shake();
        Handheld.Vibrate();
        staminaBar.value -= 1;
        stamina.Spd += 0.01f;
        slashDirs[slashBossCount].gameObject.SetActive(false);
        slashBossCount++;
        if (towerSpawner.hp <= 0)
        {
            PoolManager.ReturnBoss(towerSpawner.bossList[0]);
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
            slashCount += 1;
            ++Score.Instance.score;
        }
    }
    public void GameOver()
    {
        gameOverPanel.SetActive(true);
    }
    public void Dead()
    {
        isGameOver = true;
        animator.SetTrigger("isDead");
    }
}
