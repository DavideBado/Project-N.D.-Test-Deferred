using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterIdleMovementState : StateMachineBehaviour
{
    PlayerCharacterController Character;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        if (!Character) Character = animator.GetComponent<PlayerConroller>().Character;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0) Character.InputMovementReceived?.Invoke();
    }
}
