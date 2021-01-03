using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    // Start is called before the first frame update

    private Transform target;

    float speed = 1.0f;
    
    void Start()
    {
        transform.position = new Vector3(0.0f, 0.0f, 0.0f);
        target.position = new Vector3(100.0f, 100.0f, 100.0f);
    }

    // Update is called once per frame
    void Update()
    {
        float step = speed * Time.deltaTime;
        transform.Translate(Vector3.forward * Time.deltaTime);
    }
}