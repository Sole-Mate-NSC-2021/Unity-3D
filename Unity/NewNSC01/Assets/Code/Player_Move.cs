using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Move : MonoBehaviour
{
    public float speed=0;
    float ho;
    void Update()
    {
        ho = Input.GetAxis("Horizontal")* Time.deltaTime*speed;
        transform.Translate(0, 0, ho);
    }
}
