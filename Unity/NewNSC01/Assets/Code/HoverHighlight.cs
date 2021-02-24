using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverHighlight : MonoBehaviour
{

    public GameObject Border;

    //public GameObject Border;
    void Start()
    {
        if(this.tag == "Dot")
            this.GetComponent<BoxCollider>().size = new Vector3(10, 10, 10);
        Border = GameObject.Find(this.gameObject.name + "_Border");
    }
    void OnMouseOver()
    {
        if(this.tag == "Box")
        {
            Border.GetComponent<Renderer>().enabled = true;
            return;
        }
        //Debug.Log(this.name);
        this.GetComponent<Renderer>().enabled = true;
        //Border.GetComponent<Renderer>().enabled = true;
    }
    void OnMouseExit()
    {
        if (this.tag == "Box")
        {
            Border.GetComponent<Renderer>().enabled = false;
            return;
        }
        this.GetComponent<Renderer>().enabled = false;
        //Border.GetComponent<Renderer>().enabled = false;
    }
}
