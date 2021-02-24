using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class GoIntoGate2 : MonoBehaviour
{
    // Start is called before the first frame update

    public PlayerControl playerControl;
    public Rigidbody rb;
    public PathFinding pathFinding;
    public PathHighlight pathHighlight;
    public GenGrid grid;
    public Rewind rewind;
    public CharacterSwitch characterSwitch;
    public ChangeScene changeScene;

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
        changeScene = GetComponent<ChangeScene>();
    }

    public Vector3 toRot, currentRot;

    public int ch, TargetI, TargetJ;

    public float rotSpeed = 1.0f;

    public bool isReverse;

    // Update is called once per frame
    void Update()
    {

        if (this.gameObject.name == "CharacterPresent" && endFuture(2, 3))
        {
            Debug.Log("ERSDAD");
            changeScene.change_to_select_menu();
        }

        isReverse = endFuture(1, 0);

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

        else if (ch == 1)
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

        else if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            ClickGate(1, 3, "GoBlueGate");
            ClickGate(1, 1, "GoRedGate");
        }
    }

    void ClickGate(int a_i, int a_j, string gateName)
    {
        if (a_i == playerControl.StartI && a_j == playerControl.StartJ)
        {
            Ray ray;
            RaycastHit hit;

            ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {

                if (hit.collider.gameObject.name == gateName)
                {
                    if (gateName == "GoBlueGate")
                    {
                        currentRot = transform.eulerAngles;
                        toRot = new Vector3(0, 270, 0);
                        ch = 2;
                        TargetI = a_i + 1;
                        TargetJ = a_j;
                    }
                    else if (gateName == "GoRedGate")
                    {
                        currentRot = transform.eulerAngles;
                        toRot = new Vector3(0, difRotation(transform.eulerAngles.y, 180), 0);
                        ch = 2;
                        TargetI = a_i;
                        TargetJ = a_j - 1;
                    }
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
        //Debug.Log(playerControl.StartI + " " + playerControl.StartJ);
        if (a_i == playerControl.StartI && a_j == playerControl.StartJ)
        {
            return true;
        }
        return false;
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