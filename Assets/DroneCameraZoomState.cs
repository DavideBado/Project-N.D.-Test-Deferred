using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneCameraZoomState : DroneCameraStateBase
{
    float startValue, endValue, deltaValue;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        SetupValues();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Zoom();
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    private void SetupValues()
    {
        startValue = data.ZoomValues[data.currentZoomIndex];
        endValue = data.ZoomValues[(data.currentZoomIndex + 1) % data.ZoomValues.Length];
        deltaValue = (endValue - startValue) / data.ZoomTime;
    }

    private void Zoom()
    {
        droneCamera.m_Lens.FieldOfView += deltaValue * Time.deltaTime;
        if(deltaValue <= 0) if(droneCamera.m_Lens.FieldOfView <= endValue)
            {
                droneCamera.m_Lens.FieldOfView = endValue;
                drone.Zoom?.Invoke();
            }
        else if (deltaValue >= 0) if (droneCamera.m_Lens.FieldOfView >= endValue)
            {
                droneCamera.m_Lens.FieldOfView = endValue;
                drone.Zoom?.Invoke();
            }
    }
}
