using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverMouse : MonoBehaviour
{
    void OnMouseOver()
    {
        Debug.Log("Mouse is over Dot01.");
    }
    void OnMouseExit()
    {
        Debug.Log("Mouse is not over 9000.");
    }
}
