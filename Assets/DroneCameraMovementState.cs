using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneCameraMovementState : DroneCameraStateBase
{
    float mouseX;
    float mouseY;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    //override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        CheckInput();
        UpdateDroneRotation();
        UpdateCameraRotation();
    }

    private void UpdateDroneRotation()
    {
        mouseX += Input.GetAxis("Mouse X") * data.RotationHorSpeed;
        PlayerChCtrl.transform.rotation = Quaternion.Euler(0, mouseX, 0);
    }

    private void UpdateCameraRotation()
    {
        mouseY = Mathf.Clamp(mouseY += Input.GetAxis("Mouse Y") * data.RotationVertSpeed, data.RotationVertMinValue, data.RotationVertMaxValue);

        droneCamera.transform.rotation = Quaternion.Euler(mouseY, 0, 0);
    }

    private void CheckInput()
    {
        if (Input.GetAxis("Mouse X") == 0 && Input.GetAxis("Mouse Y") == 0) drone.InputCameraReleased?.Invoke();
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
