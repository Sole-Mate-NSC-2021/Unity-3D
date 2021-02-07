using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverHighlight : MonoBehaviour
{

    //public GameObject Border;
    void Start()
    {
        if(this.tag == "Dot")
            this.GetComponent<BoxCollider>().size = new Vector3(10, 10, 10);
        //Border = GameObject.Find(this.gameObject.name + "_Border");
    }
    void OnMouseOver()
    {
        this.GetComponent<Renderer>().enabled = true;
        //Border.GetComponent<Renderer>().enabled = true;
    }
    void OnMouseExit()
    {
        this.GetComponent<Renderer>().enabled = false;
        //Border.GetComponent<Renderer>().enabled = false;
    }
}
