using System.Collections;
using System;
using UnityEngine;

public class DroneDecelerationState : DroneMoveState
{
    float fromSpeed;
    float decelerationTimeProportional;
    float startTime;
    float ratio;
    public bool useRealtime = false;
    public float CurrentTime => useRealtime ? Time.realtimeSinceStartup : Time.time;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);

        SetupDecelData();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Deceleration();

        if (SetDirection() == Vector3.zero)
            Move(data.LastDirection);
        else drone.InputMovementReceived?.Invoke();
    }
    
    public void Deceleration()
    {
        if (drone.CurrentSpeed > 0)
        {
            drone.CurrentSpeed -= fromSpeed / data.DecelerationTime * Time.deltaTime;
        }
        else 
        {
            drone.CurrentSpeed = 0;
            drone.MinSpeedReached?.Invoke();
        }
    }

    private void SetupDecelData()
    {
        fromSpeed = drone.CurrentSpeed;
    }
}
