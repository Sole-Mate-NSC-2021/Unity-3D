using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Black_ch : MonoBehaviour
{
    void Update()
    {
        if(gameObject.GetComponent<Image>().color.a==1) {
            SceneManager.LoadScene(Stage_Select_Button.scene);
        }
    }
}
