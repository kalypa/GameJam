using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSpawner : MonoBehaviour
{
    [SerializeField]
    private Player player; // 플레이어
    private SlashDir slashDir; //베야하는 방향 표시
    public SlashDir[] slashDirs = new SlashDir[10]; //방향 배열
    private float randomDir; //방향 랜덤 처리
    public int hp = 5; //보스룸 HP
    public int count = 0; //파괴한 탑 수
    public int bossCount = 0; // 보스층 수
    public float height = 3.1f; // 층이 생성되는 높이
    public List<Tower> towerList = new List<Tower>(); //타워객체가 담아지는 리스트
    public List<Boss> bossList = new List<Boss>(); //보스객체가 담아지는 리스트
    void Start()
    {
        var tower = PoolManager.GetObject(); // 풀매니저에서 타워 가져와서 생성
        towerList[count] = tower; // 타워리스트에 타워 넣어주기
        slashDir = tower.GetComponentInChildren<SlashDir>(); // 방향 넣어주기
        Spawn(); // 타워 생성
    }

    void Spawn() //타워 생성 함수
    {
        while(count < 10) // 10개 생성
        {
            randomDir = Random.Range(1, 100);
            var tower = PoolManager.GetObject();
            ++count;
            towerList[count] = tower;
            slashDir = tower.GetComponentInChildren<SlashDir>();
            var position = new Vector3(tower.transform.position.x, tower.transform.position.y + height); //타워 높인
            tower.transform.position += position;
            if (randomDir < 50)
            {
                slashDir.transform.eulerAngles = new Vector3(0, 0, 90); // 타워 방향 가로로
            }
            height += 2.3f;
        }
    }
    public void SpawnBoss() // 보스룸 생성 함수
    {
        var boss = PoolManager.GetBoss();
        bossList[bossCount] = boss;
        slashDirs = boss.GetComponentsInChildren<SlashDir>();
        var position = new Vector3(towerList[0].transform.position.x, towerList[0].transform.position.y);
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
