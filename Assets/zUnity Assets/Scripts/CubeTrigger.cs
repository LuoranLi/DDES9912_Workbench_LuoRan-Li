using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeTrigger : MonoBehaviour
{
    public List<GameCtrl> TheGanes;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
       
        if (TheGanes[0].index != 0|| TheGanes[1].index != 0)
        {
            return;
        }
        switch (gameObject.tag)
        {

            case "gameC1":
                if (other.gameObject.GetComponent<Rigidbody>())
                {
                    other.gameObject.GetComponent<Rigidbody>().useGravity = false;
                }
                other.gameObject.transform.parent = TheGanes[0].GameObj;
                TheGanes[0].index++;
                break;
            case "gameC2":
                if (other.gameObject.GetComponent<Rigidbody>())
                {
                    other.gameObject.GetComponent<Rigidbody>().useGravity = false;
                }
                other.gameObject.transform.parent = TheGanes[1].GameObj;
                TheGanes[1].index++;
                break;
        }
                 other.gameObject.GetComponent<Rigidbody>().Sleep();
    }
}
