using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class ExecutionPhaseState : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        GameManager.instance.Setup();       
        GameManager.instance.OnExePhase = true;
        //GameManager.instance.UI_Manager.PhaseTxt.text = "ExecutionPhase";
        //GameManager.instance.UI_Manager.PhaseTxt.gameObject.SetActive(true);
        GameManager.instance.UI_Manager.ExeUI.SetActive(true);
        GameManager.instance.Player.camSpots.Clear();
        GameManager.instance.Player.camSpots = GameManager.instance.Drone.camSpots;
        GameManager.instance.Player.gameObject.SetActive(true);

        GameManager.instance.Drone.gameObject.SetActive(false);
        GameManager.instance.Drone.DroneCamera.gameObject.SetActive(false);

        GameManager.instance.Player.freeLookCamera.enabled = true;
        GameManager.instance.Player.freeLookCamera.Priority = 50;
       

        GameManager.instance.Player.SpotCameraScreen = GameManager.instance.UI_Manager.SpotCameraScreen;
        if (GameManager.instance.Player.camSpots.Count == 0)
        {
            GameManager.instance.UI_Manager.SpotCameraScreenGObj.SetActive(false);
        }
        else
        {
            GameManager.instance.UI_Manager.SpotCameraScreenGObj.SetActive(true);
            GameManager.instance.UI_Manager.SpotCameraScreen.enabled = true;
            GameManager.instance.Player.SpotCamera.gameObject.SetActive(true);
        }

        GameManager.instance.Drone.DroneCamera.Priority = 0;

        GameManager.instance.Player.transform.position = GameManager.instance.CurrentStartSpot.SpawnPosition.position;
        GameManager.instance.Player.ResetPosition = GameManager.instance.CurrentStartSpot.SpawnPosition.position;
        GameManager.instance.Player.GetComponent<NavMeshObstacle>().enabled = true;
        GameManager.instance.Player.currentSpeed = GameManager.instance.Player.walkSpeed;
        GameManager.instance.Player.isCrouching = false;
        GameManager.instance.Player.Noise.GetComponent<NoiseController>().Reset?.Invoke();
        GameManager.instance.Player.Graphics.SetActive(true);
        GameManager.instance.Player.Collider.enabled = true;
        GameManager.instance.Player.ObstacleNav.enabled = true;

        GameManager.instance.UI_Manager.directionSpriteController.enabled = true;

        foreach (EnemyAI _enemyAI in GameManager.instance.Level_Manager.EnemiesAI)
        {
            EnemyNavController enemyController = _enemyAI.GetComponent<EnemyNavController>();
            enemyController.VisibleTarget = null;
            enemyController.OldVisibleTarget = null;
            enemyController.TargetPrevHidingState = false;
            enemyController.TargetCurrentHidingState = false;
            enemyController.HiddenTarget = null;
            enemyController.NoiseTarget = null;
            enemyController.currentNoiseType = 0;
            enemyController.prevNoiseType = 0;
            //_enemyAI.AI_FSM.SetTrigger("ChangePhase");
            _enemyAI.AI_FSM.SetTrigger("ToExePhase");
            _enemyAI.GetComponent<CapsuleCollider>().enabled = true;

            enemyController.IdleTriggerTrapPosition = GameManager.instance.CurrentStartSpot.SpawnPosition.position;
        }
        GameManager.instance.OnExePhaseAction?.Invoke();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        GameManager.instance.UI_Manager.directionSpriteController.DirectionImage.gameObject.SetActive(false);
        GameManager.instance.UI_Manager.directionSpriteController.enabled = false;
        GameManager.instance.OnExePhase = false;
        GameManager.instance.UI_Manager.ExeUI.SetActive(false);

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
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
