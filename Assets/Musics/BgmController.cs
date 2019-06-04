using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgmController : MonoBehaviour
{
    private float timeSinceLastPlay = 0.0f;
    private AudioSource _as;
    // Start is called before the first frame update
    void Start()
    {
        _as = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceLastPlay += Time.deltaTime;
        if(timeSinceLastPlay > 15.0f){
            int rand = Random.Range(0,100);
            if(rand < 5){
                _as.Play();
                timeSinceLastPlay = 0.0f;
            }
        }
        
    }
}
