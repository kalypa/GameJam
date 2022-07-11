using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    DragManager dragManager;

    void Awake()
    {
        dragManager = GetComponent<DragManager>();
        dragManager.setOnSwipeDetected(MyOnSwipeDetected);
    }
    void MyOnSwipeDetected(Vector3 swipeDirection)
    {
        if(swipeDirection.x != 0 || swipeDirection.y != 0)
        {
            if(Mathf.Abs(swipeDirection.x) > Mathf.Abs(swipeDirection.y))
            {
                Debug.Log("가로베기");
            }
            else if(Mathf.Abs(swipeDirection.y) > Mathf.Abs(swipeDirection.x))
            {
                Debug.Log("세로베기");
            }
        }
    }
}
