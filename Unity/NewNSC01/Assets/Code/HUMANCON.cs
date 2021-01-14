using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUMANCON : MonoBehaviour
{
    public Animator anim;
    void Update()
    {
        anim.SetInteger("running",(int)Input.GetAxis("Horizontal"));
    }
}
