using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtkEffect : MonoBehaviour
{
    private float effectTime;

    void Update()
    {
        effectTime -= Time.deltaTime;
        if(effectTime < 0)
        {
            effectTime = 0.3f;
            var effect = PoolManager.GetEffect();
            effect.gameObject.SetActive(false);
            PoolManager.ReturnEffect(effect);
        }
    }

    private void OnEnable()
    {
        effectTime = 0.3f;
    }
    public void Effect()
    {
        var effect = PoolManager.GetEffect();
        effect.gameObject.SetActive(true);
    }
}
