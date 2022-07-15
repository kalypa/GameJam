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
        // ��������Ʈ ü�� �߰�
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Debug.Log("�� ��ü��, ���� ��: " + scene.name);

        // �� ��ȯ ȿ�� (Fade In)
        StartCoroutine(FadeStart());
    }

    void OnDisable()
    {
        // ��������Ʈ ü�� ����
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
