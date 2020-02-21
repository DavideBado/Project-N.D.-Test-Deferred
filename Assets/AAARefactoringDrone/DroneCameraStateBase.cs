using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public abstract class DroneCameraStateBase : StateMachineBehaviour
{
    public DroneCameraNRotation_ConfigData data;
    protected DroneController drone;
    protected CinemachineVirtualCamera droneCamera;
    protected CharacterController PlayerChCtrl;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        if (!drone) drone = animator.GetComponentInChildren<DroneController>();
        if (!PlayerChCtrl) PlayerChCtrl = drone.CharacterCtrl;
    }
}
