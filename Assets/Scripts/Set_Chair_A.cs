using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Set_Chair_A : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "maskA"){
            GameFlowManager.GFM.Chair_A = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.name == "maskA"){
            GameFlowManager.GFM.Chair_A = false;
        }
    }
}
