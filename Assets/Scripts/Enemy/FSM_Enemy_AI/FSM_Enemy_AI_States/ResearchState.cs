using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ResearchState : StateMachineBehaviour
{
    EnemyNavController m_enemyNavController;
    EnemyAI enemyAI;
    NavMeshAgent agent;
    float savedTime;
    bool PlayerWasInFov = false;
    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        m_enemyNavController = animator.GetComponent<EnemyNavController>();
        enemyAI = animator.GetComponent<EnemyAI>();
        agent = animator.GetComponent<NavMeshAgent>();
        agent.speed = m_enemyNavController.ResearchSpeed;
        m_enemyNavController.graphicsController.LookAroundAnimGObj.SetActive(true);
        savedTime = Time.time;

        GameManager.instance.CheckEnemiesStateNPosition?.Invoke();
        GameManager.instance.EnemiesInResearch.Add(m_enemyNavController);
        PlayerWasInFov = true;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (m_enemyNavController.VisibleTarget)
        {
            if(!PlayerWasInFov)
            {
                GameManager.instance.EnemiesInResearch.Add(m_enemyNavController);
                PlayerWasInFov = true;
            }
            m_enemyNavController.OldVisibleTarget = m_enemyNavController.VisibleTarget;
            if (Time.time - savedTime >= m_enemyNavController.CounterUpdateTime)
            {
                m_enemyNavController.Counter += m_enemyNavController.ModCounter(m_enemyNavController.transform, m_enemyNavController.VisibleTarget);
                savedTime = Time.time;
            }
                agent.destination = new Vector3(m_enemyNavController.VisibleTarget.position.x, m_enemyNavController.transform.position.y, m_enemyNavController.VisibleTarget.position.z);
            if (m_enemyNavController.Counter >= m_enemyNavController.Counter_Research_MaxValue) enemyAI.ResearchStateMaxCounter?.Invoke();
        }
        else
        {
            CheckHiddenPlayer();
            if (m_enemyNavController.NoiseTarget)
            {
                
                agent.SetDestination( new Vector3(m_enemyNavController.NoisePosition.x, m_enemyNavController.transform.position.y, m_enemyNavController.NoisePosition.z));
                if(m_enemyNavController.currentNoiseType == NoiseController.NoiseType.Object)
                {
                     GameManager.instance.EnemiesInResearch.Remove(m_enemyNavController);
                     PlayerWasInFov = false;
                }
            }
            else if (agent.pathStatus == NavMeshPathStatus.PathComplete && agent.remainingDistance < 1f)
            {
                enemyAI.ResearchStateMissPlayer?.Invoke();
            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (PlayerWasInFov)
        {
            GameManager.instance.EnemiesInResearch.Remove(m_enemyNavController);
            PlayerWasInFov = false;
        }
        GameManager.instance.CheckEnemiesStateNPosition?.Invoke();
        m_enemyNavController.graphicsController.LookAroundAnimGObj.SetActive(false);
    }

    private void CheckHiddenPlayer()
    {
        if (m_enemyNavController.OldVisibleTarget)
        {
            m_enemyNavController.TargetPrevHidingState = m_enemyNavController.TargetCurrentHidingState;
            m_enemyNavController.TargetCurrentHidingState = m_enemyNavController.OldVisibleTarget.GetComponent<PlayerMovController>().isHiding;
            if (!m_enemyNavController.TargetPrevHidingState && m_enemyNavController.TargetCurrentHidingState)
            {
                m_enemyNavController.HiddenTarget = m_enemyNavController.OldVisibleTarget;
                enemyAI.EmenySeePlayerInHidingSpot?.Invoke();
            }
            m_enemyNavController.OldVisibleTarget = m_enemyNavController.VisibleTarget;
        }
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
