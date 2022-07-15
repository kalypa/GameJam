using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickEffect : MonoBehaviour
{
    [SerializeField]
    private AudioClip clickAudio;

    AudioSource setAudioSource;

    private void Start()
    {
        setAudioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            setAudioSource.clip = clickAudio;
            setAudioSource.Play();
        }
    }
}
