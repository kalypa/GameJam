using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class tutorialManager : MonoBehaviour
{
    [SerializeField]
    private GameObject fingerObject;
    [SerializeField]
    private GameObject fingerObject2;
    [SerializeField]
    private GameObject One;
    [SerializeField]
    private Text tutorialText;
    [SerializeField]
    private Text tutorialText2; 
    [SerializeField]
    private Transform[] fingerTrnasform;
    [SerializeField]
    private GameObject tutorialCanvas;

    [SerializeField]
    private GameObject clearfilter;

    [SerializeField]
    private GameObject dirSwordH;
    [SerializeField]
    private GameObject dirSwordV;

    Vector3 horizontalObject;
    Vector3 verticalObject;

    public void TutorialStart()
    {
        if(GameManager.Instance.playerData.highScore == 0)
        {
            tutorialCanvas.gameObject.SetActive(true);
            One.gameObject.SetActive(true);
            tutorialText.gameObject.SetActive(true);
            tutorialText2.gameObject.SetActive(true);
            horizontalObject = fingerObject.transform.position;
            verticalObject = fingerObject2.transform.position;
            clearfilter.SetActive(true);
            if (TowerSpawner.Instance.isHorizontal == true)
            {
                dirSwordH.gameObject.SetActive(true);
                fingerObject.SetActive(true);
            }
            else if(TowerSpawner.Instance.isVertical == true)
            {
                dirSwordV.gameObject.SetActive(true);
                fingerObject2.SetActive(true);
            }

            TutoAnim();
        }
    }

    private void Update()
    {

        if (Input.anyKeyDown)
        {
            tutorialCanvas.gameObject.SetActive(false);
            fingerObject.SetActive(false);
            fingerObject2.SetActive(false);
            dirSwordH.SetActive(false);
            dirSwordV.SetActive(false);
            tutorialText.gameObject.SetActive(false);
            One.SetActive(false);
            clearfilter.SetActive(false);
            tutorialText2.gameObject.SetActive(false);
        }
    }

    private void TutoAnim()
    {
        StartCoroutine(TuToAnim());
    }

    IEnumerator TuToAnim()
    {
        while (true)
        {
            fingerObject.transform.DOMove(fingerTrnasform[0].position, 3f, false);
            fingerObject2.transform.DOMove(fingerTrnasform[1].position, 3f, false);
            dirSwordH.transform.DOMove(new Vector3(fingerTrnasform[0].position.x, dirSwordH.transform.position.y, dirSwordH.transform.position.z), 3f, false);
            dirSwordV.transform.DOMove(new Vector3(dirSwordV.transform.position.x, fingerTrnasform[1].position.y, dirSwordV.transform.position.z), 3f, false);

            StartCoroutine(ToFade());
            yield return new WaitForSeconds(3f);
            fingerObject.transform.position = horizontalObject;
            fingerObject2.transform.position = verticalObject;
            dirSwordH.transform.position = new Vector3(horizontalObject.x, dirSwordH.transform.position.y, dirSwordH.transform.position.z);
            dirSwordV.transform.position = new Vector3(dirSwordV.transform.position.x, verticalObject.y, dirSwordH.transform.position.z);
        }
    }

    IEnumerator ToFade()
    {
        One.GetComponent<Image>().DOFade(0f, 1.5f);
        tutorialText.DOFade(0f, 1.5f);
        tutorialText2.DOFade(0f, 1.5f);
        yield return new WaitForSeconds(1.5f);
        One.GetComponent<Image>().DOFade(1f, 1.5f);
        tutorialText.DOFade(1f, 1.5f);
        tutorialText2.DOFade(1f, 1.5f);
        yield return new WaitForSeconds(1.5f);
    }

}
