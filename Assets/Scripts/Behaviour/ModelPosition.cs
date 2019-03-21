﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelPosition : MonoBehaviour
{
    private Vector2 moveDirection = Vector2.zero;
    //private Vector3 moveDirection = Vector3.zero;

    [Header("Components")]
    public Transform Model;

    [Header("Options")]
    public float speed = 10000.0f;

    private void Update()
    {
#if UNITY_IOS || UNITY_ANDROID
        if(Input.touchCount == 3)
        {
            Touch myTouch = Input.GetTouch(0);
            if(myTouch.phase == TouchPhase.Moved)
            {
                Vector2 positionChange = myTouch.deltaPosition;
                moveDirection = positionChange.normalized;
                Model.position += (Vector3)moveDirection * speed * Time.deltaTime;
            }
        }
        /*moveDirection.x = -Input.acceleration.y;
        moveDirection.z = Input.acceleration.x;
        if (moveDirection.sqrMagnitude > 1.0) moveDirection.Normalize();
        Model.Translate(moveDirection * speed * Time.deltaTime);*/
#endif
    }
}
