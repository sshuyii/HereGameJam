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
    public DragMask dragMask;
    public GameObject[] SceneRoots;
    public Camera camAncient;
    public Camera camNear;
    public Camera camVeryNear;
    public FollowMask[] women;
    public Animator modernWoman;
    public Animator[] womenAni;
    public Animator Ani_appleA;
    public Animator Ani_shoeA;
    public Animator Ani_shoeN;

    public PlayableDirector now_tl;
    public GameObject objE;
    public GameObject objS_prefab;
    public GameObject arrowRA_prefab;
    public GameObject appleRA_prefab;
    
    GameObject objS;
    GameObject arrowRA;
    GameObject appleRA;

    bool objS_S = false;
    bool cp1S_S = false;
    bool cp2S_objS = false;
    bool hasObjS = false;
    bool cp1RA_RA = false;
    bool hasArrow = false;
    bool canGenerateArrow = true;
    bool Arrow_RA = false;
    bool Apple_Arrow = false;
    bool firstIntoAncient = true;
    bool ancientWomenRight = false;
    bool Apple_RA = false;
    bool RA_A = false;
    bool FruitPlate_A = false;
    bool travelling_appleRA = false;
    bool Chair_A = false;
    bool Foot_A = false;
    bool Shoe_N = false;
    bool docWalkingRight = false;
    bool Bag_N = false;
    [SerializeField]
    bool phone_VN = false;
    bool canCall = false;
    bool showingAll = false;
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
                        SceneRoots[0].GetComponent<EaseInOutController>().fadeOut(1.0f,0.0f);
                        StartCoroutine(DelayToInvoke(delegate() {
                            SceneRoots[1].SetActive(true);
                            StartCoroutine(DelayToInvoke(delegate() {
                                now_tl.Play();
                                StartCoroutine(DelayToInvoke(delegate() {
                                    SceneRoots[2].SetActive(true);
                                    appleRA = Instantiate(appleRA_prefab,SceneRoots[2].transform);
                                    appleRA.name = "appleRA";
                                },(float)now_tl.playableAsset.duration));
                            },2.5f));
                        },0.3f));
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
                        appleRA.GetComponent<Animator>().SetTrigger("falling");
                        SceneRoots[3].SetActive(true);//Show Ancient Scene
                        if(firstIntoAncient){
                            StartCoroutine(DelayToInvoke(delegate() {
                                women[0].SetWalking(true);
                                womenAni[0].SetTrigger("next");
                            },4.0f));//walking
                            firstIntoAncient = false;
                        }
                    },0.2f));
                    curStage = 2;
                }
            }
            if(women[0].isAtEnd()){
                ancientWomenRight = true;
                womenAni[0].SetTrigger("next");//waiting
                women[0].ReSet(0.01f,3,"maskA_edgeR","AncientLeftEnd");
            }
        }else if(curStage == 2){
            if(!Apple_RA){
                Destroy(appleRA);
                travelling_appleRA = false;
                hasArrow = false;
                canGenerateArrow = true;
                Apple_Arrow = false;
                appleRA = Instantiate(appleRA_prefab,SceneRoots[2].transform);
                appleRA.name = "appleRA";
                curStage = 1;
            }else if(Apple_RA && RA_A && FruitPlate_A && ancientWomenRight){
                if(!travelling_appleRA){
                    //Debug.Break();
                    appleRA.layer = LayerMask.NameToLayer("Default");
                    SpriteRenderer sr_appleRA = appleRA.GetComponent<SpriteRenderer>();
                    sr_appleRA.sortingLayerName = "Front";
                    sr_appleRA.sortingOrder = 1000;
                    sr_appleRA.maskInteraction = SpriteMaskInteraction.None;
                    travelling_appleRA = true;
                }
                else if(appleRA.transform.position.y < 0.83f){
                    //Debug.Log("here");
                    Destroy(appleRA);
                    Ani_appleA.SetTrigger("next");//AppleA appears
                    StartCoroutine(DelayToInvoke(delegate(){
                        Ani_appleA.SetTrigger("next");//AppleA jumping
                        StartCoroutine(DelayToInvoke(delegate(){
                            SceneRoots[2].GetComponent<EaseInOutController>().fadeOut(1.5f);
                            StartCoroutine(DelayToInvoke(delegate(){
                                SceneRoots[4].SetActive(true);
                                StartCoroutine(DelayToInvoke(delegate(){
                                    StartCoroutine(DelayToInvoke(delegate(){
                                        Ani_appleA.SetTrigger("next");//AppleA disappearing
                                        womenAni[0].SetTrigger("next");//walking
                                        women[0].gameObject.GetComponent<SpriteRenderer>().flipX = true;
                                        women[0].SetWalking(true);
                                        StartCoroutine(DelayToInvoke(delegate(){
                                            womenAni[1].SetBool("walking",true);//walking
                                            women[1].SetWalking(true);
                                        },2.0f));
                                    },1.0f));
                                },1.5f));
                            },0.5f));
                        },1.5f));
                    },1.0f));
                    curStage = 3;
                }
            }
        }else if(curStage == 3){
            if(women[0].isAtEnd()){
                women[0].gameObject.GetComponent<SpriteRenderer>().flipX = false;
                womenAni[0].SetTrigger("sitting");//sitting
            }
            if(women[1].isAtEnd()){
                Vector3 scale = women[1].gameObject.transform.localScale;
                scale.x = 1.0f;
                women[1].gameObject.transform.localScale = scale;
                womenAni[1].SetTrigger("idle");
            }
            if(Foot_A && Chair_A && women[0].isAtEnd() && women[1].isAtEnd() && !dragMask.Dragging()){
                camAncient.depth = 2;
                camNear.depth = 1;
                StartCoroutine(DelayToInvoke(delegate(){
                    Ani_shoeA.SetTrigger("falling");
                    Ani_shoeN.SetTrigger("falling");
                    SceneRoots[3].GetComponent<EaseInOutController>().fadeOut(2.0f);
                    StartCoroutine(DelayToInvoke(delegate(){
                        StartCoroutine(WaitTillAndDo(delegate(){
                            camAncient.depth = 1;
                            camNear.depth = 2;
                        },() => !dragMask.Dragging()));
                        SceneRoots[5].SetActive(true);
                        StartCoroutine(DelayToInvoke(delegate(){
                            womenAni[1].SetBool("walking",true);
                            women[1].ReSet(-0.015f,4,"maskN_edgeL","DocRightEnd");
                            women[1].SetWalking(true);
                            docWalkingRight = true;
                            StartCoroutine(DelayToInvoke(delegate(){
                                womenAni[2].SetTrigger("walking");
                                women[2].SetWalking(true);
                            },2.0f));
                        },2.0f));
                    },1.0f));
                },1.0f));
                curStage = 4;
            }
        }else if(curStage == 4){
            if(women[2].isAtEnd()){
                womenAni[2].SetTrigger("waiting");
            }
            if(Bag_N && women[1].isAtEnd() && docWalkingRight && women[2].isAtEnd() && !dragMask.Dragging()){
                camNear.depth = 3;
                camVeryNear.depth = 2;
                SceneRoots[4].GetComponent<EaseInOutController>().fadeOut(3.0f);
                Debug.Log("here");
                womenAni[1].SetTrigger("next");//placing bags
                //TODO: basket showing
                StartCoroutine(DelayToInvoke(delegate(){
                    womenAni[2].SetTrigger("getting");
                    StartCoroutine(WaitTillAndDo(delegate(){
                        camNear.depth = 2;
                        camVeryNear.depth = 3;
                        women[2].ReSet(0.01f,5,"maskVN_edgeR","DamaLeftEnd");
                        women[2].SetWalking(true);
                        StartCoroutine(WaitTillAndDo(delegate(){
                            StartCoroutine(DelayToInvoke(delegate(){
                                womenAni[2].SetTrigger("walkingBack");
                            },0.5f));
                        },() => dragMask.Dragging()));
                        canCall = true;
                    },() => !dragMask.Dragging()));
                },1.0f));
                curStage = 5;
            }
        }else if(curStage == 5){
            if(canCall){
                if(women[2].isAtEnd() && phone_VN && !dragMask.Dragging()){
                    StartCoroutine(DelayToInvoke(delegate(){
                        womenAni[2].SetTrigger("calling");
                        StartCoroutine(DelayToInvoke(delegate(){
                            modernWoman.SetTrigger("calling");
                            showingAll = true;
                        },2.0f));
                    },0.5f));
                }
            }
            if(showingAll){
                StartCoroutine(DelayToInvoke(delegate(){
                    SceneRoots[2].SetActive(true);
                    SceneRoots[3].SetActive(true);
                    SceneRoots[4].SetActive(true);
                    StartCoroutine(WaitTillAndDo(delegate(){
                        curStage = 6;
                    },() => !dragMask.Dragging()));
                },1.0f));
                showingAll = false;
            }
        }
    }

    // void DragManager_SetActive(bool isActive){
    //     dragManager.SetActive(isActive);
    // }

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
        }else if(ItemInArray(array, "phoneVN") && ItemInArray(array, "maskVN")){
            phone_VN = state;
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
