
using System;
using UnityEngine;
using UnityEngine.AI;


public class playercon : MonoBehaviour
{

    public Animator mAnimator;
    public Camera cam;
    public NavMeshAgent mNavMeshAgent;
    float lastPosition;
    // Update is called once per frame

    public bool mRunning = false;
    public bool mReverse = false;

    void Start()
    {
        mAnimator = GetComponent<Animator>();
        mNavMeshAgent = GetComponent<NavMeshAgent>();
        //mNavMeshAgent.updateRotation = false;
    }

    void Update()
    {
        //agent.updateRotation = false;

        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

           if( Physics.Raycast(ray, out hit, 100))
            {
                mNavMeshAgent.SetDestination(hit.point);
            }
        }

        //
        //transform.rotation = Quaternion.LookRotation(mNavMeshAgent.velocity.normalized);
        //

        if (mNavMeshAgent.remainingDistance <= mNavMeshAgent.stoppingDistance)
        {
            mRunning = false;
        }
        else
            mRunning = true;



        if (mRunning == false)
            mNavMeshAgent.updatePosition = false;
        else
            mNavMeshAgent.updatePosition = true;



        if (Input.GetKey(KeyCode.Return))
        {
            mAnimator.SetFloat("Speed", -1f);
            mAnimator.SetBool("running", true);
        }
        else
        {
            mAnimator.SetBool("running", mRunning);
            mAnimator.SetFloat("Speed", 1f);
        }
    }
}
