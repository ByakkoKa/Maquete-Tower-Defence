using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoving : MonoBehaviour
{


    public float minX = 0, maxX = 16;
    private Vector3 dragOrigin; //Where are we moving?
    private Vector3 clickOrigin = Vector3.zero; //Where are we starting?
    private Vector3 basePos = Vector3.zero; //Where should the camera be initially?

    void FixedUpdate()
    {
        if (Input.GetMouseButton(0) && GameManager.currentChar == null )
        {
            if (clickOrigin == Vector3.zero)
            {
                clickOrigin = Input.mousePosition;
                basePos = transform.position;
            }
            dragOrigin = Input.mousePosition;
        }

        if (!Input.GetMouseButton(0))
        {
            clickOrigin = Vector3.zero;
            return;
        }

        transform.position = new Vector3(basePos.x + ((clickOrigin.x - dragOrigin.x) * .01f),
                                             0/*basePos.y + ((clickOrigin.y - dragOrigin.y) * .01f)*/,
                                             -10);

        if (transform.position.x < minX)
        {
            transform.position = new Vector3(minX, 0, transform.position.z);
        }
        if (transform.position.x > maxX)
        {
            transform.position = new Vector3(maxX, 0, transform.position.z);
        }
        if (transform.position.y != 0)
        {
            transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        }        
    }

    /*public float speed = 0.1F;
    void FixedUpdate()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;
            transform.Translate(-touchDeltaPosition.x * speed, -touchDeltaPosition.y * speed, 0);
        }
    }*/
}
