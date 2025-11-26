using UnityEngine;
using System.Collections.Generic;

public class CubeParentTrigger : MonoBehaviour
{

    public GameObject TheGame;
    public GameObject TheGameObjects;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Cube")|| other.CompareTag("Player"))
        {
           
            other.gameObject.transform.parent = TheGame. gameObject.transform;
        
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {


            other.gameObject.transform.parent = TheGameObjects.gameObject.transform;

        }
    }

}