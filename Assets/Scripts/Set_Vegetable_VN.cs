using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Set_Vegetable_VN : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "maskVN"){
            GameFlowManager.GFM.Vegetable_VN = true;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.name == "maskVN"){
            GameFlowManager.GFM.Vegetable_VN = false;
        }
    }
}
