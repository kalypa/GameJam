using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoSingleton<PoolManager>
{
    [SerializeField]
    private GameObject poolingObjectPrefab; //Ǯ���� ������Ʈ
    [SerializeField]
    private GameObject poolingEffect; // Ǯ���� ����Ʈ
    [SerializeField]
    private GameObject poolingBoss; // Ǯ���� ����������Ʈ
    [SerializeField]
    private GameObject poolingCoinEffect;
    Queue<Tower> poolingObjectQueue = new Queue<Tower>(); //Ÿ�� ������Ʈ�� Ǯ���� ť
    Queue<Effect> poolingEffectQueue = new Queue<Effect>();
    Queue<Boss> poolingBossQueue = new Queue<Boss>();     //���� ������Ʈ�� Ǯ���� ť
    private void Awake()
    {
        Initialize(6); //�ʱ� ���� ��
        EffectInitialize(1);
        BossInitialize(1);
    }
    private void Initialize(int initCount) // �ʱ� ����
    {
        for (int i = 0; i < initCount; i++)
        {
            poolingObjectQueue.Enqueue(CreateNewObject());
        }
    }

    private void EffectInitialize(int initCount) // �ʱ� ����
    {
        for (int i = 0; i < initCount; i++)
        {
            poolingEffectQueue.Enqueue(CreateNewEffect());
        }
    }

    private void BossInitialize(int initCount) // �ʱ� ����
    {
        for (int i = 0; i < initCount; i++)
        {
            poolingBossQueue.Enqueue(CreateNewBoss());
        }
    }

    private Tower CreateNewObject() // ���ο� ������Ʈ �����
    {
        var newObj = Instantiate(poolingObjectPrefab).GetComponent<Tower>();
        newObj.gameObject.SetActive(false);
        newObj.transform.SetParent(transform);
        return newObj;
    }

    private Effect CreateNewEffect() // ���ο� ������Ʈ �����
    {
        var newef = Instantiate(poolingEffect).GetComponent<Effect>();
        newef.gameObject.SetActive(false);
        newef.transform.SetParent(transform);
        return newef;
    }

    private Boss CreateNewBoss() // ���ο� ������Ʈ �����
    {
        var newBs = Instantiate(poolingBoss).GetComponent<Boss>();
        newBs.gameObject.SetActive(false);
        newBs.transform.SetParent(transform);
        return newBs;
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

    public static Effect GetEffect() // ����Ʈ ����
    {
        if (Instance.poolingEffectQueue.Count > 0)
        {
            var ef = Instance.poolingEffectQueue.Dequeue();
            ef.transform.SetParent(null);
            ef.gameObject.SetActive(true);
            return ef;
        }
        else
        {
            var ef = Instance.CreateNewEffect();
            ef.gameObject.SetActive(true);
            ef.transform.SetParent(null);
            return ef;
        }
    }

    public static Boss GetBoss() // ����������Ʈ ����
    {
        if (Instance.poolingBossQueue.Count > 0)
        {
            var Bs = Instance.poolingBossQueue.Dequeue();
            Bs.transform.SetParent(null);
            Bs.gameObject.SetActive(true);
            return Bs;
        }
        else
        {
            var Bs = Instance.CreateNewBoss();
            Bs.gameObject.SetActive(true);
            Bs.transform.SetParent(null);
            return Bs;
        }
    }

    public static void ReturnObject(Tower obj) // ������Ʈ �ı�
    {
        obj.gameObject.SetActive(false);
        obj.transform.SetParent(Instance.transform);
        Instance.poolingObjectQueue.Enqueue(obj);
    }

    public static void ReturnEffect(Effect ef) // ������Ʈ �ı�
    {
        ef.gameObject.SetActive(false);
        ef.transform.SetParent(Instance.transform);
        Instance.poolingEffectQueue.Enqueue(ef);
    }

    public static void ReturnBoss(Boss bs) // ������Ʈ �ı�
    {
        bs.gameObject.SetActive(false);
        bs.transform.SetParent(Instance.transform);
        Instance.poolingBossQueue.Enqueue(bs);
    }
}
