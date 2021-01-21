using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Start_Game : MonoBehaviour
{
    void Start() {
        Destroy(GameObject.Find("audio_source"));
    }
}
