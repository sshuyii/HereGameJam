using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowTrigger : MonoBehaviour
{
    private Transform arrow;
    private Rigidbody rb_arrow;

    void Start()
    {
        arrow = transform.parent;
        rb_arrow = arrow.GetComponent<Rigidbody>();
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "appleRA"){
            rb_arrow.isKinematic = true;
            //Debug.Log(arrow.parent);
            arrow.parent = other.gameObject.transform;
            //Debug.Log(arrow.parent);
        }
    }
}
