using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Set_FruitPlate_A : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "maskA"){
            GameFlowManager.GFM.FruitPlate_A = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.name == "maskA"){
            GameFlowManager.GFM.FruitPlate_A = false;
        }
    }
}
