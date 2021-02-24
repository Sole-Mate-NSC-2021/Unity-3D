using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LetMeWalk : MonoBehaviour
{

    Rewind rewind;

    public GameObject presentCharacter;

    private void Start()
    {
        presentCharacter = GameObject.Find("CharacterPresent");
        rewind = GameObject.Find("CharacterFuture").GetComponent<Rewind>();
    }

    private void Update()
    {

        //Debug.Log(rewind.localTimeScale);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(rewind.localTimeScale == -1 && presentCharacter.GetComponent<Push_Box>().isOut == true)
        {
            if(other.name == "CharacterFuture")
            {
                presentCharacter.GetComponent<Push_Box>().letWalk = true;
            }
        }
    }
}
