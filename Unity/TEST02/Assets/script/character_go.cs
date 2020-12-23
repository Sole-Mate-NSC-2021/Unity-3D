using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class character_go : MonoBehaviour
{
    Camera _cam;
    public LayerMask groundLayer;

    public NavMeshAgent _PlayerAgent;

    void Awake()
    {
        _cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _PlayerAgent.SetDestination(_getpointundercursor());
        }
    }

    private Vector3 _getpointundercursor()
    {
        Vector2 _ScreenPos = Input.mousePosition;
        Vector3 _Worldpos = _cam.ScreenToWorldPoint(_ScreenPos);

        RaycastHit _hitposition;
        Physics.Raycast(_Worldpos, _cam.transform.forward, out _hitposition, 100 ,groundLayer);

        return _hitposition.point;
    }
}
