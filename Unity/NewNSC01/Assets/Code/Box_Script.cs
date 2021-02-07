using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box_Script : MonoBehaviour
{
    Vector3 fixedRotation;
    float maxX = 10.5f;
    // Start is called before the first frame update
    void Start()
    {
        fixedRotation = transform.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        transform.eulerAngles = fixedRotation;
        transform.position = new Vector3(Mathf.Min(transform.position.x, maxX), transform.position.y, transform.position.z);
    }
}
