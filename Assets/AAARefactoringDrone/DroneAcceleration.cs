using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneAcceleration : DroneMoveState
{
    float acceleration;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
    }
    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        acceleration = Mathf.Sqrt(Mathf.Pow(data.Acceleration_Z * Input.GetAxis("Vertical"), 2) + Mathf.Pow(data.Acceleration_X * Input.GetAxis("Horizontal"), 2));

        drone.CurrentSpeed += acceleration * Time.deltaTime;

        if(drone.CurrentSpeed >= data.SpeedMaxValue)
        {
            drone.CurrentSpeed = data.SpeedMaxValue;
            drone.MaxSpeedReached?.Invoke();
        }
        else
        {
            Move(SetDirection());
        }
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
