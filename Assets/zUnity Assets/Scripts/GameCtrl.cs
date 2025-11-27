using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class GameCtrl : MonoBehaviour
{
  


    [SerializeField] Transform[] ropeSegments; 
    [SerializeField] LineRenderer lineRenderer;
    [SerializeField] float startWidth = 0.02f;
    [SerializeField] float endWidth = 0.02f;




   
    [SerializeField] Button Downobj;

    public Transform GameObj;
    [SerializeField] Transform TheBox;
    [SerializeField] Transform TheCrane;
    [SerializeField] Transform TheGearRig;

    [SerializeField] float Speed=2f;
    public int index;
    private float targetY;
    private float smoothFactor = 20f;





    [SerializeField] List<EventTrigger> trigger;
    private UnityEvent onHold=new UnityEvent();
    private bool isHolding = false;
    private string ButtonName;

   
    // Start is called before the first frame update
    void Start()
    {
        lineRenderer.positionCount = ropeSegments.Length; 
        lineRenderer.startWidth = startWidth;
        lineRenderer.endWidth = endWidth;

      //  Downobj.onClick.AddListener(DownObj);

        for (int i = 0; i < trigger.Count; i++)
        {
            AddEvent(trigger[i], EventTriggerType.PointerDown, (data) =>
            {
                isHolding = true;

                ButtonName = ((PointerEventData)data).pointerCurrentRaycast.gameObject.name;
            });
            AddEvent(trigger[i], EventTriggerType.PointerUp, (data) => isHolding = false);
        }
       
        onHold.AddListener(()=> { ObjMove(ButtonName); });
    }

    // Update is called once per frame
    void Update()
    {
        if (isHolding)
        {
            onHold?.Invoke();
        }
        UpdateLineRenderer();
       
    }
   private void UpdateLineRenderer()
    {
        for (int i = 0; i < ropeSegments.Length; i++)
        {
           
            lineRenderer.SetPosition(i, ropeSegments[i].position);
        }
    }
  
    public void ObjMove(string name)
    {
      
        

            switch (name)
            {
                case "Up":
                    targetY = GameObj.transform.position.y + 1 * Speed * Time.deltaTime;
                    TheBox.transform.Rotate(Vector3.up, Speed*80 * Time.deltaTime, Space.Self);
                    break;
                case "Down":
                    targetY = GameObj.transform.position.y - 1 * Speed * Time.deltaTime;
                    TheBox.transform.Rotate(Vector3.up, -Speed*80 * Time.deltaTime, Space.Self);
                    break;
                case "Right":
                     TheCrane.transform.Rotate(Vector3.up, (Speed * 10 * Time.deltaTime), Space.Self);
                      TheGearRig.transform.Rotate(Vector3.up, -(Speed * 80 * Time.deltaTime), Space.Self);
                break;
                case "Left":
                     TheCrane.transform.Rotate(Vector3.up, -(Speed * 10 * Time.deltaTime), Space.Self);
                      TheGearRig.transform.Rotate(Vector3.up, (Speed * 80 * Time.deltaTime), Space.Self);
                break;

            }
            float currentY = Mathf.Lerp(GameObj.transform.position.y, targetY, smoothFactor / Time.deltaTime);

        //  Debug.Log(currentY);
        if (name == "Up" || name == "Down")
        {
            GameObj.transform.position = new Vector3(GameObj.transform.position.x, Mathf.Clamp(currentY, -10f, 3.8f), GameObj.transform.position.z

          );
            GameMoveCtrl.Thegames.AudioPlay("绳索");
           
        }
         
          

        if (name == "Right" || name == "Left")
        {
            GameMoveCtrl.Thegames.AudioPlay("起重机");
        }
      




    }
    public void DownObj()
    {
        if (GameObj.transform.childCount<=0)
        {
            return;
        }
        GameObj.transform.GetChild(0).GetComponent<Rigidbody>().WakeUp();
        GameObj.transform.GetChild(0).GetComponent<Rigidbody>().useGravity = true;
        GameMoveCtrl.Thegames.AudioPlay("解除吸附");
        GameObj.transform.DetachChildren();
       index = 0;
    }
   
    private void AddEvent(EventTrigger trigger, EventTriggerType type, UnityAction<BaseEventData> action)
    {
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = type;
        entry.callback.AddListener(action);
        trigger.triggers.Add(entry);
    }
}
