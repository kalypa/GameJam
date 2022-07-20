using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoSingleton<PoolManager>
{
    [SerializeField]
    private GameObject poolingObjectPrefab; //풀링할 오브젝트
    [SerializeField]
    private GameObject poolingEffect; // 풀링할 이펙트
    [SerializeField]
    private GameObject poolingBoss; // 풀링할 보스오브젝트
    [SerializeField]
    private GameObject poolingCoinEffect;
    Queue<Tower> poolingObjectQueue = new Queue<Tower>(); //타워 오브젝트를 풀링할 큐
    Queue<Effect> poolingEffectQueue = new Queue<Effect>();
    Queue<Boss> poolingBossQueue = new Queue<Boss>();     //보스 오브젝트를 풀링할 큐
    private void Awake()
    {
        Initialize(6); //초기 생성 수
        EffectInitialize(1);
        BossInitialize(1);
    }
    private void Initialize(int initCount) // 초기 생성
    {
        for (int i = 0; i < initCount; i++)
        {
            poolingObjectQueue.Enqueue(CreateNewObject());
        }
    }

    private void EffectInitialize(int initCount) // 초기 생성
    {
        for (int i = 0; i < initCount; i++)
        {
            poolingEffectQueue.Enqueue(CreateNewEffect());
        }
    }

    private void BossInitialize(int initCount) // 초기 생성
    {
        for (int i = 0; i < initCount; i++)
        {
            poolingBossQueue.Enqueue(CreateNewBoss());
        }
    }

    private Tower CreateNewObject() // 새로운 오브젝트 만들기
    {
        var newObj = Instantiate(poolingObjectPrefab).GetComponent<Tower>();
        newObj.gameObject.SetActive(false);
        newObj.transform.SetParent(transform);
        return newObj;
    }

    private Effect CreateNewEffect() // 새로운 오브젝트 만들기
    {
        var newef = Instantiate(poolingEffect).GetComponent<Effect>();
        newef.gameObject.SetActive(false);
        newef.transform.SetParent(transform);
        return newef;
    }

    private Boss CreateNewBoss() // 새로운 오브젝트 만들기
    {
        var newBs = Instantiate(poolingBoss).GetComponent<Boss>();
        newBs.gameObject.SetActive(false);
        newBs.transform.SetParent(transform);
        return newBs;
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

    public static Effect GetEffect() // 이펙트 생성
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

    public static Boss GetBoss() // 보스오브젝트 생성
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

    public static void ReturnObject(Tower obj) // 오브젝트 파괴
    {
        obj.gameObject.SetActive(false);
        obj.transform.SetParent(Instance.transform);
        Instance.poolingObjectQueue.Enqueue(obj);
    }

    public static void ReturnEffect(Effect ef) // 오브젝트 파괴
    {
        ef.gameObject.SetActive(false);
        ef.transform.SetParent(Instance.transform);
        Instance.poolingEffectQueue.Enqueue(ef);
    }

    public static void ReturnBoss(Boss bs) // 오브젝트 파괴
    {
        bs.gameObject.SetActive(false);
        bs.transform.SetParent(Instance.transform);
        Instance.poolingBossQueue.Enqueue(bs);
    }
}
