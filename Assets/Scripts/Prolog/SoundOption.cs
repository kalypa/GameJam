using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundOption : MonoSingleton<SoundOption>
{
    [SerializeField]
    private Scrollbar bgmScaler;
    [SerializeField]
    private Scrollbar sfxScaler;

    [SerializeField]
    private AudioSource[] sfxSource;
    [SerializeField]
    private AudioSource bgmSource;



    private void Start()
    {
        SoundUpdate();
        bgmScaler.value = 0.3f;
        sfxScaler.value = 0.3f;
        GameManager.Instance.playerData.bgmScale = bgmScaler.value;
        GameManager.Instance.playerData.sfxScale = sfxScaler.value;
        GameManager.Instance.playerData.firstStart = false;
        GameManager.Instance.Save();
    }

    public void SoundUpdate()
    {
        GameManager.Instance.playerData.bgmScale = bgmScaler.value;
        GameManager.Instance.playerData.sfxScale = sfxScaler.value;
        Debug.Log(GameManager.Instance.playerData.bgmScale);
        Debug.Log(GameManager.Instance.playerData.sfxScale);

        for(int i = 0; i<sfxSource.Length; i++)
        {
            sfxSource[i].volume = GameManager.Instance.playerData.sfxScale;
        }
        bgmSource.volume = GameManager.Instance.playerData.bgmScale;
    }
}
