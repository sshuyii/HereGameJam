using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMask : MonoBehaviour
{
    public float speed = 0.02f;
    public int Stage = 2;
    public string movingTriggerName = "maskA_edge";
    public string endingTriggerName = "PathLeftEnd";
    [SerializeField]
    private bool arrivedAtDes = false;
    private bool isWalking = false;
    //private Rigidbody _rb;

    // void Awake(){
    //     _rb = gameObject.GetComponent<Rigidbody>();
    // }
    void Start(){
        isWalking = false;
    }
    void OnTriggerStay(Collider other)
    {
        if(GameFlowManager.GFM.duringStage(Stage)){
            if(isWalking && !arrivedAtDes){
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
            arrivedAtDes = true;
        }
    }

    public bool isAtEnd(){
        return arrivedAtDes;
    }

    public void SetWalking(bool walking){
        isWalking = walking;
    }

    public void ReSet(float _speed, int _Stage, string _movingTrigger, string _endingTrigger){
        speed = _speed;
        Stage = _Stage;
        movingTriggerName = _movingTrigger;
        endingTriggerName = _endingTrigger;
        arrivedAtDes = false;
        isWalking = false;
    }
}
