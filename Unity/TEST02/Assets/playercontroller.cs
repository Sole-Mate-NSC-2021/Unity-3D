using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class playercontroller : MonoBehaviour
{
    // Start is called before the first frame update
    public Camera cam;
    public NavMeshAgent Agent;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                Agent.SetDestination(hit.point);
            }
        }
    }
}