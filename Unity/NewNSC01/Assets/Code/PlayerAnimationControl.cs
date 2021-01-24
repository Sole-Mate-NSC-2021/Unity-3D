using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationControl : MonoBehaviour
{
    public PlayerControl playerControl;
    public Animator anim;

    void Update()
    {
        //Debug.Log(playerControl.animSpeed);
        anim.SetFloat("running", playerControl.animSpeed);
    }
}
