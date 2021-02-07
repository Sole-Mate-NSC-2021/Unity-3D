using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] private GameObject clickMarkerPrefab;
    [SerializeField] private Transform visualObjectsParent;
    GenNode targetNode;
    GenGrid grid;
    PathHighlight pathHighlight;
    PathFinding pathFinding;
    AudioSource ad;
    Rigidbody rb;
    
    List<GenNode> lis;

    public GameObject dotPointed, dotHovered;
    Vector3 UpRot, DownRot, RightRot, LeftRot;
    Quaternion targetRot;
    public float speed, animSpeed, smooth = 1.0f;
    public bool isRunning = false, isWall, gameStart = true;

    float maxSpeed = 8.0f, maxAnimSpeed = 10.0f;
    public int StartI, StartJ, TargetI, TargetJ, NextI, NextJ, BeginStartI, BeginStartJ;
    public int it, NextRot;

    public string nextDotName;

    void Start()
    {
        ad = GetComponent<AudioSource>();        
        rb = GetComponent<Rigidbody>();      
        
        grid = GetComponent<GenGrid>();
        pathHighlight = GetComponent<PathHighlight>();
        pathFinding = GetComponent<PathFinding>();

        animSpeed = 0;
        speed = maxSpeed;

        UpRot = new Vector3(0, 0, 0);
        DownRot = new Vector3(0, 180, 0);
        LeftRot = new Vector3(0, -90, 0);
        RightRot = new Vector3(0, 90, 0);

        StartI = BeginStartI;
        StartJ = BeginStartJ;

        ad.Stop();
    }
    void Update()
    {
        lis = grid.FinalPath;

        TargetI = pathHighlight.TargetI;
        TargetJ = pathHighlight.TargetJ;

        if ((StartI != TargetI) || (StartJ != TargetJ))
        {           
            if (gameStart)
            {
                NextI = TargetI;
                NextJ = TargetJ;
            }

            else {
                NextI = lis[it].gridI;
                NextJ = lis[it].gridJ;
            }



            targetNode = grid.AccessNode(NextI, NextJ);

            nextDotName = "Dot" + NextI + NextJ;
            
            if(NextI - StartI != 0)
            {
                NextRot = NextI - StartI;
                if(NextRot > 0)
                {
                    transform.eulerAngles = UpRot;
                }
                else
                {
                    transform.eulerAngles = DownRot;
                }
            }
            else
            {
                NextRot = NextJ - StartJ;
                if(NextRot > 0)
                {
                    transform.eulerAngles = RightRot;
                }
                else
                {
                    
                    transform.eulerAngles = LeftRot;
                }
            }
            isRunning = true;
            Running();
        }
        else
        {
            isRunning = false;
            animSpeed = 0.0f;
        }
    }

    void Running()
    {
        if (!ad.isPlaying)
            ad.Play();
        animSpeed = maxAnimSpeed;
        rb.MovePosition(transform.position + transform.forward * (speed * Time.fixedDeltaTime));
        //transform.Translate(0.0f, 0.0f, speed * Time.deltaTime);
    }

    void ReachDestination()
    {
        gameStart = false;
        StartI = NextI;
        StartJ = NextJ;
        /*
        pathFinding.StartI = StartI;
        pathFinding.StartJ = StartJ;
        */
        pathHighlight.DrawPath();
        it++;
        ad.Stop();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == nextDotName)
        {
            isRunning = false;
            ReachDestination();
        }
    }

}