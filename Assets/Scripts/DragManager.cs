using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class DragManager : MonoSingleton<DragManager>
{
    Vector3 screenSize;
    float minSwipeDist;
    Vector3 swipeDirection;
    Action<Vector3> actionOnSwipeDetected;

    void Awake()
    {
        Vector3 screenSize = new Vector3(Screen.width, Screen.height);
        minSwipeDist = Mathf.Max(screenSize.x, screenSize.y) / 14;
    }

    void Update()
    {
        #if UNITY_ANDROID
            processMobileInput();
        #else
            processInput();
        #endif
    }
    Vector3 touchDownPos;
    bool swiped = false;
    void processMobileInput()
    {
        if(Input.touches.Length > 0)
        {
            Touch t = Input.GetTouch(0);
            if(t.phase == TouchPhase.Began)
            {
                touchDownPos = new Vector3(t.position.x, t.position.y);
                swiped = false;
            }
            else if(t.phase == TouchPhase.Moved)
            {
                Vector3 currentTouchPos = new Vector3(t.position.x, t.position.y);
                bool swipeDetected = checkSwipe(touchDownPos, currentTouchPos);
                swipeDirection = (currentTouchPos - touchDownPos).normalized;
                if (swipeDetected)
                    onSwipeDetected(swipeDirection);
            }
            else if(t.phase == TouchPhase.Ended)
            {
                swiped = true;
            }
        }
    }
    Vector3 mouseDownPos;
    void processInput()
    {
        if(Input.GetMouseButtonDown(0) == true)
        {
            mouseDownPos = Input.mousePosition;
            swiped = false;
        }
        else if(Input.GetMouseButton(0) == true)
        {
            bool swipeDetected = checkSwipe(mouseDownPos, Input.mousePosition);
            swipeDirection = (Input.mousePosition - mouseDownPos).normalized;
            if (swipeDetected)
                onSwipeDetected(swipeDirection);
        }
        else if(Input.GetMouseButtonUp(0) == true)
        {
            swiped = true;
        }
    }
    bool checkSwipe(Vector3 downPos, Vector3 currentPos)
    {
        if(swiped == true)
            return false;
        if(isInputBlocked == true)
            return false;
        Vector3 currentSwipe = currentPos - downPos;
        if(currentSwipe.magnitude >= minSwipeDist)
        {
            return true;
        }
        return false;
    }

    public void setOnSwipeDetected(Action<Vector3> onSwipeDetected)
    {
        actionOnSwipeDetected = onSwipeDetected;
    }

    void onSwipeDetected(Vector3 swipeDirection)
    {
        swiped = true;
        actionOnSwipeDetected(swipeDirection);
    }
    private bool isInputBlocked = false;
    public void blockInput()
    {
        isInputBlocked = true;
    }
}
