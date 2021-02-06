using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoIntoGate : MonoBehaviour
{
    // Start is called before the first frame update

    public PlayerControl playerControl;
    public Rigidbody rb;
    public PathFinding pathFinding;
    public PathHighlight pathHighlight;
    public GenGrid grid;
    public Rewind rewind;
    public CharacterSwitch characterSwitch;

    public List<GenNode> lis;

    bool isFuture, isPresent = true;

    void Start()
    {
        playerControl = GetComponent<PlayerControl>();
        rb = GetComponent<Rigidbody>();
        pathFinding = GetComponent<PathFinding>();
        pathHighlight = GetComponent<PathHighlight>();
        grid = GetComponent<GenGrid>();
        rewind = GetComponent<Rewind>();
        characterSwitch = GameObject.Find("CharacterSwitch").GetComponent<CharacterSwitch>();
    }

    public Vector3 toRot, currentRot;

    public int ch, TargetI, TargetJ;

    public float rotSpeed = 1.0f;

    public bool isReverse;

    // Update is called once per frame
    void Update()
    {

        isReverse = endFuture(1, 5);

        if (isReverse && isPresent)
        {
            isPresent = false;
            StartCoroutine(toReverse());
        }

        if (ch == 2)
        {
            currentRot = Vector3.Lerp(currentRot, toRot, rotSpeed * Time.fixedDeltaTime);
            if (Mathf.Abs(currentRot.y - toRot.y) < 0.2f)
            {
                transform.eulerAngles = currentRot;
                ch = 1;
            }
            else
            {
                transform.eulerAngles = currentRot;
            }
        }

        else if(ch == 1)
        {
            pathFinding.TargetI = TargetI;
            pathFinding.TargetJ = TargetJ;
            pathFinding.StartI = playerControl.StartI;
            pathFinding.StartJ = playerControl.StartJ;

            pathHighlight.TargetI = TargetI;
            pathHighlight.TargetJ = TargetJ;

            lis = grid.FinalPath;
            ch = 0;            
            playerControl.it = 1;
        }

        else if (Input.GetMouseButtonDown(0))
        {
            ClickGate(1, 4, "GoRedGate");
            ClickGate(0, 4, "GoBlueGate");
        }
    }

    void ClickGate(int a_i, int a_j, string gateName)
    {
        if(a_i == playerControl.StartI && a_j == playerControl.StartJ)
        {                
            Ray ray;
            RaycastHit hit;

            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            
            if (Physics.Raycast(ray, out hit))
            {

                if (hit.collider.gameObject.name == gateName)
                {
                    currentRot = transform.eulerAngles;
                    toRot = new Vector3(0, difRotation(transform.eulerAngles.y, 90), 0);
                    ch = 2;
                    TargetI = a_i;
                    TargetJ = a_j + 1;
                }
            }
        }
    }
    float difRotation(float now, float to)
    {
        if (Mathf.Abs(to - now) < Mathf.Abs(to - now + 360))
        {
            return to;
        }
        return to + 360;
    }
    bool endFuture(int a_i, int a_j)
    {
        if(a_i == playerControl.StartI && a_j == playerControl.StartJ)
        {
            return true;
        }
        return false;
    }

    IEnumerator toReverse()
    {
        yield return new WaitForSeconds(2.5f);
        rewind.localTimeScale = -1;
        isFuture = characterSwitch.isFuture;
        characterSwitch.isFuture = !isFuture;
    }

}
