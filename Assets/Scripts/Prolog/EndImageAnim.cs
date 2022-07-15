using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndImageAnim : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SetAnim());
    }

    IEnumerator SetAnim()
    {
        while (true)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + 5, transform.position.z);
            yield return new WaitForSeconds(1f);
            transform.position = new Vector3(transform.position.x, transform.position.y - 5, transform.position.z);
            yield return new WaitForSeconds(1f);
        }
    }
}
