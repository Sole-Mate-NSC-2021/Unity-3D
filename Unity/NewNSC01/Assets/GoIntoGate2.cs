using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoIntoGate2 : MonoBehaviour
{
    Animator anim;
    PlayerControl playerControl;
    Rigidbody rb;
    PathHighlight pathHighlight;
    CharacterSwitch characterSwitch;
    Rewind rewind;

    public bool isRunning = false, isFuture;

    private void Start()
    {
        playerControl = GetComponent<PlayerControl>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        pathHighlight = GetComponent<PathHighlight>();
        characterSwitch = GetComponent<CharacterSwitch>();
        rewind = GetComponent<Rewind>();
    }

    void Update()
    {
        if (isFuture)
        {
            if (isRunning)
            {
                rb.MovePosition(transform.position + transform.forward);
                anim.SetFloat("running", 10);
            }
            else
            {
                anim.SetFloat("running", 0);
            }
        }

        Ray ray;
        RaycastHit hit;
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.name == "RedGate")
                {
                    Debug.Log(this.name);
                    if (isFuture)
                    {
                        Debug.Log("Red");
                        isRunning = true;
                    }
                }
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "RedCollider")
        {
            Debug.Log("MANMA");
            isRunning = false;
            anim.SetFloat("running", 0);
            toReverse();
        }
    }
    IEnumerator toReverse()
    {
        pathHighlight.enabled = false;
        yield return new WaitForSeconds(2.5f);
        rewind.localTimeScale = -1;
        isFuture = characterSwitch.isFuture;
        characterSwitch.isFuture = !isFuture;
    }

}
