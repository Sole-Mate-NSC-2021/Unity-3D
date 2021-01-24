using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamControl : MonoBehaviour
{
    public float _panspeed = 20f;
    public float _ScreenBoarder = 10f;
    
   
    void Update()
    {
        Vector3 pos = transform.position;

        if (Input.mousePosition.y >= Screen.height - _ScreenBoarder)
        {
            pos.z += _panspeed * Time.deltaTime;
        }


        if (Input.mousePosition.y <= _ScreenBoarder)
        {
            pos.z -= _panspeed * Time.deltaTime;
        }

        if (Input.mousePosition.x >= Screen.width - _ScreenBoarder)
        {
            pos.x += _panspeed * Time.deltaTime;
        }


        if (Input.mousePosition.x <= _ScreenBoarder)
        {
            pos.x -= _panspeed * Time.deltaTime;
        }
        transform.position = pos;
    }
}
