
using System;
using UnityEngine;
using UnityEngine.AI;

public class playercon : MonoBehaviour
{

    public Animator animator;
    public Camera cam;
    public NavMeshAgent agent;
    float InputX;
    float InputY;
    float speed;
    float lastPosition;
   
    // Update is called once per frame
    void Update()
    {
        if (agent.remainingDistance>0.25f)
            InputX = -1f;
        else
            InputX = 0f;
        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

           if( Physics.Raycast(ray, out hit))
            {
                agent.SetDestination(hit.point);
            }

        }
        animator.SetFloat("InputX", InputX);
    }
}
