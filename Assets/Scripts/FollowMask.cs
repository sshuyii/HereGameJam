using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMask : MonoBehaviour
{
    public float speed = 0.05f;
    public int Stage = 2;
    public string movingTriggerName = "maskA_edge";
    public string endingTriggerName = "PathLeftEnd";
    private bool arrivedAtLeft = false;
    private bool isWalking = false;
    //private Rigidbody _rb;

    // void Awake(){
    //     _rb = gameObject.GetComponent<Rigidbody>();
    // }
    void Start(){
        isWalking = true;
    }
    void OnTriggerStay(Collider other)
    {
        if(GameFlowManager.GFM.duringStage(Stage)){
            if(isWalking && !arrivedAtLeft){
                if(other.gameObject.name == movingTriggerName){
                    //_rb.AddForce(Vector3.left*speed);
                    transform.Translate(Vector3.left*speed,transform.parent);
                }
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == endingTriggerName){
            arrivedAtLeft = true;
        }
    }

    public bool isAtEnd(){
        return arrivedAtLeft;
    }
}
