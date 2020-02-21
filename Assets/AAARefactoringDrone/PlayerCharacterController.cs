using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerCharacterController : MonoBehaviour
{
    public Animator FSM;

    public Action Loaded;
    public Action InputMovementReceived;
    public Action InputMovementReleased;

    public float CurrentSpeed;

    public CharacterController CharacterCtrl;

    private void OnEnable()
    {
        InputMovementReceived += SetInputMovementReceivedTrigger;
        InputMovementReleased += SetMovementInputReleasedTrigger;
        Loaded += SetLoadedTrigger;
    }

    private void OnDisable()
    {
        InputMovementReceived -= SetInputMovementReceivedTrigger;
        InputMovementReleased -= SetMovementInputReleasedTrigger;
        Loaded -= SetLoadedTrigger;
    }

    private void SetInputMovementReceivedTrigger()
    {
        FSM.SetTrigger("InputMovementReceived");
    }

    private void SetMovementInputReleasedTrigger()
    {
        FSM.SetTrigger("MovementInputReleased");
    }

    private void SetLoadedTrigger()
    {
        FSM.SetTrigger("Loaded");
    }
}
