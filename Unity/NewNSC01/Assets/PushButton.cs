using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushButton : MonoBehaviour
{    
    public class pairDoor
    {
        public int i;
        public int j;

        public pairDoor(int a_i, int a_j)
        {
            i = a_i;
            j = a_j;
        }
    };

    public GameObject[,] Button, Door;

    public GameObject RedButton, BlueButton, ButtonPressed, RedDoor, BlueDoor;

    PlayerControl playerControl;

    Vector3 lastRot, toRot, currentRot;

    public Animator anim;

    public List<pairDoor> lisDoor;

    Rewind rewind;

    int i, j;

    int ch = 0;

    public float rotSpeed = 9;

    bool changeDoor = false;

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

        anim = GetComponent<Animator>();

        playerControl = GetComponent<PlayerControl>();

        rewind = GetComponent<Rewind>();

        lisDoor = new List<pairDoor>();

    }

    // Update is called once per frame
    void Update()
    {
        if (ch == 1)
        {
            currentRot = Vector3.Lerp(currentRot, lastRot, rotSpeed * Time.deltaTime);
            transform.eulerAngles = currentRot;
            if (changeDoor)
            {
                changeDoor = false;
            }
            if (Mathf.Abs(transform.eulerAngles.y - lastRot.y) < 0.1f)
            {
                ch = 0;
            }
        }

        if (ch == 2)
        {
            currentRot = Vector3.Lerp(currentRot, toRot, rotSpeed * Time.deltaTime);
            transform.eulerAngles = currentRot;
            if (Mathf.Abs(transform.eulerAngles.y - toRot.y) < 0.1f)
            {
                currentRot = new Vector3(0, difRotation(lastRot.y, 0), 0);
                anim.SetFloat("push button", 1);
            }
        }

        else if (ch == 1)
        {
            currentRot = Vector3.Lerp(currentRot, lastRot, rotSpeed * Time.deltaTime);
            transform.eulerAngles = currentRot;
            if (Mathf.Abs(transform.eulerAngles.y - lastRot.y) < 0.1f)
            {
                ch = 0;
            }
        }

        if (!changeDoor && anim.GetCurrentAnimatorStateInfo(0).IsName("button push"))
        {
            //Debug.Log("Runnninin");
            //Debug.Log(anim.GetCurrentAnimatorStateInfo(0).normalizedTime);
            if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
            {
                Debug.Log("UUWUW");
                changeDoor = true;          
                anim.SetFloat("push button", 0);
            }
            else
            {
                //Debug.Log("not end");
            }
        }

        if (changeDoor && anim.GetCurrentAnimatorStateInfo(0).IsName("idle"))
        {


            if(rewind.localTimeScale == -1)
            {
                i = lisDoor[lisDoor.Count - 1].i; j = lisDoor[lisDoor.Count - 1].j;
                Debug.Log("HAHAHA");
                Debug.Log("Out : " + i + " " + j);
                lisDoor.Remove(lisDoor[lisDoor.Count - 1]);
            }
            else
            {
                Debug.Log("Add : " + i + " " + j);
                pairDoor tmp = new pairDoor(i, j);
                lisDoor.Add(tmp);
            }            
            ch = 1;
            bool isActive = Door[i, j].activeSelf;
            Door[i, j].SetActive(!isActive);

            changeDoor = false;
        }

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
                        ch = 2;
                        currentRot = lastRot;
                        toRot = new Vector3(0, difRotation(lastRot.y, 0), 0);
                    }
                }
            }
        }
    }
    float difRotation(float now, float to)
    {
        if(Mathf.Abs(to - now) < Mathf.Abs(to - now + 360))
        {
            return to;
        }
        return to + 360;
    }
}