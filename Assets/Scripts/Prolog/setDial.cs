using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;
public class setDial : MonoBehaviour
{
    [SerializeField]
    private Image newImage;
    [SerializeField]
    private Text mentText;
    [SerializeField]
    private Image endImage;
    [SerializeField]
    private Image backGroundImage;
    [SerializeField]
    private MentData mentData;
    [SerializeField]
    private AudioSource bgmSource;
    [SerializeField]
    private Image FadeImage;
    [SerializeField]
    private GameObject loadingImage;

    public bool nowMent = false;
    public bool istyping;
    public bool isFirst;
    public float waitTime;

    void Start()
    {
        SetTyping();
        bgmSource.Play();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (istyping == false)
            {
                mentText.gameObject.SetActive(false);
                endImage.gameObject.SetActive(false);
                endImage.gameObject.SetActive(false);
                backGroundImage.gameObject.SetActive(false);
                bgmSource.Stop();
                StartCoroutine(FadeandLoad());
            }
            istyping = false;
        }
    }

    IEnumerator FadeandLoad()
    {
        FadeImage.DOFade(1f, 1f);
        for(int i = 0; i < loadingImage.GetComponentsInChildren<Image>().Length; i++)
        {
            loadingImage.GetComponentsInChildren<Image>()[i].DOFade(1f, 1f);
        }
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Main");
    }

    public void SetTyping()
    {
        StartCoroutine("Typing");
    }

    public IEnumerator Typing()
    {
        int index = 0;
        istyping = true;
        endImage.color = new Color(1f, 1f, 1f, 0);

        while (index < mentData.mentData.Length + 1)
        {
            nowMent = true;
            mentText.text = mentData.mentData.Substring(0, index);
            index++;

            if (istyping == false)
            {
                mentText.text = mentData.mentData;
                index = mentData.mentData.Length;
                nowMent = false;
                endImage.color = new Color(1f, 1f, 1f, 1f);
            }
            yield return new WaitForSeconds(waitTime);

        }
        istyping = false;
        nowMent = false;
        endImage.color = new Color(1f, 1f, 1f, 1f);

    }
}
