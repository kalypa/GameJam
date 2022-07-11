using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSpawner : MonoBehaviour
{
    private float count = 0;
    private float height = 3;
    void Start()
    {
        var tower = PoolManager.GetObject();
    }

    void Update()
    {
        Spawn();
    }
    void Spawn()
    {
        while(count < 10)
        {
            var tower = PoolManager.GetObject();
            var position = new Vector3(tower.transform.position.x, tower.transform.position.y + height);
            tower.transform.position += position;
            ++count;
            height += 3;
        }
    }
}
