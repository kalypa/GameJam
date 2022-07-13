using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Gold : MonoSingleton<Gold>
{

    public void GetGold()
    {
        gameObject.transform.DOMove(new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 3, gameObject.transform.position.z), 1.5f, false);
        gameObject.GetComponent<SpriteRenderer>().DOFade(0f, 1.5f);
    }
}
