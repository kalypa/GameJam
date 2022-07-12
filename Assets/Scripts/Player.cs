using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField]
    private Slider staminaBar;
    [SerializeField]
    private StaminaBar stamina;
    [SerializeField]
    private TowerSpawner towerSpawner;
    [SerializeField]
    private AtkEffect atkEffect;
    [SerializeField]
    private CameraShake cameraShake;
    DragManager dragManager;
    private Animator animator;
    public int slashCount = 0;
    public int slashBossCount = 0;
    void Awake()
    {
        dragManager = GetComponent<DragManager>();
        dragManager.setOnSwipeDetected(MyOnSwipeDetected);
    }

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Debug.Log(slashCount);
    }
    void MyOnSwipeDetected(Vector3 swipeDirection)
    {
        if(swipeDirection.x != 0 || swipeDirection.y != 0)
        {
            if(Mathf.Abs(swipeDirection.x) > Mathf.Abs(swipeDirection.y))
            {
                Debug.Log("가로베기");
                if (slashCount != 49)
                {
                    if (towerSpawner.towerList[0].GetComponentInChildren<SlashDir>().transform.eulerAngles != new Vector3(0, 0, 0))
                    {
                        Atk();
                    }
                }
                if (slashCount == 49)
                {
                    towerSpawner.SpawnBoss();
                    if (towerSpawner.bossList[0].GetComponentsInChildren<SlashDir>()[slashBossCount].transform.eulerAngles != new Vector3(0, 0, 0))
                    {
                        AtkBoss();
                    }
                }
            }
            else if(Mathf.Abs(swipeDirection.y) > Mathf.Abs(swipeDirection.x))
            {
                Debug.Log("세로베기");
                if (slashCount != 49)
                {
                    if (towerSpawner.towerList[0].GetComponentInChildren<SlashDir>().transform.eulerAngles == new Vector3(0, 0, 0))
                    {
                        Atk();
                    }
                }
                if (slashCount == 49)
                {
                    towerSpawner.SpawnBoss();
                    if (towerSpawner.bossList[0].GetComponentsInChildren<SlashDir>()[slashBossCount].transform.eulerAngles != new Vector3(0, 0, 90))
                    {
                        AtkBoss();
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
        var tower = PoolManager.GetObject();
        towerSpawner.towerList.Add(tower);
        tower.transform.position = new Vector3(towerSpawner.towerList[towerSpawner.count].transform.position.x, towerSpawner.towerList[towerSpawner.count].transform.position.y + 2);
        ++towerSpawner.count;
    }

    void AtkBoss()
    {
        ++slashCount;
        animator.SetTrigger("isAtk");
        --towerSpawner.hp;
        atkEffect.Effect();
        cameraShake.Shake();
        Handheld.Vibrate();
        staminaBar.value -= 1;
        stamina.Spd += 0.01f;
        towerSpawner.slashDirs[slashBossCount].gameObject.SetActive(false);
        ++slashBossCount;
        --towerSpawner.bossCount;

        if (towerSpawner.hp == 0)
        {
            PoolManager.ReturnBoss(towerSpawner.bossList[0]);
            towerSpawner.bossList.Remove(towerSpawner.bossList[0]);
        }
    }
}
