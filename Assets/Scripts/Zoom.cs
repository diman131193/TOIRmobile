using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zoom : MonoBehaviour {

    private Vector2 firstTouchPrevPosition, secondTouchPrevPosition;

    private float touchesPrevPosDifference, touchesCurPosDifference, zoomModifier, zoomZ;

    [Header("Options")]
    public float zoomModifierSpeed = 0.05f;

    [Header("Components")]
    public Transform Model;

	void Update () {
        if (Input.touchCount == 2)
        {
            Touch firstTouch = Input.GetTouch(0);
            Touch secondTouch = Input.GetTouch(1);

            firstTouchPrevPosition = firstTouch.position - firstTouch.deltaPosition;
            secondTouchPrevPosition = secondTouch.position - secondTouch.deltaPosition;

            touchesPrevPosDifference = (firstTouchPrevPosition - secondTouchPrevPosition).magnitude;
            touchesCurPosDifference = (firstTouch.position - secondTouch.position).magnitude;

            zoomModifier = (firstTouch.deltaPosition - secondTouch.deltaPosition).magnitude * zoomModifierSpeed;

            if (touchesPrevPosDifference > touchesCurPosDifference)
            {
                zoomZ = Mathf.Clamp(Model.position.z + zoomModifier, 15.0f, 60.0f);
            }

            if (touchesPrevPosDifference < touchesCurPosDifference)
            {
                zoomZ = Mathf.Clamp(Model.position.z - zoomModifier, 15.0f, 60.0f);
            }

            Model.position = new Vector3(Model.position.x, Model.position.y, zoomZ);
        }
    }
}
