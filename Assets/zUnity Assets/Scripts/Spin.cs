using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AniamtionControoler : MonoBehaviour
{

    public float speed = 1;
    void Start()
    {
        
    }

    void Update()
    {
          transform.Rotate(0, speed * Time.deltaTime, 0);
        
    }


    public void BtnRotate(float speed1) 
    {
        speed = speed1;
    }
}
