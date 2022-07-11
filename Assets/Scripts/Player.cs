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

    void Awake()
    {
        dragManager = GetComponent<DragManager>();
        dragManager.setOnSwipeDetected(MyOnSwipeDetected);
    }

    void MyOnSwipeDetected(Vector3 swipeDirection)
    {
        if(swipeDirection.x != 0 || swipeDirection.y != 0)
        {
            if(Mathf.Abs(swipeDirection.x) > Mathf.Abs(swipeDirection.y))
            {
                Debug.Log("가로베기");
                if(towerSpawner.towerList[0].GetComponentInChildren<SlashDir>().transform.eulerAngles != new Vector3(0, 0, 0))
                {
                    PoolManager.ReturnObject(towerSpawner.towerList[0]);
                    towerSpawner.towerList.Remove(towerSpawner.towerList[0]);
                    atkEffect.Effect();
                    cameraShake.Shake();
                    staminaBar.value -= 1;
                    stamina.Spd += 0.01f;
                    --towerSpawner.count;
                    var tower = PoolManager.GetObject();
                    towerSpawner.towerList.Add(tower);
                    tower.transform.position = new Vector3(towerSpawner.towerList[towerSpawner.count].transform.position.x, towerSpawner.towerList[towerSpawner.count].transform.position.y + 2);
                    ++towerSpawner.count;
                }
            }
            else if(Mathf.Abs(swipeDirection.y) > Mathf.Abs(swipeDirection.x))
            {
                Debug.Log("세로베기");
                if (towerSpawner.towerList[0].GetComponentInChildren<SlashDir>().transform.eulerAngles == new Vector3(0, 0, 0))
                {
                    PoolManager.ReturnObject(towerSpawner.towerList[0]);
                    towerSpawner.towerList.Remove(towerSpawner.towerList[0]);
                    atkEffect.Effect();
                    cameraShake.Shake();
                    staminaBar.value -= 1;
                    stamina.Spd += 0.01f;
                    --towerSpawner.count;
                    var tower = PoolManager.GetObject();
                    towerSpawner.towerList.Add(tower);
                    tower.transform.position = new Vector3(towerSpawner.towerList[towerSpawner.count].transform.position.x, towerSpawner.towerList[towerSpawner.count].transform.position.y + 2);
                    ++towerSpawner.count;
                }
            }
        }
    }
}
