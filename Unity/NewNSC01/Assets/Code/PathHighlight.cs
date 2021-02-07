using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathHighlight : MonoBehaviour
{
    [SerializeField] private GameObject clickMarkerPrefab;
    [SerializeField] private Transform visualObjectsParent;

    public LineRenderer myLineRenderer;

    public GameObject dotTargeted, dotHovered;

    public int HoverI, HoverJ, TargetI, TargetJ, StartI = 0, StartJ = -1;

    public int BeginEndI, BeginEndJ;

    public bool isPointing;

    public Vector3 targetPos, hoverPos, startLinePos, endLinePos;

    PlayerControl playerControl;

    GenGrid grid;

    PathFinding pathFinding;

    public List<GenNode> lis;

    // Start is called before the first frame update
    void Start()
    {
        TargetI = BeginEndI;
        TargetJ = BeginEndJ;

        grid = GetComponent<GenGrid>();
        playerControl = GetComponent<PlayerControl>();
        pathFinding = GetComponent<PathFinding>();

        myLineRenderer = GetComponent<LineRenderer>();
        myLineRenderer.startWidth = 0.3f;
        myLineRenderer.endWidth = 0.3f;
        myLineRenderer.positionCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        StartI = playerControl.StartI;
        StartJ = playerControl.StartJ;
        if (Input.GetMouseButtonDown(0))
            SelectDestination();
        else if (!playerControl.isOnWay)
        {
            HoverPoint();
        }
    }

    void HoverPoint()
    {
        Ray ray;
        RaycastHit hit;

        isPointing = false;

        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.tag == "Dot")
            {
                isPointing = true;
                dotHovered = hit.collider.gameObject;
                hoverPos = dotHovered.transform.position;

                clickMarkerPrefab.transform.SetParent(visualObjectsParent);
                clickMarkerPrefab.SetActive(true);
                clickMarkerPrefab.transform.position = hoverPos;

                HoverI = dotHovered.name[3] - '0';
                HoverJ = dotHovered.name[4] - '0';

                pathFinding.TargetI = HoverI;
                pathFinding.TargetJ = HoverJ;

                pathFinding.StartI = StartI;
                pathFinding.StartJ = StartJ;

                lis = grid.FinalPath;

            }
        }
        HoverPath();
    }
    void SelectDestination()
    {
        Ray ray;
        RaycastHit hit;

        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.tag == "Dot")
            {
                dotTargeted = hit.collider.gameObject;
                targetPos = dotTargeted.transform.position;

                clickMarkerPrefab.transform.SetParent(visualObjectsParent);
                clickMarkerPrefab.SetActive(true);
                clickMarkerPrefab.transform.position = targetPos;

                TargetI = dotTargeted.name[3] - '0';
                TargetJ = dotTargeted.name[4] - '0';

                pathFinding.TargetI = TargetI;
                pathFinding.TargetJ = TargetJ;

                pathFinding.StartI = StartI;
                pathFinding.StartJ = StartJ;

                playerControl.it = 1;

                //Debug.Log("Check " + " " + Start + " " + StartJ + " " + TargetI + " " + TargetJ);
                    
                lis = grid.FinalPath;

            }
        }
    }

    void HoverPath()
    {
        if (lis == null)
        {
            return;
        }
        if (lis.Count < 2)
        {
            return;
        }

        int cnt = 0;

        myLineRenderer.positionCount = (lis.Count - 1) * 2;

        if (!isPointing)
        {
            myLineRenderer.positionCount = 0;
            return;
        }

        if(StartI == HoverI && StartJ == HoverJ)
        {
            myLineRenderer.positionCount = 0;
            return;
        }

        for (int i = 0; i < lis.Count - 1; i++)
        {
            startLinePos = lis[i].Position;
            endLinePos = lis[i + 1].Position;
            myLineRenderer.SetPosition(cnt++, startLinePos);
            myLineRenderer.SetPosition(cnt++, endLinePos);
        }
    }
    public void DrawPath()
    {
        if (lis == null)
        {
            return;
        }
        if (lis.Count < 2)
        {
            return;
        }

        int it = playerControl.it;

        int cnt = 0;

        myLineRenderer.positionCount = Mathf.Max(0, (lis.Count - it - 1) * 2);
            
        for (int i = it; i < lis.Count - 1; i++)
        {
            startLinePos = lis[i].Position;
            endLinePos = lis[i + 1].Position;
            myLineRenderer.SetPosition(cnt++, startLinePos);
            myLineRenderer.SetPosition(cnt++, endLinePos);
        }
    }
}
