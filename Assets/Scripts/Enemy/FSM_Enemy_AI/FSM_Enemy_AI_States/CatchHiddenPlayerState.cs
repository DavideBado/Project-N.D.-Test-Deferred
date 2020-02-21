using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class CatchHiddenPlayerState : StateMachineBehaviour
{
    EnemyNavController m_enemyNavController;
    EnemyAI enemyAI;
    NavMeshAgent agent;

    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        m_enemyNavController = animator.GetComponent<EnemyNavController>();
        enemyAI = animator.GetComponent<EnemyAI>();
        agent = animator.GetComponent<NavMeshAgent>();
        agent.speed = m_enemyNavController.RunSpeed;

        GameManager.instance.Player.currentSpeed = 0;
        GameManager.instance.Player.enabled = false;
    }
    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent.destination = new Vector3(m_enemyNavController.HiddenTarget.position.x, m_enemyNavController.transform.position.y, m_enemyNavController.HiddenTarget.position.z);

        if(agent.pathStatus == NavMeshPathStatus.PathComplete && m_enemyNavController.VisibleTarget != null)
        {
           m_enemyNavController.graphicsController.StartAttackAnimation?.Invoke();
            //GameManager.instance.PlayerCaught?.Invoke();
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
