using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Move : MonoBehaviour
{
    public float speed = 0.0f, smooth = 1.0f;
    public GameObject dotPointed;
    public Vector3 targetPos;
    public Quaternion targetRot;
    public float animSpeed = 0.0f;
    AudioSource ad;
    void Start() {
        dotPointed = GameObject.Find("Dot00");
        targetPos = dotPointed.transform.position;
        ad = GetComponent<AudioSource>();
        ad.Stop();
    }
    void Update()
    {

        /// Kang Update
        if (Input.GetMouseButton(0))
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
                }
            }
        }
        if (dotPointed != null)
        {
            //Debug.Log(dotPointed.name);
            targetRot = Quaternion.LookRotation(targetPos - transform.position);

            transform.rotation = targetRot;

            if (Mathf.Abs(targetPos.x - transform.position.x) > 0.1f)
            {
                if (!ad.isPlaying)
                    ad.Play();
                animSpeed = 1.0f;
                transform.Translate(0, 0, speed * Time.deltaTime);
            }
            else
            {
                ad.Stop();
                animSpeed = 0.0f;
                dotPointed = null;
            }
        }
        ///
    }
}
/*

        a = Input.GetAxis("Horizontal");
        ho = a*Time.deltaTime*speed;
        if(Mathf.Abs(a)>0) {
            if(!ad.isPlaying) ad.Play();
        }
        else {
            ad.Stop();
        }
        if( dir == 0 && a<0) {
            transform.rotation *= Quaternion.Euler(0, 180f, 0);
            dir=1;
        }
        else if(dir == 1 && a>0) {
            transform.rotation *= Quaternion.Euler(0, -180f, 0); 
            dir=0;
        }
        transform.Translate(0, 0, Mathf.Abs(ho) );
 */