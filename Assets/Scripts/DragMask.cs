using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DragMask: MonoBehaviour{
    private bool isDragging = false;
    private Camera currentCam;
    private float curCamOriginDepth;
    private Vector3 curMaskOriginScale;
    private AudioSource[] _as;
    private GameObject currMask;
    private Vector3 lastMousePosition = Vector3.zero;

    void Update(){
        if (Input.GetMouseButtonDown(0)) { //检测鼠标左键是否点击
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity)) {
                if(hit.collider.tag == "mask"){
                    isDragging = true;
                    currMask = hit.collider.gameObject;
                    curMaskOriginScale = currMask.transform.localScale;
                    _as = currMask.GetComponents<AudioSource>();
                    foreach(AudioSource au in _as){
                        au.Play();
                    }
                    currentCam = hit.collider.gameObject.GetComponent<GetCam>().cam;
                    curCamOriginDepth = currentCam.depth;
                    currentCam.depth = 100;
                }
            }
        }

        if (Input.GetMouseButtonUp(0))
	    {
            if(isDragging){
                currentCam.depth = curCamOriginDepth;
                if(GameFlowManager.GFM.duringStage(6)){
                    currMask.transform.localScale = curMaskOriginScale;
                }
                foreach(AudioSource au in _as){
                    au.Pause();
                }
            }
            isDragging = false;
	        lastMousePosition = Vector3.zero;
	    }

        if(isDragging){
            if (lastMousePosition != Vector3.zero)
	        {
	            Vector3 offset = Camera.main.ScreenToWorldPoint(Input.mousePosition) - lastMousePosition;
	            currMask.transform.position += offset;
                if(GameFlowManager.GFM.duringStage(6)){
                    if(currMask.transform.localScale.x < 100){
                        currMask.transform.localScale *= 1.1f;
                    }
                    
                }
	        }
            lastMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        
    }
}