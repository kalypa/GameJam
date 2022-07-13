using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Gold : MonoSingleton<Gold>
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void GetGold()
    {
        gameObject.transform.DOMove(new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 3, gameObject.transform.position.z), 1f, false);
        gameObject.GetComponent<SpriteRenderer>().DOFade(0f, 1f);
    }
}
