using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

[RequireComponent(typeof(CharacterController))]
public class DroneController : MonoBehaviour
{
    public Animator FSM;

   public Action TiltSpeedReached;
   public Action MaxSpeedReached;
   public Action MinSpeedReached;
   public Action InputMovementReceived;
   public Action InputMovementReleased;
    public Action InputCameraReceived;
   public Action InputCameraReleased;
   public Action Loaded;
    public Action Zoom;

    public float CurrentSpeed;

    public CharacterController CharacterCtrl;

    public CinemachineVirtualCamera DroneCamera;

    private void OnEnable()
    {
        MaxSpeedReached += SetMaxSpeedTrigger;
        MinSpeedReached += SetMinSpeedReachedTrigger;
        InputMovementReceived += SetInputMovementReceivedTrigger;
        InputMovementReleased += SetMovementInputReleasedTrigger;
        Loaded += SetLoadedTrigger;
    }

    private void OnDisable()
    {
        MaxSpeedReached -= SetMaxSpeedTrigger;
        MinSpeedReached -= SetMinSpeedReachedTrigger;
        InputMovementReceived -= SetInputMovementReceivedTrigger;
        InputMovementReleased -= SetMovementInputReleasedTrigger;
        Loaded -= SetLoadedTrigger;
    }

    private void SetMaxSpeedTrigger()
    {
        FSM.SetTrigger("MaxSpeedReached");
    }

    private void SetInputMovementReceivedTrigger()
    {
        FSM.SetTrigger("InputMovementReceived");
    }

    private void SetMovementInputReleasedTrigger()
    {
        FSM.SetTrigger("MovementInputReleased");
    }

    private void SetMinSpeedReachedTrigger()
    {
        FSM.SetTrigger("MinSpeedReached");
    }

    private void SetLoadedTrigger()
    {
        FSM.SetTrigger("Loaded");
    }
    
}
