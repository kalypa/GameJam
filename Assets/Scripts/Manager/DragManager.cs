using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class DragManager : MonoSingleton<DragManager>
{
    Vector3 screenSize; //스크린 크기
    float minSwipeDist; //최소 스와이프 거리
    Vector3 swipeDirection; //스와이프 방향
    Action<Vector3> actionOnSwipeDetected; // 스와이프 감지

    void Awake()
    {
        Vector3 screenSize = new Vector3(Screen.width, Screen.height);
        minSwipeDist = Mathf.Max(screenSize.x, screenSize.y) / 14;
    }

    void Update()
    {
        #if UNITY_ANDROID //안드로이드
            processMobileInput();
        #else
            processInput(); //PC
        #endif
    }
    Vector3 touchDownPos;
    bool swiped = false;
    void processMobileInput() //모바일 스와이프 함수
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
    void processInput() //PC 스와이프 함수
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
    bool checkSwipe(Vector3 downPos, Vector3 currentPos) //스와이프 확인
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

    public void setOnSwipeDetected(Action<Vector3> onSwipeDetected) //스와이프 감지 확인
    {
        actionOnSwipeDetected = onSwipeDetected;
    }

    void onSwipeDetected(Vector3 swipeDirection) //스와이프 감지
    {
        swiped = true;
        actionOnSwipeDetected(swipeDirection);
    }
    private bool isInputBlocked = false;
    public void blockInput() //스와이프 막기
    {
        isInputBlocked = true;
    }
}
