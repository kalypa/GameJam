using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerSpawner : MonoSingleton<TowerSpawner>
{
    [SerializeField]
    private Player player; // 플레이어
    private SlashDir slashDir; //베야하는 방향 표시
    public SlashDir[] slashDirs = new SlashDir[10]; //방향 배열
    private float randomDir; //방향 랜덤 처리
    public int hp = 5; //보스룸 HP
    public int count = 0; //파괴한 탑 수
    public int bossCount = 0; // 보스층 수
    public float height = 0f; // 층이 생성되는 높이
    public List<Tower> towerList = new List<Tower>(); //타워객체가 담아지는 리스트
    public List<Boss> bossList = new List<Boss>(); //보스객체가 담아지는 리스트
    public bool isVertical = false;
    public bool isHorizontal = false;
    public Sprite sword1;
    public Sprite sword2;
    public Sprite sword3;
    public Sprite sword4;
    public void Spawn() //타워 생성 함수
    {
        while(count < 5) // 4개 생성
        {
            randomDir = Random.Range(1, 100);
            var tower = PoolManager.GetObject();
            towerList.Add(tower);
            slashDir = tower.GetComponentInChildren<SlashDir>();
            SlashDirImageChange();
            var position = new Vector3(towerList[count].transform.position.x, towerList[count].transform.position.y + height); //타워 높이
            tower.transform.position += position;
            if (randomDir < 50)
            {
                slashDir.transform.eulerAngles = new Vector3(0, 0, 90); // 타워 방향 가로로
            }
            height += 3.1f;
            ++count;
        }
        if(Player.Instance.isFirstStart == true)
        {
            towerList.Swap(3, 4);
            towerList.Swap(4, 5);
        }
        else if (Player.Instance.isFirstStart == false)
        {
            towerList.Swap(4, 5);
        }
    }
    public void SpawnFirst()
    {
        randomDir = Random.Range(1, 100);
        var tower = PoolManager.GetObject();
        towerList.Add(tower);
        slashDir = tower.GetComponentInChildren<SlashDir>();
        SlashDirImageChange();
        if (randomDir < 50)
        {
            slashDir.transform.eulerAngles = new Vector3(0, 0, 90); // 타워 방향 가로로
            isHorizontal = true;
        }
        else
        {
            isVertical = true;
        }
        height += 3.1f;
    }
    public void SpawnBoss() // 보스룸 생성 함수
    {
        var boss = PoolManager.GetBoss();
        bossList.Add(boss);
        slashDirs = boss.GetComponentsInChildren<SlashDir>();
        var position = new Vector3(towerList[0].transform.position.x, -0.4522314f);
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
    public void SlashDirImageChange()
    {
        if(GameManager.Instance.playerData.itemUseData[0] == true)
        {
            slashDir.GetComponent<SpriteRenderer>().sprite = sword1;
        }
        else if(GameManager.Instance.playerData.itemUseData[1] == true)
        {
            slashDir.GetComponent<SpriteRenderer>().sprite = sword2;
        }
        else if(GameManager.Instance.playerData.itemUseData[2] == true)
        {
            slashDir.GetComponent<SpriteRenderer>().sprite = sword3;
        }
        else if(GameManager.Instance.playerData.itemUseData[3] == true)
        {
            slashDir.GetComponent<SpriteRenderer>().sprite = sword4;
        }
    }
}
