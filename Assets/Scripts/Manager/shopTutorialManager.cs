using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class shopTutorialManager : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer finger;
    [SerializeField]
    private Text shopTutorialText;

    [SerializeField]
    private GameObject clearFilter;

    [SerializeField]
    private GameObject finger2;
    [SerializeField]
    private Text shopTutorialText2;

    private void Start()
    {
        TutorialAnim();
    }

    private void Update()
    {
        if (Input.anyKeyDown&&finger.gameObject.activeSelf==true)
        {
            finger.gameObject.SetActive(false);
            shopTutorialText.gameObject.SetActive(false);
            finger2.gameObject.SetActive(true);
            shopTutorialText2.gameObject.SetActive(true);
            StartCoroutine(TutoAnimSet());
        }
        else if(Input.anyKeyDown)
        {
            finger.gameObject.SetActive(false);
            shopTutorialText.gameObject.SetActive(false);
            finger2.gameObject.SetActive(false);
            clearFilter.SetActive(false);
            shopTutorialText2.gameObject.SetActive(false);
        }
    }

    IEnumerator TutoAnimSet()
    {
        while (true)
        {
            finger2.transform.DOMove(new Vector3(finger2.transform.position.x, finger2.transform.position.y + 0.3f, finger2.transform.position.z), 1f, false);
            yield return new WaitForSeconds(1f);
            finger2.transform.DOMove(new Vector3(finger2.transform.position.x, finger2.transform.position.y - 0.3f, finger2.transform.position.z), 1f, false);
            yield return new WaitForSeconds(1f);
            if(finger2.gameObject.activeSelf == false)
            {
                break;
            }
        }

    }

    void TutorialAnim()
    {
        StartCoroutine(TutoAnim());
    }

    IEnumerator TutoAnim()
    {
        while (true)
        {
            if (finger.gameObject.activeSelf == true)
            {
                finger.DOFade(0f, 1f);
                shopTutorialText.DOFade(0f, 1f);
                yield return new WaitForSeconds(1f);
                finger.DOFade(1f, 1f);
                shopTutorialText.DOFade(1f, 1f);
                yield return new WaitForSeconds(1f);
            }
            if (finger.gameObject.activeSelf == false)
            {
                break;
            }
        }
    }
}
