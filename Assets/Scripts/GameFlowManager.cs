using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFlowManager : MonoBehaviour
{
    public static GameFlowManager GFM;
    // Start is called before the first frame update
    public GameObject dragManager;

    public GameObject[] SceneRoots;
    public Animator[] MaskAnimators;
    public FollowMask[] women;
    public Animator[] womenAni;
    public Animator[] Animators;
    public Animator[] maskAnis;

    public bool Apple_RA = false;
    public bool RA_A = false;
    public bool FruitPlate_A = false;
    public bool Chair_A = false;
    public bool Foot_A = false;
    public bool Shoe_N = false;
    public bool Bag_N = false;
    public bool Vegetable_VN = false;
    [SerializeField]
    private int curStage = 1;

    void Start()
    {
        if(!GFM){
            GFM = this;
        }
        SceneRoots[2].SetActive(false);
        SceneRoots[3].SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(curStage == 1){
            if(Apple_RA && RA_A && FruitPlate_A){
                Animators[0].SetBool("falling",true);//AppleRA
                Animators[1].SetBool("falling",true);//AppleA
                Animators[2].SetBool("falling",true);//AppleNow
                StartCoroutine(Wait_AppleJump_WomanWalk());
                curStage = 2;
            }
        }else if(curStage == 2){
            if(women[0].isAtEnd()){
                womenAni[0].SetTrigger("next");
            }
            if(Foot_A && Chair_A && women[0].isAtEnd()){
                //Animators[3].SetBool("falling",true);//Shoe_A
                //Animators[4].SetBool("falling",true);//Shoe_N
                StartCoroutine(ActivateDoctor());
                //StartCoroutine(WaitAndActivateRoots3());
                curStage = 3;
            }
        }else if(curStage == 3){
            if(Bag_N && women[1].isAtEnd()){
                StartCoroutine(ActivateDaMa());
                //Animators[5].SetBool("falling",true);//Bag_N
                //Animators[6].SetBool("falling",true);//Bag_VN
                curStage = 4;
            }
        }else if(curStage == 4){
            if(Vegetable_VN && women[2].isAtEnd()){
                foreach(Animator ani in maskAnis){
                    ani.SetBool("highlight",true);
                }
                curStage = 5;
            }
        }
        
    }

    void DragManager_SetActive(bool isActive){
        dragManager.SetActive(isActive);
    }

    public bool duringStage(int i){
        return (curStage == i);
    }

    private IEnumerator Wait_AppleJump_WomanWalk(){
        yield return new WaitForSeconds(1.5f);
        Animators[1].SetTrigger("next");//AppleA
        yield return new WaitForSeconds(3.0f);
        womenAni[0].SetTrigger("next");
        Animators[1].SetTrigger("next");//AppleA
    }

    private IEnumerator ActivateDoctor()
    {
        yield return new WaitForSeconds(1.0f);
        SceneRoots[2].SetActive(true);
        yield return new WaitForSeconds(2.0f);
        women[1].enabled = true;
    }

    private IEnumerator ActivateDaMa()
    {
        yield return new WaitForSeconds(1.0f);
        womenAni[1].SetTrigger("next");
        yield return new WaitForSeconds(2.0f);
        SceneRoots[3].SetActive(true);
    }
}
