using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ChangeScene : MonoBehaviour
{
    public static bool _is_change=false;
    public static string scenename;

    /*
    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "")
        SceneManager.LoadScene()
    }
    */

    public void change_to_select_menu() {
        scenename = "Level selection";
        Debug.Log("OMG");
        GameObject.Find("RawImage").GetComponent<Animator>().Play("Fade_in");
        _is_change=true;
    }
    public void change_to_setting() {
        scenename="Setting";
        GameObject.Find("RawImage").GetComponent<Animator>().Play("Fade_in");
        _is_change=true;
    }
}
