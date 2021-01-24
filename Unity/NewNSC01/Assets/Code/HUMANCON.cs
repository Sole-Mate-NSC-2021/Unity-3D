using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUMANCON : MonoBehaviour
{
    public Player_Move playerMove;
    public Animator anim;
    void Update()
    {
        anim.SetFloat("running", playerMove.animSpeed);
    }
}
