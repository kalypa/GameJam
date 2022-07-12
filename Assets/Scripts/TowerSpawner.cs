using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSpawner : MonoSingleton<TowerSpawner>
{
    [SerializeField]
    private Player player; // �÷��̾�
    private SlashDir slashDir; //�����ϴ� ���� ǥ��
    public SlashDir[] slashDirs = new SlashDir[10]; //���� �迭
    private float randomDir; //���� ���� ó��
    public int hp = 5; //������ HP
    public int count = 0; //�ı��� ž ��
    public int bossCount = 0; // ������ ��
    public float height = 2.8f; // ���� �����Ǵ� ����
    public List<Tower> towerList = new List<Tower>(); //Ÿ����ü�� ������� ����Ʈ
    public List<Boss> bossList = new List<Boss>(); //������ü�� ������� ����Ʈ
    public void Spawn() //Ÿ�� ���� �Լ�
    {
        while(count < 10) // 10�� ����
        {
            randomDir = Random.Range(1, 100);
            var tower = PoolManager.GetObject();
            ++count;
            towerList.Add(tower);
            slashDir = tower.GetComponentInChildren<SlashDir>();
            var position = new Vector3(tower.transform.position.x, tower.transform.position.y + height); //Ÿ�� ����
            tower.transform.position += position;
            if (randomDir < 50)
            {
                slashDir.transform.eulerAngles = new Vector3(0, 0, 90); // Ÿ�� ���� ���η�
            }
            height += 2.8f;
        }
    }
    public void SpawnFirst()
    {
        var tower = PoolManager.GetObject();
        towerList.Add(tower);
        slashDir = tower.GetComponentInChildren<SlashDir>();
    }
    public void SpawnBoss() // ������ ���� �Լ�
    {
        var boss = PoolManager.GetBoss();
        bossList.Add(boss);
        slashDirs = boss.GetComponentsInChildren<SlashDir>();
        var position = new Vector3(towerList[0].transform.position.x, -0.4522314f/*towerList[0].transform.position.y*/);
        boss.transform.position += position;
        for(int i = 0; i < slashDirs.Length; i++)
        {
            randomDir = Random.Range(1, 100);
            if (randomDir < 50)
            {
                slashDirs[i].transform.eulerAngles = new Vector3(0, 0, 90);
            }
            else
            {
                slashDirs[i].transform.eulerAngles = new Vector3(0, 0, 0);
            }
            player.slashDirs[i] = slashDirs[i];
        }
    }
}
