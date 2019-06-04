using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Set_RA_A : MonoBehaviour
{
   void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "maskA"){
            GameFlowManager.GFM.RA_A = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.name == "maskA"){
            GameFlowManager.GFM.RA_A = false;
        }
    }
}
