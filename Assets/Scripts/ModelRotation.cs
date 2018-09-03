using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelRotation : MonoBehaviour
{
    [Header("Options")]
    public float rotationSpeed;
    public float joystickSensivity;

    [Header("Components")]
    public Transform Model;
    public Joystick joystick;

    void Update() {
        Vector3 moveVector = Vector3.right * joystick.Horizontal + Vector3.up * joystick.Vertical;

        Vector3 rotateDirection;

        if (moveVector.x > joystickSensivity)
        {
            Model.RotateAround(Model.position, Vector3.down, rotationSpeed);
        }

        if (moveVector.x < -joystickSensivity)
        {
            Model.RotateAround(Model.position, Vector3.up, rotationSpeed);
        }

        if (moveVector.y > joystickSensivity)
        {
            Model.RotateAround(Model.position, Vector3.right, rotationSpeed);
        }

        if (moveVector.y < -joystickSensivity)
        {
            Model.RotateAround(Model.position, Vector3.left, rotationSpeed);
        }
    }
}
