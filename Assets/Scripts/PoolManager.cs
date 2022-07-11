using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoSingleton<PoolManager>
{
    [SerializeField]
    private GameObject poolingObjectPrefab; //풀링할 오브젝트

    Queue<Tower> poolingObjectQueue = new Queue<Tower>(); //타워 오브젝트를 풀링할 큐

    private void Awake()
    {
        Initialize(10); //초기 생성 수
    }

    private void Initialize(int initCount) // 초기 생성
    {
        for (int i = 0; i < initCount; i++)
        {
            poolingObjectQueue.Enqueue(CreateNewObject());
        }
    }

    private Tower CreateNewObject() // 새로운 오브젝트 만들기
    {
        var newObj = Instantiate(poolingObjectPrefab).GetComponent<Tower>();
        newObj.gameObject.SetActive(false);
        newObj.transform.SetParent(transform);
        return newObj;
    }

    public static Tower GetObject() // 오브젝트 생성
    {
        if (Instance.poolingObjectQueue.Count > 0)
        {
            var obj = Instance.poolingObjectQueue.Dequeue();
            obj.transform.SetParent(null);
            obj.gameObject.SetActive(true);
            return obj;
        }
        else
        {
            var newObj = Instance.CreateNewObject();
            newObj.gameObject.SetActive(true);
            newObj.transform.SetParent(null);
            return newObj;
        }
    }

    public static void ReturnObject(Tower obj) // 오브젝트 파괴
    {
        obj.gameObject.SetActive(false);
        obj.transform.SetParent(Instance.transform);
        Instance.poolingObjectQueue.Enqueue(obj);
    }
}
