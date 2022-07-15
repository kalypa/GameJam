using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class StartFade : MonoBehaviour
{
    void OnEnable()
    {
        // 델리게이트 체인 추가
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Debug.Log("씬 교체됨, 현재 씬: " + scene.name);

        // 씬 전환 효과 (Fade In)
        StartCoroutine(FadeStart());
    }

    void OnDisable()
    {
        // 델리게이트 체인 제거
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    IEnumerator FadeStart()
    {
        yield return new WaitForSeconds(0.5f);
        gameObject.GetComponent<Image>().DOFade(0f, 1f);
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
        Player.Instance.backGroundSound.clip = Player.Instance.backGroundClip[0];
        Player.Instance.backGroundSound.Play();
        SoundOption.Instance.SoundUpdate();
        GameManager.Instance.Save();

    }
}
