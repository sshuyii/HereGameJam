using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour
{
    public float speed = 4.0f;
    public float angularSpeed = 0.5f;
    private Rigidbody _rb;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.velocity = new Vector3(10, 1.15f, 0) * speed;
        _rb.angularVelocity = new Vector3(0, 0, -1) * angularSpeed;
    }

    // void OnTriggerEnter(Collider other){
    //     if(other.gameObject.name == "deathZoneRA"){
    //         StartCoroutine(DelayToInvoke(delegate(){

    //         },1.0f));
    //     }
    // }
    // private IEnumerator DelayToInvoke(Action action, float delaySeconds)  
    // {  
    //     yield return new WaitForSeconds(delaySeconds);  
    //     action();  
    // }
}
