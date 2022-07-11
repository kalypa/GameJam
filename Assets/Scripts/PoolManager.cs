using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoSingleton<PoolManager>
{
    [SerializeField]
    private GameObject poolingObjectPrefab; //Ǯ���� ������Ʈ

    Queue<Tower> poolingObjectQueue = new Queue<Tower>(); //Ÿ�� ������Ʈ�� Ǯ���� ť

    private void Awake()
    {
        Initialize(10); //�ʱ� ���� ��
    }

    private void Initialize(int initCount) // �ʱ� ����
    {
        for (int i = 0; i < initCount; i++)
        {
            poolingObjectQueue.Enqueue(CreateNewObject());
        }
    }

    private Tower CreateNewObject() // ���ο� ������Ʈ �����
    {
        var newObj = Instantiate(poolingObjectPrefab).GetComponent<Tower>();
        newObj.gameObject.SetActive(false);
        newObj.transform.SetParent(transform);
        return newObj;
    }

    public static Tower GetObject() // ������Ʈ ����
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

    public static void ReturnObject(Tower obj) // ������Ʈ �ı�
    {
        obj.gameObject.SetActive(false);
        obj.transform.SetParent(Instance.transform);
        Instance.poolingObjectQueue.Enqueue(obj);
    }
}
