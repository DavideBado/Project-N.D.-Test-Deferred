using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneMoveState : StateMachineBehaviour
{
    public DroneMovement_ConfigData data;
    protected DroneController drone;
    protected CharacterController DroneCharaCtrl;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!drone) drone = animator.GetComponent<PlayerConroller>().Drone;
        if (!DroneCharaCtrl) DroneCharaCtrl = drone.CharacterCtrl;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Move(SetDirection());
    }

    protected virtual void Move(Vector3 _direction)
    {
        DroneCharaCtrl.Move(_direction.normalized * drone.CurrentSpeed * Time.deltaTime);
    }

    protected Vector3 SetDirection()
    {
        Vector3 _direction = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        if (_direction == Vector3.zero) drone.InputMovementReleased?.Invoke();
        else
        {
            data.LastDirection = _direction;
        }
        return _direction;
    }

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
