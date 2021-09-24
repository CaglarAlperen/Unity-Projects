using System;
using UnityEngine;

public class InputController : MBSingleton<InputController>
{
    private Vector2 dragStartPos;
    private Vector2 dragEndPos;
    public static event Action<Vector2> OnDragged;
    public static event Action OnSpace;

    private void Update()
    {
        if (SystemInfo.deviceType == DeviceType.Handheld)
        {
            TouchControl();
        }
        else
        {
            KeyControl();
        }
    }

    private void TouchControl()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                dragStartPos = touch.position;
            }

            if (touch.phase == TouchPhase.Ended)
            {
                dragEndPos = touch.position;
                OnDragged?.Invoke(dragEndPos - dragStartPos);
            }
        }
    }

    private void KeyControl()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            OnDragged?.Invoke(Vector2.up);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S)) 
        {
            OnDragged?.Invoke(Vector2.down);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            OnDragged?.Invoke(Vector2.right);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            OnDragged?.Invoke(Vector2.left);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnSpace?.Invoke();
        }
    }
}
