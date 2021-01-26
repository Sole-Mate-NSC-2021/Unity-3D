using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSwitch : MonoBehaviour
{
    public bool isFuture;
    public bool enebleScript;
    public GameObject futureCharacter, nowCharacter;

    void Start()
    {
        isFuture = true;
        futureCharacter = GameObject.Find("astronaustBody");
        nowCharacter = GameObject.Find("Character2");
    }
    void Update()
    {
        if (Input.GetKeyDown("c"))
        {
            isFuture = !isFuture;
        }
        futureCharacter.GetComponent<PlayerControl>().enabled = isFuture;
        //nowCharacter.GetComponent<PlayerControl>().enabled = !isFuture;
    }
}
