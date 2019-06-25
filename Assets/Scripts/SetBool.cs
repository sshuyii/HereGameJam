using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetBool : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        GameFlowManager.GFM.SetBool(name, other.gameObject.name, true);
    }

    void OnTriggerStay(Collider other)
    {
        GameFlowManager.GFM.SetBool(name, other.gameObject.name, true);
    }
    
    void OnTriggerExit(Collider other)
    {
        GameFlowManager.GFM.SetBool(name, other.gameObject.name, false);
    }
}
