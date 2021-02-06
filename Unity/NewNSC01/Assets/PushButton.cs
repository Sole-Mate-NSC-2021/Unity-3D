using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushButton : MonoBehaviour
{

    public GameObject[,] Button, Door;

    public GameObject RedButton, BlueButton, ButtonPressed, RedDoor, BlueDoor;

    PlayerControl playerControl;

    Vector3 lastRot, toRot, currentRot;

    int i, j;

    int ch = 0;

    // Start is called before the first frame update
    void Start()
    {
        Button = new GameObject[2, 5];
        Button[1, 4] = RedButton;
        Button[1, 3] = BlueButton;

        Door = new GameObject[2, 5];
        Door[1, 4] = RedDoor;
        Door[1, 3] = BlueDoor;

        toRot = new Vector3(0, 0, 0);

        playerControl = GetComponent<PlayerControl>();

    }

    // Update is called once per frame
    void Update()
    {

        /*
        if (ch == 2)
        {
            currentRot = Vector3.Lerp(currentRot, toRot, 2 * Time.deltaTime);
            transform.eulerAngles = currentRot;
            if (transform.eulerAngles == )
            {
                Debug.Log("HASDA");
                bool isActive = Door[i, j].activeSelf;
                Door[i, j].SetActive(!isActive);
                ch = 1;
                currentRot = transform.eulerAngles;
            }
        }

        else if (ch == 1)
        {
            Debug.Log("AHA");
            currentRot = Vector3.Lerp(currentRot, lastRot, 2 * Time.deltaTime);
            transform.eulerAngles = currentRot;
            if (transform.eulerAngles == lastRot)
            {
                ch = 0;
            }
        }
        */
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray;
            RaycastHit hit;

            ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if(hit.collider.tag == "Button")
                {
                    ButtonPressed = hit.collider.gameObject;
                    i = ButtonPressed.name[6] - '0';
                    j = ButtonPressed.name[7] - '0';
                    if(playerControl.StartI == i && playerControl.StartJ == j)
                    {
                        lastRot = transform.eulerAngles;
                        /*
                        ch = 2;
                        currentRot = lastRot;
                        if(lastRot.y > 180)
                        {
                            toRot = new Vector3(0, 360, 0);
                        }
                        else
                        {
                            toRot = new Vector3(0, 0, 0);
                        }
                        */
                        //transform.eulerAngles = lastRot;
                    }
                }
            }
        }
    }
}
