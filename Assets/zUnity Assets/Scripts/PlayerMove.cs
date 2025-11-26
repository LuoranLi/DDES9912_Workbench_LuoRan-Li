using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private static PlayerMove instance;
    public static PlayerMove Instance { get { return instance; } set { instance = value; } }

  
    public float MoveSpeed = 10f;
    public float RotaSpeed = 5f;
    public float CameraYMin = -25f;
    public float CameraYMax = 25f;
    public float JumpHeight = 2f; 
    public float Gravity = -9.81f; 
    public AudioClip[] FootstepClips; 
    public AudioSource AudioSource; 

    public CharacterController CC;
    public Camera TheManCamera;
    private Vector3 velocity; 
    private Vector3 vect;
    private Quaternion quate;
    private Quaternion Cameraquate;
    private bool isGrounded;
    private float footstepTimer; 
    private float footstepInterval = 0.3f; 

    public bool IsMove;

    private void Start()
    {
        instance = this;
    }

    void Update()
    {
        if (!IsMove) return;

            isGrounded = CC.isGrounded;
        if (isGrounded && velocity.y < 0)
            velocity.y = -2f;

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        MoveData(new Vector2(h, v));
        float MX = Input.GetAxis("Mouse X");
        float MY = Input.GetAxis("Mouse Y");
        Rotatia(new Vector2(MX, MY));


        if (Input.GetButtonDown("Jump") && isGrounded && IsMove)
        {
            velocity.y = Mathf.Sqrt(JumpHeight * -2f * Gravity);
        }


        velocity.y += Gravity * Time.deltaTime;
        CC.Move(velocity * Time.deltaTime);

   
      //  HandleFootsteps(h, v);
    }

    public void MoveData(Vector2 moving)
    {
        if (IsMove)
        {
        
            vect = transform.forward * moving.y + transform.right * moving.x;
            vect = vect.normalized; 
            CC.Move(vect * Time.deltaTime * MoveSpeed);
        }
    }

    public void Rotatia(Vector2 Mouse)
    {
        if (IsMove)
        {
           
            quate = Quaternion.Euler(0, transform.localEulerAngles.y + Mouse.x, 0);
            transform.localRotation = Quaternion.Lerp(transform.localRotation, quate, RotaSpeed * Time.deltaTime);

       
            float newX = CheckAngle(TheManCamera.transform.localEulerAngles.x - Mouse.y);
            newX = Mathf.Clamp(newX, CameraYMin, CameraYMax);
            Cameraquate = Quaternion.Euler(newX, 0, 0);
            TheManCamera.transform.localRotation = Quaternion.Lerp(TheManCamera.transform.localRotation, Cameraquate, RotaSpeed * Time.deltaTime);
        }
    }


    private void HandleFootsteps(float h, float v)
    {
        bool isMoving = (Mathf.Abs(h) > 0.1f || Mathf.Abs(v) > 0.1f) && isGrounded && IsMove;

        if (isMoving)
        {
            footstepTimer += Time.deltaTime;
   
            if (footstepTimer >= footstepInterval && !AudioSource.isPlaying)
            {
                PlayRandomFootstep();
                footstepTimer = 0;
            }
        }
        else
        {

            footstepTimer = 0;
        }
    }


    private void PlayRandomFootstep()
    {
        if (FootstepClips.Length == 0) return;

        int randomIndex = Random.Range(0, FootstepClips.Length);
        AudioSource.clip = FootstepClips[randomIndex];
        AudioSource.Play();
    }

    float CheckAngle(float value)
    {
        float angle = value - 180;
        if (angle > 0)
        {
            return angle - 180;
        }
        return angle + 180;
    }

}
