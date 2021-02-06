using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamControl : MonoBehaviour
{
    public float panSpeed;
    public float ScreenBoarder = 10f;

    public Vector3 dragOrigin, dragPos, nextPos, realPos;

    public float leftX, rightX, leftY, rightY;

    void Start()
    {
        panSpeed = 1.0f;
        leftX = -25;
        rightX = -15;
        leftY = 14;
        rightY = 23;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            dragOrigin = Input.mousePosition;
            return;
        }

        if (!Input.GetMouseButton(1)) 
            return;

        dragPos = Camera.main.ScreenToViewportPoint(dragOrigin - Input.mousePosition);
        nextPos = new Vector3(dragPos.x * panSpeed, dragPos.y * panSpeed, 0);
        realPos = Camera.main.ViewportToWorldPoint(nextPos);

        realPos.x = Mathf.Max(realPos.x, leftX);
        realPos.x = Mathf.Min(realPos.x, rightX);

        realPos.y = Mathf.Max(realPos.y, leftY);
        realPos.y = Mathf.Min(realPos.y, rightY);

        nextPos = Camera.main.WorldToViewportPoint(realPos);
        nextPos.z = Camera.main.ScreenToViewportPoint(dragOrigin).z;
        transform.Translate(nextPos, Space.World);
    }
}
