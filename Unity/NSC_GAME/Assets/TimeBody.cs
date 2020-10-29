using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeBody : MonoBehaviour
{

    public bool isRewinding = false;
    public Animator mAnimator;

    List<PointInTime> pointsInTime;

    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        pointsInTime = new List<PointInTime>();
        rb = GetComponent<Rigidbody>();
        mAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            StartRewind();
        }
        if (Input.GetKeyUp(KeyCode.Return))
            StopRewind();

    }

    void FixedUpdate()
    {
        if (isRewinding)
        {
            Rewind();
        }
        else
            Record();
    }

    void Rewind()
    {

        PointInTime pointInTime = pointsInTime[0];
        if (pointsInTime.Count > 0)
        {
            transform.position = pointInTime.position;
            transform.rotation = pointInTime.rotation;
            pointsInTime.RemoveAt(0);
        }
        else
        {
            StopRewind();
        }
    }

    void Record()
    {
        pointsInTime.Insert(0, new PointInTime(transform.position, transform.rotation));
    }


    // Update is called once per frame


    void StartRewind()
    {
        isRewinding = true;
        rb.isKinematic = true;
    }

    void StopRewind()
    {
        isRewinding = false;
        rb.isKinematic = false;
    }

}
