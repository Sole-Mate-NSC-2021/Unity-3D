using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSwitch : MonoBehaviour
{
    public bool isFuture;
    public bool enebleScript;
    public bool isBlock;
    public GameObject futureCharacter, nowCharacter;
    void Start()
    {

        isFuture = true;
    }
    void Update()
    {

        if (Input.GetKeyDown("c"))
        {
            isFuture = !isFuture;
        }
        futureCharacter.GetComponent<PlayerControl>().enabled = isFuture;
        //nowCharacter.GetComponent<PlayerControl>().enabled = !isFuture;
        futureCharacter.GetComponent<PathHighlight>().enabled = isFuture;
        nowCharacter.GetComponent<PathHighlight>().enabled = !isFuture;

        if (Input.GetKeyDown("r"))
        {
            isBlock = GameObject.Find("Door").GetComponent<BoxCollider>().enabled;
            GameObject.Find("Door").GetComponent<BoxCollider>().enabled = !isBlock;
        }

        //nowCharacter.GetComponent<PlayerControl>().enabled = !isFuture;
    }
}