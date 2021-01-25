using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]

public class PlayerControl : MonoBehaviour
{
    [SerializeField] private GameObject clickMarkerPrefab;
    [SerializeField] private Transform visualObjectsParent;
    private LineRenderer myLineRenderer;

    public float speed = 0.0f, smooth = 1.0f;
    public bool isRunning = false;
    public GameObject dotPointed, dotHovered;
    Vector3 targetPos, hoverPos, startLinePos, endLinePos;
    Quaternion targetRot;
    public float animSpeed = 1.0f;

    AudioSource ad;

    GenGrid grid;

    List<GenNode> lis;

    void Start()
    {

        grid = GetComponent<GenGrid>();

        dotPointed = GameObject.Find("Dot00");

        if (this.name == "Character2")
        {
            dotPointed = GameObject.Find("Dot14");
        }
        targetPos = dotPointed.transform.position;

        ad = GetComponent<AudioSource>();
        ad.Stop();

        myLineRenderer = GetComponent<LineRenderer>();
        myLineRenderer.startWidth = 0.3f;
        myLineRenderer.endWidth = 0.3f;
        myLineRenderer.positionCount = 0;
        //myLineRenderer.alignment = LineAlignment.TransformZ;
    }
    void Update()
    {
        /// Kang Update

        lis = grid.FinalPath;

        SelectDestination();
        if (dotPointed != null)
        {
            targetRot = Quaternion.LookRotation(targetPos - transform.position);
            transform.rotation = targetRot;
            if (Mathf.Abs(targetPos.x - transform.position.x) > 0.1f)
            {
                //Debug.Log(Mathf.Abs(targetPos.x - transform.position.x));
                isRunning = true;
                Running();
            }
            else
            {
                isRunning = false;
                ReachDestination();
            }
        }
        if(!isRunning)
        {
            myLineRenderer.positionCount = 0;
            DrawPath();
        }
    }

    /// Functions
    void SelectDestination()
    {
        Ray ray;
        RaycastHit hit;

        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.tag == "Dot")
            {
                dotHovered = hit.collider.gameObject;
                if(Input.GetMouseButtonDown(0))
                dotPointed = hit.collider.gameObject;
                if (!isRunning)
                {
                    myLineRenderer.positionCount = 2;
                    hoverPos = dotPointed.transform.position;
                    DrawPath();
                }
                if(Input.GetMouseButtonDown(0))
                    targetPos = dotPointed.transform.position;
                //Debug.Log(targetPos.ToString("F4"));
                clickMarkerPrefab.transform.SetParent(visualObjectsParent);
                clickMarkerPrefab.SetActive(true);
                clickMarkerPrefab.transform.position = targetPos;
            }
        }
    }
    void Running()
    {
        if (!ad.isPlaying)
            ad.Play();
        animSpeed = 1.0f;
        transform.Translate(0.0f, 0.0f, speed * Time.deltaTime);
        DrawPath();
    }

    void ReachDestination()
    {
        clickMarkerPrefab.transform.SetParent(transform);
        clickMarkerPrefab.SetActive(false);
        ad.Stop();
        animSpeed = 0.0f;
        //dotPointed = null;
    }

    void DrawPath()
    {
        /*
        if (!isRunning)
        {
            endLinePos = hoverPos;
        }
        else
        {
            endLinePos = targetPos;
        }
        */
        if(myLineRenderer.positionCount < 2)
        {

            if(lis.Count < 1)
            {
                Debug.Log(lis.Count);
                return;
            }

            int it = 0;

            myLineRenderer.positionCount = (lis.Count - 1) * 2 ;
            
            for(int i = 0; i < lis.Count - 1; i++)
            {
                startLinePos = lis[i].Position;
                endLinePos = lis[i + 1].Position;
                myLineRenderer.SetPosition(it++, startLinePos);
                myLineRenderer.SetPosition(it++, endLinePos);
            }

            return;
        }
        
        /*
        myLineRenderer.SetPosition(0, transform.position);
        myLineRenderer.SetPosition(1, endLinePos);
        */
    }
}