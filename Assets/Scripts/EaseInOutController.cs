using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EaseInOutController : MonoBehaviour
{
    public float introDelay = 2.0f;
    public float introTrans = 1.0f;
    [SerializeField]
    private SpriteRenderer[] srsInChildren;
    private bool animating = false;
    private float from_alpha = 1.0f;
    private float transitionSpeed = 1.0f;
    private float to_alpha = 0.0f;
    private float cur_alpha = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        srsInChildren = transform.GetComponentsInChildren<SpriteRenderer>();
    }

    public void updateSRsinChildren(){
        srsInChildren = transform.GetComponentsInChildren<SpriteRenderer>();
    }

    void OnEnable()
    {
        fadeIn(introTrans,introDelay);
    }

    void OnDisable()
    {
        cur_alpha = 0.0f;
        updateAlphaInChildren();
    }

    // Update is called once per frame
    void Update()
    {
        if(animating){
            if(Mathf.Abs(cur_alpha-to_alpha)<1e-2){
                cur_alpha = to_alpha;
                animating = false;
                if(Mathf.Abs(to_alpha)<1e-2){
                    gameObject.SetActive(false);
                }
            }else{
                cur_alpha += Mathf.Sign(to_alpha-cur_alpha) * Time.deltaTime * transitionSpeed;
            }
            updateAlphaInChildren();
        }
    }

    public void fadeIn(){
        transitionAni(1.0f);
    }

    public void fadeOut(){
        transitionAni(0.0f);
    }

    public void fadeIn(float transitionTime){
        transitionAni(1.0f,transitionTime);
    }

    public void fadeOut(float transitionTime){
        transitionAni(0.0f,transitionTime);
    }

    public void fadeIn(float transitionTime, float delayTime){
        transitionAniWithDelay(1.0f,delayTime,transitionTime);
    }

    public void fadeOut(float transitionTime, float delayTime){
        transitionAniWithDelay(0.0f,delayTime,transitionTime);
    }
    public void transitionAni(float des_alpha, float transitionTime){
        transitionAniWithDelay(des_alpha,0.0f,transitionTime);
    }

    public void transitionAni(float des_alpha){
        transitionAniWithDelay(des_alpha,0.0f);
    }

    public void transitionAniWithDelay(float des_alpha, float delayTime, float transitionTime){
        updateSRsinChildren();
        from_alpha = cur_alpha;
        to_alpha = des_alpha;
        transitionSpeed = Mathf.Abs(des_alpha-cur_alpha)/transitionTime;
        if(delayTime<1e-4){
            animating = true;
        }else{
            StartCoroutine(delayAni(delayTime));
        }
    }
    public void transitionAniWithDelay(float des_alpha, float delayTime){
        updateSRsinChildren();
        from_alpha = cur_alpha;
        to_alpha = des_alpha;
        transitionSpeed = 1.0f;
        if(delayTime<1e-4){
            animating = true;
        }else{
            StartCoroutine(delayAni(delayTime));
        }        
    }

    private IEnumerator delayAni(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        animating = true;
    }

    void updateAlphaInChildren(){
        foreach(SpriteRenderer sr in srsInChildren){
            if(sr){
                Color tmp = sr.color;
                tmp.a = cur_alpha;
                sr.color = tmp;
            }
        }
    }
}
