using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField]
    private Slider staminaBar; //���¹̳�(ü��) ��
    [SerializeField]
    private StaminaBar stamina; //���¹̳� �� �ڵ� �������� ���� ����
    [SerializeField]
    private TowerSpawner towerSpawner; // Ÿ�������ʸ� �������� ���� ����
    [SerializeField]
    private AtkEffect atkEffect; //���� ����Ʈ
    [SerializeField]
    private CameraShake cameraShake; //ī�޶� ��鸲
    [SerializeField]
    private Boss boss; //������
    DragManager dragManager; //��������
    private Animator animator; //�ִϸ��̼�
    public List<SlashDir> slashList = new List<SlashDir>(); //������ ���� ����Ʈ
    private SlashDir slashDir; //�� ����
    public SlashDir[] slashDirs; //�� ���� �迭
    public int slashCount = 0; //�� Ƚ��
    public int slashBossCount = 0; //������ �� Ƚ��
    public GameObject panel = null;
    void Awake()
    {
        dragManager = GetComponent<DragManager>();
        animator = GetComponent<Animator>();
        dragManager.setOnSwipeDetected(MyOnSwipeDetected);
    }
    void MyOnSwipeDetected(Vector3 swipeDirection) // �������� �Լ�
    {
        if (panel.activeSelf == false)
        {
            if (swipeDirection.x != 0 || swipeDirection.y != 0) // x��ǥ Ȥ�� y��ǥ�� 0�� �ƴ϶��
            {
                if (Mathf.Abs(swipeDirection.x) > Mathf.Abs(swipeDirection.y)) //x�� ������ y�� ���񰪺��� ũ�ٸ�
                {
                    if (slashCount != 9)
                    {
                        slashDir = towerSpawner.towerList[0].GetComponentInChildren<SlashDir>();
                        if (slashDir.transform.eulerAngles != new Vector3(0, 0, 0))
                        {
                            Atk();
                        }
                    }
                    else if (slashCount == 9)
                    {
                        if (slashDirs[slashBossCount].transform.eulerAngles != new Vector3(0, 0, 0))
                        {
                            AtkBoss();
                        }
                    }
                }
                else if (Mathf.Abs(swipeDirection.y) > Mathf.Abs(swipeDirection.x))//y�� ������ x�� ���񰪺��� ũ�ٸ� 
                {
                    if (slashCount != 9)
                    {
                        slashDir = towerSpawner.towerList[0].GetComponentInChildren<SlashDir>();
                        if (slashDir.transform.eulerAngles == new Vector3(0, 0, 0))
                        {
                            Atk();
                        }
                    }
                    else if (slashCount == 9)
                    {
                        if (slashDirs[slashBossCount].transform.eulerAngles == new Vector3(0, 0, 0))
                        {
                            AtkBoss();
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
        if(slashCount != 9)
        {
            var tower = PoolManager.GetObject();
            towerSpawner.towerList.Add(tower);
            tower.transform.position = new Vector3(towerSpawner.towerList[towerSpawner.count].transform.position.x, towerSpawner.towerList[towerSpawner.count].transform.position.y + 2);
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
            ++slashCount;
        }
    }
}
