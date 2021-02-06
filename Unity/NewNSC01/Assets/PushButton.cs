using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushButton : MonoBehaviour
{

    public GameObject[,] Button, Door;

    public GameObject RedButton, BlueButton, ButtonPressed, RedDoor, BlueDoor;

    PlayerControl playerControl;

    // Start is called before the first frame update
    void Start()
    {
        Button = new GameObject[2, 5];
        Button[1, 4] = RedButton;
        Button[1, 3] = BlueButton;

        Door = new GameObject[2, 5];
        Door[1, 4] = RedDoor;
        Door[1, 3] = BlueDoor;

        playerControl = GetComponent<PlayerControl>();

    }

    // Update is called once per frame
    void Update()
    {
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
                    int i = ButtonPressed.name[6] - '0';
                    int j = ButtonPressed.name[7] - '0';
                    if(playerControl.StartI == i && playerControl.StartJ == j)
                    {
                        bool isActive = Door[i, j].activeSelf;
                        Door[i, j].SetActive(!isActive);
                    }
                }
            }
        }
    }
}
