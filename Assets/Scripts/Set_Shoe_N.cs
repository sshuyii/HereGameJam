using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Set_Shoe_N : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "maskN"){
            GameFlowManager.GFM.Shoe_N = true;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.name == "maskN"){
            GameFlowManager.GFM.Shoe_N = false;
        }
    }
}
