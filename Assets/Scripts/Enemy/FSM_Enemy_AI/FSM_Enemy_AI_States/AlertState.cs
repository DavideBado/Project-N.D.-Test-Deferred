using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AlertState : StateMachineBehaviour
{
    EnemyNavController m_enemyNavController;
    EnemyAI enemyAI;
    NavMeshAgent agent;
    float timer = 2;
    float savedTime;

    Vector3 SavedPosition = new Vector3();
    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        m_enemyNavController = animator.GetComponent<EnemyNavController>();
        enemyAI = animator.GetComponent<EnemyAI>();
        agent = animator.GetComponent<NavMeshAgent>();

        //m_enemyNavController.GetComponent<MeshRenderer>().material = m_enemyNavController.graphicsController.AlertMat;
        m_enemyNavController.graphicsController.AlertAnimGObj.SetActive(true);

        agent.speed = 0;
        agent.isStopped = true;
        savedTime = Time.time;

        SavedPosition = agent.transform.position;
    }

    //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent.transform.position = SavedPosition;
        if (m_enemyNavController.NoiseTarget && m_enemyNavController.currentNoiseType == NoiseController.NoiseType.Run) enemyAI.EmenyHeardRun?.Invoke();
        else
        if (m_enemyNavController.VisibleTarget)
        {
            m_enemyNavController.OldVisibleTarget = m_enemyNavController.VisibleTarget;
            timer = 2;
            if (Time.time - savedTime >= m_enemyNavController.CounterUpdateTime)
            {
                m_enemyNavController.Counter += m_enemyNavController.ModCounter(m_enemyNavController.transform, m_enemyNavController.VisibleTarget);
                savedTime = Time.time;
            }
            m_enemyNavController.transform.LookAt(m_enemyNavController.VisibleTarget.transform.position);
            if (m_enemyNavController.Counter >= m_enemyNavController.Counter_Alert_MaxValue) enemyAI.AlertStateMaxCounter?.Invoke();
        }
        else
        {
            if (m_enemyNavController.NoiseTarget)
            {
                if (m_enemyNavController.currentNoiseType == NoiseController.NoiseType.Walk)
                {
                    timer = 2;
                   m_enemyNavController.transform.LookAt(m_enemyNavController.NoisePosition);
                }
                else if (m_enemyNavController.currentNoiseType == NoiseController.NoiseType.Object)
                {
                    enemyAI.EmenyAloneHeardObj?.Invoke();
                }
            }
            else
            {
                timer -= Time.deltaTime;
                if (timer <= 0) enemyAI.AlertStateMissThePlayer?.Invoke();
            }
        }

        CheckHiddenPlayer();
    }

    //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent.isStopped = false;
        m_enemyNavController.graphicsController.AlertAnimGObj.SetActive(false);
        //m_enemyNavController.GetComponent<MeshRenderer>().material = m_enemyNavController.graphicsController.PatrolMat;
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
}