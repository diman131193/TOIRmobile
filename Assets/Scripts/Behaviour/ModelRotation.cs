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
    public Joystick rightJoystick;

    void Update() {
        Vector3 rightMoveVector = Vector3.right * rightJoystick.Horizontal + Vector3.up * rightJoystick.Vertical;

        if (rightMoveVector.x > joystickSensivity)
        {
            Model.RotateAround(Model.position, Vector3.down, rotationSpeed);
        }

        if (rightMoveVector.x < -joystickSensivity)
        {
            Model.RotateAround(Model.position, Vector3.up, rotationSpeed);
        }

        if (rightMoveVector.y > joystickSensivity)
        {
            Model.RotateAround(Model.position, Vector3.right, rotationSpeed);
        }

        if (rightMoveVector.y < -joystickSensivity)
        {
            Model.RotateAround(Model.position, Vector3.left, rotationSpeed);
        }
    }
}
