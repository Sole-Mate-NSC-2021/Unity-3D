using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]

public class PlayerControl : MonoBehaviour
{
    [SerializeField] private GameObject clickMarkerPrefab;
    [SerializeField] private Transform visualObjectsParent;
    private LineRenderer myLineRenderer;

    GenGrid grid;
    PathHighlight pathHighlight;
    List<GenNode> lis;

    public float speed = 0.0f, smooth = 1.0f;
    public bool isRunning = false;
    public GameObject dotPointed, dotHovered;
    Vector3 StartPos, targetPos, hoverPos, startLinePos, endLinePos;
    Quaternion targetRot;
    public float animSpeed = 1.0f;

    public int TargetI, TargetJ, StartI, StartJ, NextI, NextJ, it;

    GenNode targetNode;

    AudioSource ad;

    void Start()
    {
        StartI = -1;
        StartJ = -1;

        grid = GetComponent<GenGrid>();
        pathHighlight = GetComponent<PathHighlight>();
        ad = GetComponent<AudioSource>();
        ad.Stop();
    }
    void Update()
    {
        /// Kang Update
        /// 
        lis = grid.FinalPath;

        TargetI = pathHighlight.TargetI;
        TargetJ = pathHighlight.TargetJ;

        if ((StartI != TargetI) || (StartJ != TargetJ))
        {

            NextI = lis[it].gridI;
            NextJ = lis[it].gridJ;

            targetNode = grid.AccessNode(NextI, NextJ);
            Debug.Log(targetNode.gridI + targetNode.gridJ);
            targetPos = targetNode.Position;
            targetRot = Quaternion.LookRotation(targetPos - transform.position);
            transform.rotation = targetRot;
            if (Mathf.Abs(targetPos.x - transform.position.x) > 0.1f)
            {
                isRunning = true;
                Running();
            }
            else
            {
                ReachDestination();
            }
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
        animSpeed = 1.0f;
        transform.Translate(0.0f, 0.0f, speed * Time.deltaTime);
    }

    void ReachDestination()
    {
        StartI = NextI;
        StartJ = NextJ;
        pathHighlight.DrawPath();
        it++;
        ad.Stop();
    }
}