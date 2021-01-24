using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] private GameObject clickMarkerPrefab;

    public float speed = 0.0f, smooth = 1.0f;
    GameObject dotPointed;
    Vector3 targetPos;
    Quaternion targetRot;
    public float animSpeed = 1.0f;

    AudioSource ad;
    void Start()
    {
        dotPointed = GameObject.Find("Dot00");
        targetPos = dotPointed.transform.position;

        ad = GetComponent<AudioSource>();
        ad.Stop();
    }
    void Update()
    {
        /// Kang Update
        if (Input.GetMouseButtonDown(0))
        {
            SelectDestination();
        }
        if (dotPointed != null)
        {
            targetRot = Quaternion.LookRotation(targetPos - transform.position);
            transform.rotation = targetRot;
            if (Mathf.Abs(targetPos.x - transform.position.x) > 0.1f)
            {
                Running();
            }
            else
            {
                ReachDestination();
            }
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
                targetPos = dotPointed.transform.position;
                //Debug.Log(targetPos.ToString("F4"));
                clickMarkerPrefab.SetActive(true);
            }
        }
    }
    void Running()
    {
        if (!ad.isPlaying)
            ad.Play();
        animSpeed = 1.0f;
        Debug.Log(animSpeed);
        transform.Translate(0.0f, 0.0f, speed * Time.deltaTime);
    }

    void ReachDestination()
    {
        ad.Stop();
        animSpeed = 0.0f;
        dotPointed = null;
    }
}