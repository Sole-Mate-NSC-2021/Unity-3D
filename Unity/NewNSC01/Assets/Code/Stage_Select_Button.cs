using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage_Select_Button : MonoBehaviour
{
    public static string scene;
    public void GO_TO_SCENE01() {
        scene = "Scene01";
        GameObject.Find("BlackTrans").GetComponent<Animator>().Play("Black_in");
    }
    public void GO_TO_SCENE02() {
        scene = "Scene02";
        GameObject.Find("BlackTrans").GetComponent<Animator>().Play("Black_in");
    }
}
