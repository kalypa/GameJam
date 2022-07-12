using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSpawner : MonoBehaviour
{
    private SlashDir slashDir;
    public SlashDir[] slashDirs;
    private float randomDir;
    public int hp = 5;
    public int count = 0;
    public int bossCount = 0;
    public float height = 3.1f;
    public List<Tower> towerList = new List<Tower>();
    public List<Boss> bossList = new List<Boss>();
    void Start()
    {
        var tower = PoolManager.GetObject();
        towerList[count] = tower;
        slashDir = tower.GetComponentInChildren<SlashDir>();
        Spawn();
    }

    void Update()
    {
        Debug.Log(count);
    }
    void Spawn()
    {
        while(count < 10)
        {
            randomDir = Random.Range(1, 100);
            var tower = PoolManager.GetObject();
            ++count;
            towerList[count] = tower;
            slashDir = tower.GetComponentInChildren<SlashDir>();
            var position = new Vector3(tower.transform.position.x, tower.transform.position.y + height);
            tower.transform.position += position;
            if (randomDir < 50)
            {
                slashDir.transform.eulerAngles = new Vector3(0, 0, 90);
            }
            height += 3.1f;
        }
    }
    public void SpawnBoss()
    {
        randomDir = Random.Range(1, 100);
        var boss = PoolManager.GetBoss();
        bossList[bossCount] = boss;
        slashDirs = boss.GetComponentsInChildren<SlashDir>();
        var position = new Vector3(towerList[count].transform.position.x, towerList[count].transform.position.y + height);
        boss.transform.position += position;
        for(int i = 0; i < slashDirs.Length; i++)
        {
            if(randomDir < 50)
            {
                slashDirs[i].transform.eulerAngles = new Vector3(0, 0, 90);
            }
            else
            {
                slashDirs[i].transform.eulerAngles = new Vector3(0, 0, 0);
            }
        }
        ++bossCount;
    }
}
