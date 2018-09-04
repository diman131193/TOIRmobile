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
    public Joystick leftJoystick;
    public Joystick rightJoystick;

    void Update() {
        Vector3 leftMoveVector = Vector3.right * leftJoystick.Horizontal + Vector3.up * leftJoystick.Vertical;
        Vector3 rightMoveVector = Vector3.right * rightJoystick.Horizontal + Vector3.up * rightJoystick.Vertical;

        if (leftMoveVector.x > joystickSensivity || rightMoveVector.x > joystickSensivity)
        {
            Model.RotateAround(Model.position, Vector3.down, rotationSpeed);
        }

        if (leftMoveVector.x < -joystickSensivity || rightMoveVector.x < -joystickSensivity)
        {
            Model.RotateAround(Model.position, Vector3.up, rotationSpeed);
        }

        if (leftMoveVector.y > joystickSensivity || rightMoveVector.y > joystickSensivity)
        {
            Model.RotateAround(Model.position, Vector3.right, rotationSpeed);
        }

        if (leftMoveVector.y < -joystickSensivity || rightMoveVector.y < -joystickSensivity)
        {
            Model.RotateAround(Model.position, Vector3.left, rotationSpeed);
        }
    }
}
