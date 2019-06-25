using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.Playables;

public class GameFlowManager : MonoBehaviour
{
    public static GameFlowManager GFM;
    // Start is called before the first frame update
    public GameObject dragManager;
    public GameObject[] SceneRoots;
    public FollowMask[] women;
    public Animator[] womenAni;
    public Animator Ani_appleRA;
    public Animator Ani_appleA;

    public PlayableDirector now_tl;
    public GameObject objE;
    public GameObject objS_prefab;
    public GameObject arrowRA_prefab;
    public GameObject appleRA_prefab;
    
    GameObject objS;
    GameObject arrowRA;

    bool objS_S = false;
    bool cp1S_S = false;
    bool cp2S_objS = false;
    bool hasObjS = false;
    [SerializeField]
    bool cp1RA_RA = false;
    [SerializeField]
    bool hasArrow = false;
    [SerializeField]
    bool canGenerateArrow = true;
    [SerializeField]
    bool Arrow_RA = false;
    bool Apple_Arrow = false;
    bool Apple_RA = false;
    bool RA_A = false;
    bool FruitPlate_A = false;
    bool Chair_A = false;
    bool Foot_A = false;
    bool Shoe_N = false;
    bool Bag_N = false;
    bool Bag_VN = false;
    [SerializeField]
    private int curStage = 0;

    void Start()
    {
        if(!GFM){
            GFM = this;
        }
        if(curStage == 0){
            for(int i = 1 ; i<SceneRoots.Length; i++){
                SceneRoots[i].SetActive(false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(curStage == 0){
            if(!hasObjS){
                if(cp1S_S){
                    objS = Instantiate(objS_prefab,SceneRoots[0].transform);
                    hasObjS = true;
                }
            }else{
                if(!objS_S){
                    objS.SetActive(false);
                    Destroy(objS);
                    hasObjS = false;
                }else if(cp2S_objS){
                    if(Mathf.Abs(objS.transform.position.y - 2.58f) < 1e-1){
                    //if(objS.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f){
                        objS.GetComponent<SpriteRenderer>().enabled = false;
                        Destroy(objS);
                        objE.GetComponent<SpriteRenderer>().enabled = true;
                        SceneRoots[0].GetComponent<EaseInOutController>().fadeOut(2.0f,1.0f);
                        SceneRoots[1].SetActive(true);
                        StartCoroutine(DelayToInvoke(delegate() {
                            now_tl.Play();
                            StartCoroutine(DelayToInvoke(delegate() {
                                SceneRoots[2].SetActive(true);
                            },(float)now_tl.playableAsset.duration));
                        },2.5f));
                        curStage = 1;
                    }
                }
            }
        }
        if(curStage == 1){
            if(!hasArrow){
                if(cp1RA_RA && canGenerateArrow){
                    arrowRA = Instantiate(arrowRA_prefab,SceneRoots[2].transform);
                    hasArrow = true;
                    canGenerateArrow = false;
                }
            }else{
                if(!Arrow_RA){
                    //arrowRA.SetActive(false);
                    hasArrow = false;
                    Destroy(arrowRA);
                    StartCoroutine(DelayToInvoke(delegate(){
                        canGenerateArrow = true;
                    },2.0f));
                }else if(Apple_Arrow){
                    StartCoroutine(DelayToInvoke(delegate() {
                        Ani_appleRA.SetTrigger("falling");
                        SceneRoots[3].SetActive(true);
                    },0.2f));
                    curStage = 2;
                }
            }
        }else if(curStage == 2){
            if(Apple_RA && RA_A && FruitPlate_A){
                Ani_appleRA.SetTrigger("traveling");//AppleRA
                Ani_appleA.SetBool("falling",true);//AppleA
                StartCoroutine(DelayToInvoke(delegate(){
                    Ani_appleA.SetTrigger("next");//AppleA
                    StartCoroutine(DelayToInvoke(delegate(){
                        womenAni[0].SetTrigger("next");
                        Ani_appleA.SetTrigger("next");//AppleA
                    },3.0f));
                },1.5f));

                curStage = 3;
            }
        }else if(curStage == 3){
            if(women[0].isAtEnd()){
                womenAni[0].SetTrigger("next");
            }
            if(Foot_A && Chair_A && women[0].isAtEnd()){
                StartCoroutine(DelayToInvoke(delegate(){
                    SceneRoots[4].SetActive(true);
                    StartCoroutine(DelayToInvoke(delegate(){
                        women[1].enabled = true;
                    },2.0f));
                },1.0f));
                //StartCoroutine(WaitAndActivateRoots3());
                curStage = 4;
            }
        }else if(curStage == 4){
            if(Bag_N && women[1].isAtEnd()){
                StartCoroutine(DelayToInvoke(delegate(){
                    womenAni[1].SetTrigger("next");
                    StartCoroutine(DelayToInvoke(delegate(){
                        SceneRoots[5].SetActive(true);
                    },2.0f));
                },1.0f));
                curStage = 5;
            }
        }else if(curStage == 5){
            if(Bag_VN && women[2].isAtEnd()){
                curStage = 6;
            }
        }
        
    }

    void DragManager_SetActive(bool isActive){
        dragManager.SetActive(isActive);
    }

    public bool duringStage(int i){
        return (curStage == i);
    }

    bool ItemInArray(String[] array, string item){
        return Array.IndexOf(array, item)> -1;
    }
    public void SetBool(string A, string B, bool state){
        string[] array = new string[]{A,B};
        if(ItemInArray(array, "objS(Clone)") && ItemInArray(array, "maskS")){
            objS_S = state;
        }else if(ItemInArray(array, "cp1S") && ItemInArray(array, "maskS")){
            cp1S_S = state;
        }else if(ItemInArray(array, "cp2S") && ItemInArray(array, "objS(Clone)")){
            cp2S_objS = state;
        }else if(ItemInArray(array, "cp1RA") && ItemInArray(array, "maskRA")){
            cp1RA_RA = state;
        }else if(ItemInArray(array, "arrow(Clone)") && ItemInArray(array, "maskRA")){
            Arrow_RA = state;
        }else if(ItemInArray(array, "appleRA") && ItemInArray(array, "arrowTrigger")){
            Apple_Arrow = state;
        }else if(ItemInArray(array, "appleRA") && ItemInArray(array, "maskRA")){
            Apple_RA = state;
        }else if(ItemInArray(array, "maskRA") && ItemInArray(array, "maskA")){
            RA_A = state;
        }else if(ItemInArray(array, "godHand") && ItemInArray(array, "maskA")){
            FruitPlate_A = state;
        }else if(ItemInArray(array, "chair") && ItemInArray(array, "maskA")){
            Chair_A = state;
        }else if(ItemInArray(array, "shoeAncient") && ItemInArray(array, "maskA")){
            Foot_A = state;
        }else if(ItemInArray(array, "ShoeDoctor") && ItemInArray(array, "maskN")){
            Shoe_N = state;
        }else if(ItemInArray(array, "BagTrigger") && ItemInArray(array, "maskN")){
            Bag_N = state;
        }else if(ItemInArray(array, "basket") && ItemInArray(array, "maskVN")){
            Bag_VN = state;
        }
    }

    private IEnumerator DelayToInvoke(Action action, float delaySeconds)  
    {  
        yield return new WaitForSeconds(delaySeconds);  
        action();  
    }
    private IEnumerator WaitTillAndDo(Action action,Func<bool> condition)  
    {  
        yield return new WaitUntil(condition);
        action();  
    }
}
