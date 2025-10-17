using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {

        if (GameCtrl.Thegames.index != 0)
        {
            return;
        }

        if (other.gameObject.GetComponent<Rigidbody>())
        {
            other.gameObject.GetComponent<Rigidbody>().useGravity = false;
        }
        other.gameObject.transform.parent = GameCtrl.Thegames.GameObj;
        GameCtrl.Thegames.index++;
    }
}
