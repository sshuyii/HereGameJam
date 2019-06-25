using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnCollision : MonoBehaviour
{
    public GameObject gameObj;
    public Transform parent;
    public float spawnInterval = 1.0f;

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == gameObj.name || other.gameObject.name == (gameObj.name+"(Clone)")){
            Destroy(other.gameObject);
            parent.GetComponent<EaseInOutController>().updateSRsinChildren();
            StartCoroutine(spawnNewObjAfter(spawnInterval));
        }
    }
    IEnumerator spawnNewObjAfter(float delay){
        yield return new WaitForSeconds(delay);
        Instantiate(gameObj,parent);
        parent.GetComponent<EaseInOutController>().updateSRsinChildren();
    }
}
