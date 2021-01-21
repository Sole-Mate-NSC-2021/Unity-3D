using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Fade_change : MonoBehaviour {

    void Update() {
        if(gameObject.GetComponent<RawImage>().color.a == 1f) {
            SceneManager.LoadScene(ChangeScene.scenename);
        }
    }
}
