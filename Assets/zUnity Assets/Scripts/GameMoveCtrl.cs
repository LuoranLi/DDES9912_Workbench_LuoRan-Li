using System.Collections.Generic;
using UnityEngine;

public class GameMoveCtrl : MonoBehaviour
{

    private static GameMoveCtrl theGames;
    public static GameMoveCtrl Thegames { get { return theGames; } set { theGames = value; } }

    [SerializeField] List<GameCtrl> Thagame;
    [SerializeField] AudioSource TheAudioSou;
    [SerializeField] List<AudioClip> TheAudioClip;
    [SerializeField] GameObject TheCar;
    [SerializeField] float MoveSpeed = 5f;
    [SerializeField] bool UseLocalDirection = true;


    [SerializeField] bool EnableZLimit = true;
    [SerializeField] float MinZ = -50f;
    [SerializeField] float MaxZ = 50f;

    private void Awake()
    {
        theGames = this;
    }
    public  void Cartags(string butname)
    {

        //  Debug.Log(butname);
        //PlayerMove.Instance.IsMove = false;
        switch (butname)
        {
            case "Up":
                MoveForward();
                break;
            case "Down":
                MoveBackward();
                break;
        }
        AudioPlay("小车");
    }
    public void AudioPlay(string Name)
    {
        if (TheAudioSou.isPlaying) return;

        for (int i = 0; i < TheAudioClip.Count; i++)
        {
            if (TheAudioClip[i].name== Name)
            {
                TheAudioSou.clip = TheAudioClip[i];
                TheAudioSou.Play();
                return;
            }
        }

    }
    public void AudioStop()
    {
        if (!TheAudioSou.isPlaying) return;
        TheAudioSou.Stop();
    }
    public void MachineTags(string gametag,string butname)
    {
       
        if (gametag=="gameC1")
        {
            switch (butname)
            {
                case "Up":
                    Thagame[0].ObjMove("Up");
                    break;
                case "Down":
                    Thagame[0].ObjMove("Down");
                    break;
                case "Left":
                    Thagame[0].ObjMove("Left");
                    break;
                case "Right":
                    Thagame[0].ObjMove("Right");
                    break;
                case "ReMove":
                    Thagame[0].DownObj();
      
                    break;
            }
        }
        else if (gametag == "gameC2")
        {
            switch (butname)
            {
                case "Up":
                    Thagame[1].ObjMove("Up");
                    break;
                case "Down":
                    Thagame[1].ObjMove("Down");
                    break;
                case "Left":
                    Thagame[1].ObjMove("Left");
                    break;
                case "Right":
                    Thagame[1].ObjMove("Right");
                    break;
                case "ReMove":
                    Thagame[1].DownObj();

                    break;
            }
        }
       
    }
    private void MoveForward()
    {
        Vector3 moveDirection = GetMovementDirection();
        UpdatePosition(moveDirection);
    }

    private void MoveBackward()
    {
        Vector3 moveDirection = -GetMovementDirection();
        UpdatePosition(moveDirection);
    }

    private Vector3 GetMovementDirection()
    {
        return UseLocalDirection ? TheCar.transform.forward : Vector3.forward;
    }

    private void UpdatePosition(Vector3 direction)
    {
        direction.Normalize();
        Vector3 targetPosition = TheCar.transform.position + direction * MoveSpeed * Time.deltaTime;

        if (EnableZLimit)
        {
            targetPosition.z = Mathf.Clamp(targetPosition.z, MinZ, MaxZ);
        }

        TheCar. transform.position = targetPosition;
    }

    private void OnDrawGizmosSelected()
    {
        if (!EnableZLimit) return;

        Gizmos.color = new Color(1, 0, 0, 0.3f);
        Vector3 minZCenter = new Vector3(transform.position.x, transform.position.y, MinZ);
        Gizmos.DrawCube(minZCenter, new Vector3(10, 10, 0.1f));

        Vector3 maxZCenter = new Vector3(transform.position.x, transform.position.y, MaxZ);
        Gizmos.DrawCube(maxZCenter, new Vector3(10, 10, 0.1f));
    }
}
