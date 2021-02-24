using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Push_Box : MonoBehaviour
{
    // Start is called before the first frame update


    public Vector3 lastRot, toRot, currentRot, lastPos;

    public bool isFuture = true, isUp, isPushing = false, wasPushing = false, attach = false, a_isPushing = false, a_wasPushing = false;
    public bool isOut, letWalk = false;
    public int i, j, ch, ch2, cnt, cnt2;
    public float rotSpeed = 2.0f;
    public GameObject box, otherCharacter, endDot;

    public Animator anim;

    public PlayerControl playerControl;

    HingeJoint hj;

    Rigidbody rb;

    CharacterSwitch characterSwitch;

    void Start()
    {

        letWalk = false;

        playerControl = GetComponent<PlayerControl>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        characterSwitch = GameObject.Find("CharacterSwitch").GetComponent<CharacterSwitch>();
    }

    // Update is called once per frame
    void Update()
    {
        a_isPushing = characterSwitch.futureCharacter.GetComponent<Push_Box>().isPushing;        
        a_wasPushing = characterSwitch.futureCharacter.GetComponent<Push_Box>().wasPushing;
        isFuture = characterSwitch.isFuture;
        rb.useGravity = true;
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
        if(ch2 == 4)
        {
            currentRot = Vector3.Lerp(currentRot, toRot, rotSpeed * Time.fixedDeltaTime);
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, currentRot.y, transform.eulerAngles.z);
            if (Mathf.Abs(currentRot.y - toRot.y) < 0.1f)
            {
                ch2 = 3;
                //anim.SetFloat("push box", 1);
                //currentRot = transform.eulerAngles;
                //anim.speed = 2.0f;
            }
        }

        else if(ch2 == 3)
        {
            if(cnt >= 10)
            {
                attach = true;
                anim.SetFloat("running", 0f);
                ch2 = 2;
            }
            else
            {
                anim.SetFloat("running", 10f);
                rb.MovePosition(transform.position + transform.forward * 0.1f);
            }
        }
        else if(ch2 == 2 && attach)
        {
            if (a_wasPushing)
            {
                Debug.Log("Fin");
                attach = false;
                this.transform.parent = null;
                ch2 = 1;
                isOut = true;
            }
            else {
                //Debug.Log("Isattach");
                this.transform.parent = box.transform;
                transform.eulerAngles = new Vector3(0, 270, 0);
            }
        }

        else if(ch2 == 1 && letWalk)
        {
            
            transform.eulerAngles = new Vector3(0, 270, 0);
            if (Mathf.Abs(endDot.transform.position.x - transform.position.x) > 0.1f)
            {
                anim.SetFloat("running", 10f);
                rb.MovePosition(transform.position + transform.forward * 0.1f);
            }
            else
            {
                playerControl.StartI = 1;
                playerControl.StartJ = 3;
                cnt2 = 0;
                ch2 = 0;
                anim.SetFloat("running", 0f);
            }
            
            /*
            if(cnt2 >= 25)
            {
                playerControl.StartI = 1;
                playerControl.StartJ = 3;
                cnt2 = 0;
                ch2 = 0;
                anim.SetFloat("running", 0f);
            }
            else
            {
                cnt++;
                anim.SetFloat("running", 10f);
                rb.MovePosition(transform.position + transform.forward * 0.1f);
            }*/

        }

        if (ch == 2)
        {
            currentRot = Vector3.Lerp(currentRot, toRot, rotSpeed * Time.fixedDeltaTime);
            transform.eulerAngles = new Vector3 (transform.eulerAngles.x, currentRot.y, transform.eulerAngles.z);
            if (Mathf.Abs(currentRot.y - toRot.y) < 0.1f)
            {
                anim.SetFloat("push box", 1);
                //currentRot = transform.eulerAngles;
                //anim.speed = 2.0f;
            }
        }

        else if(!isPushing && ch == 1)
        {
            currentRot = Vector3.Lerp(currentRot, lastRot, rotSpeed * Time.fixedDeltaTime);
            transform.eulerAngles = new Vector3 (transform.eulerAngles.x, currentRot.y, transform.eulerAngles.z);
            if (Mathf.Abs(currentRot.y - lastRot.y) < 0.1f)
            {
                currentRot = new Vector3(0, difRotation(lastRot.y, 0), 0);
                ch = 0;
                //anim.speed = 2.0f;
            }
        }

        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Box_Push"))
        {
            //Debug.Log(isPushing);
            isPushing = true;
            //if(anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
            //{
            if (!isUp)
            {
                anim.SetFloat("push box", 0);
                ch = 1;
            }
            else
            {
                rb.MovePosition(transform.position + transform.forward * 0.01f);
            }
                //anim.SetFloat("push box", 0);
            //}
        }

        else if (isPushing && !anim.GetCurrentAnimatorStateInfo(0).IsName("Box_Push"))
        {
            Debug.Log(isPushing);
            if(!isFuture)
                wasPushing = true;
            if (Mathf.Abs(lastPos.x - transform.position.x) < 0.1f)
            {
                anim.SetFloat("running", 0f);
                isPushing = false;
            }
            else
            {
                anim.SetFloat("running", 10.0f);
                rb.MovePosition(transform.position - transform.forward * 0.05f);
            }
        }

        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            Ray ray;
            RaycastHit hit;
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (!Physics.Raycast(ray, out hit) || hit.collider.tag != "Box")
                return;
            //Debug.Log("FUUUUDAS");
            if (playerControl.StartI == 1 && playerControl.StartJ == 3 && isFuture)
            {
                lastPos = transform.position;
                lastRot = transform.eulerAngles;
                ch = 2;
                currentRot = lastRot;
                toRot = new Vector3(0, difRotation(lastRot.y, 90), 0);
            }
            else if(playerControl.StartI == 0 && playerControl.StartJ == 3 && !isFuture)
            {
                lastRot = transform.eulerAngles;
                ch2 = 4;
                cnt = 0;
                currentRot = lastRot;
                toRot = new Vector3(0, difRotation(lastRot.y, 270), 0);
                //BoxPresent();
            }
        }

    }
    void BoxFuture()
    {
        anim.SetFloat("push box", 1);
    }
    void BoxPresent()
    {
    }
    float difRotation(float now, float to)
    {
        if (Mathf.Abs(to - now) < Mathf.Abs(to - now + 360))
        {
            return to;
        }
        return to + 360;
    }
    private void OnCollisionStay(Collision other)
    {
        if (other.collider.name == "Box")
        {
            //Debug.Log("IN");
            cnt++;
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if(other.collider.name == "Box")
        {
            Debug.Log("Yehee");
            isOut = false;
        }
    }
}
