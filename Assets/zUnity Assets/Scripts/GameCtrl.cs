using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class GameCtrl : MonoBehaviour
{
    private static GameCtrl theGames;
    public static GameCtrl Thegames { get { return theGames; } set { theGames = value; } }


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

    private void Awake()
    {
        theGames = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        lineRenderer.positionCount = ropeSegments.Length; 
        lineRenderer.startWidth = startWidth;
        lineRenderer.endWidth = endWidth;

        Downobj.onClick.AddListener(DownObj);

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
  
    private void ObjMove(string name)
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
             if (name != "Right" || name != "Left")
            GameObj.transform.position = new Vector3(GameObj.transform.position.x, Mathf.Clamp(currentY, -2.8f, 1.2f), GameObj.transform.position.z

            );
        
       
          
        
       

    }
    private void DownObj()
    {
        GameObj.transform.GetChild(0).GetComponent<Rigidbody>().useGravity = true;
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
