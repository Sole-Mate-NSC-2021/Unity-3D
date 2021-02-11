using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Box_Script : MonoBehaviour
{
    Vector3 fixedRotation;
    float maxX = 10.5f;
    public GameObject futureCharacter, presentCharacter;
    public bool isUp;
    // Start is called before the first frame update

    void Start()
    {
        fixedRotation = transform.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y > 8)
        {
            isUp = true;
        }
        else
        {
            isUp = false;
        }
        futureCharacter.GetComponent<Push_Box>().isUp = isUp;
        presentCharacter.GetComponent<Push_Box>().isUp = isUp;
        transform.eulerAngles = fixedRotation;
        transform.position = new Vector3(Mathf.Min(transform.position.x, maxX), transform.position.y, transform.position.z);
    }
}
