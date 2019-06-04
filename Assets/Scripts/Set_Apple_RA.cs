using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Set_Apple_RA : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "maskRA"){
            GameFlowManager.GFM.Apple_RA = true;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.name == "maskRA"){
            GameFlowManager.GFM.Apple_RA = false;
        }
    }
}
