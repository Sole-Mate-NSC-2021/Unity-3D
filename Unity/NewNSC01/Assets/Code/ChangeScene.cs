using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ChangeScene : MonoBehaviour
{
    public static bool _is_change=false;
    public static string scenename;
    public GameObject pause, setting;

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
    public void Pause_On()
    {
        pause.SetActive(true);
    }
    public void Pause_Off()
    {
        pause.SetActive(false);
    }

    public void Setting_On()
    {
        pause.SetActive(false);
        setting.SetActive(true);
    }

    public void Setting_Off()
    {
        setting.SetActive(false);
        pause.SetActive(true);
    }

    public void Resetart()
    {
        int SS = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(SS);
        pause.SetActive(false);
    }
}
