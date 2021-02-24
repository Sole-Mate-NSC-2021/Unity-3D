using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamControl2 : MonoBehaviour
{
    public float panSpeed;
    public float ScreenBoarder = 10f;

    public Vector3 dragOrigin, dragPos, nextPos, realPos, tmpPos;

    public float leftX, rightX, leftY, rightY;

    void Start()
    {
        panSpeed = 1.0f;
        leftX = 31;
        rightX = -15;
        leftY = 14;
        rightY = 23;
    }

    void Update()
    {
        transform.position = new Vector3(Mathf.Min(19.0f, transform.position.x), Mathf.Max(19.0f, transform.position.y), transform.position.z);
        transform.position = new Vector3(Mathf.Max(19.0f, transform.position.x), Mathf.Min(25.5f, transform.position.y), transform.position.z);

        if (Input.GetMouseButtonDown(1))
        {
            dragOrigin = Input.mousePosition;
            return;
        }

        if (!Input.GetMouseButton(1))
            return;

        tmpPos = Camera.main.ScreenToViewportPoint(Input.mousePosition - dragOrigin);
        dragPos = Camera.main.ScreenToViewportPoint(dragOrigin - Input.mousePosition);
        nextPos = new Vector3(tmpPos.x * panSpeed, dragPos.y * panSpeed, 0);
        realPos = Camera.main.ViewportToWorldPoint(nextPos);

        nextPos = Camera.main.WorldToViewportPoint(realPos);
        nextPos.z = Camera.main.ScreenToViewportPoint(dragOrigin).z;
        transform.Translate(nextPos, Space.World);
        transform.position = new Vector3(Mathf.Min(19.0f, transform.position.x), Mathf.Max(19.0f, transform.position.y), transform.position.z);
        transform.position = new Vector3(Mathf.Max(19.0f, transform.position.x), Mathf.Min(25.5f, transform.position.y), transform.position.z);
    }
}
