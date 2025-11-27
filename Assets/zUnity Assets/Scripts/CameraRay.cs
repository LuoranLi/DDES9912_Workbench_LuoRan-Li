using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraRay : MonoBehaviour
{
    public float RayLength = 8f;
    private bool isMouseLocked = false;
    private void Start()
    {
        ToggleMouseLock();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ToggleMouseLock();
           
        }
        if (Input.GetMouseButtonUp(0))
        {
            //   PlayerMove.Instance.IsMove = true;
            GameMoveCtrl.Thegames.AudioStop();
        }
            if (Input.GetMouseButton(0))
        {

     
                Vector3 screenCenter = new Vector3(Screen.width / 2f, Screen.height / 2f, 0f);
                Ray ray = Camera.main.ScreenPointToRay(screenCenter);
                RaycastHit hit;

                if (Physics.Raycast(ray,out hit, RayLength))
                {
                  
                  //  Debug.Log(hit.collider.gameObject.name);
                    if (hit.collider.gameObject.GetComponent<ScalePulse>())
                    {
                        hit.collider.gameObject.GetComponent<ScalePulse>().PulseDown();

                        switch (hit.collider.tag)
                        {
                            case "Car":
                                GameMoveCtrl.Thegames.Cartags(hit.collider.gameObject.GetComponent<ScalePulse>().butName);
                                break;

                        }
                        if (hit.collider.tag== "gameC1"|| hit.collider.tag == "gameC2")
                        {
                            GameMoveCtrl.Thegames.MachineTags(hit.collider.tag, hit.collider.gameObject.GetComponent<ScalePulse>().butName);
                        }
                    }
                }
               
            
          
        }
    }
    private void ToggleMouseLock()
    {
        isMouseLocked = !isMouseLocked;

        if (isMouseLocked)
        {
           
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Confined; 
        }
        else
        {
    
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None; 
        }
    }
}