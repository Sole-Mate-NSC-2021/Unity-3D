using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Move : MonoBehaviour
{
    int dir=0;
    public float speed=0, smooth=1;
    float ho, angle=90f,a;
    void Update()
    {
        a = Input.GetAxis("Horizontal");
        ho = a*Time.deltaTime*speed;
        if( dir == 0 && a<0) {
            transform.rotation *= Quaternion.Euler(0, 180f, 0);
            dir=1;
        }
        else if(dir == 1 && a>0) {
            transform.rotation *= Quaternion.Euler(0, -180f, 0); 
            dir=0;
        }
        transform.Translate(0, 0, Mathf.Abs(ho) );
    }
}
