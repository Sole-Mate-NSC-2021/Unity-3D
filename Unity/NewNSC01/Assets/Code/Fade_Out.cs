using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Fade_Out : MonoBehaviour
{
    void Update()
    {
        if(ChangeScene._is_change==true) {
            ChangeScene._is_change=false;
            gameObject.GetComponent<Animator>().Play("Fade_out");
        }
    }
}
