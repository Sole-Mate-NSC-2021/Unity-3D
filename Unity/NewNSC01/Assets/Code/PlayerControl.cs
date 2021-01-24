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
    GameObject dotPointed;
    Vector3 targetPos, hoverPos, endLinePos;
    Quaternion targetRot;
    public float animSpeed = 1.0f;

    AudioSource ad;
    void Start()
    {
        dotPointed = GameObject.Find("Dot00");
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
        else if(!isRunning)
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
        dotPointed = null;
    }

    void DrawPath()
    {
        if (!isRunning)
        {
            endLinePos = hoverPos;
        }
        else
        {
            endLinePos = targetPos;
        }
        if(myLineRenderer.positionCount < 2)
        {
            return;
        }
        myLineRenderer.SetPosition(0, transform.position);
        myLineRenderer.SetPosition(1, endLinePos);
    }
}