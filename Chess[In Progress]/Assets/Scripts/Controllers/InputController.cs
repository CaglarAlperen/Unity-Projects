using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    public static event Action<Vector2> OnClick;
    public static event Action<Vector2> OnHold;
    public static event Action OnHoldEnd;

    private float _holdTreshold = 0.2f;
    private float clickStartTime;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            clickStartTime = Time.time;
        }
        if (Input.GetMouseButton(0))
        {
            if (Time.time - clickStartTime > _holdTreshold)
            {
                OnHold?.Invoke(Input.mousePosition);
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            if (Time.time - clickStartTime < _holdTreshold)
            {
                OnClick?.Invoke(Input.mousePosition);
            }
            else
            {
                OnHoldEnd?.Invoke();
            }
        }
    }
}
