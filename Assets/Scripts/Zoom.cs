using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zoom : MonoBehaviour {

    private Vector2 firstTouchPrevPosition, secondTouchPrevPosition;

    private float touchesPrevPosDifference, touchesCurPosDifference, zoomModifier;

    [Header("Options")]
    public float zoomModifierSpeed;

    [Header("Components")]
    public Camera cam;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
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
                cam.orthographicSize += zoomModifier;
                //Model.Translate(Model.position.x, Model.position.y, Model.position.z + zoomModifier);
            }

            if (touchesPrevPosDifference < touchesCurPosDifference)
            {
                cam.orthographicSize -= zoomModifier;
                //Model.Translate(Model.position.x, Model.position.y, Model.position.z - zoomModifier);
            }
            
            
        }
        cam.orthographicSize = Mathf.Clamp(cam.orthographicSize, 2.0f, 10.0f);
        Debug.Log(cam.orthographicSize);

    }
}
