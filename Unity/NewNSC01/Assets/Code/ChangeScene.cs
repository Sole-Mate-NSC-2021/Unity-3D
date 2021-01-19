using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ChangeScene : MonoBehaviour
{
    public static bool _is_change=false;
    public void chage_to_select_menu() {
        GameObject.Find("RawImage").GetComponent<Animator>().Play("Fade_in");
        _is_change=true;
    }
}
