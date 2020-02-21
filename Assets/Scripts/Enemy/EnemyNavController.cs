using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyNavController : MonoBehaviour
{
    public EnemyGraphicsController graphicsController;
    public List<Transform> PathTargets = new List<Transform>();
    [HideInInspector]
    public Transform VisibleTarget;
    [HideInInspector]
    public Transform OldVisibleTarget;

    [HideInInspector]
    public bool TargetPrevHidingState = false;
    [HideInInspector]
    public bool TargetCurrentHidingState = false;
    [HideInInspector]
    public Transform HiddenTarget;

    [HideInInspector]
    public Transform NoiseTarget;
    [HideInInspector]
    public Vector3 NoisePosition;
    [HideInInspector]
    public NoiseController.NoiseType currentNoiseType;
    [HideInInspector]
    public NoiseController.NoiseType prevNoiseType;

    [HideInInspector]
    public int visibleTargetArea;
    public NavMeshAgent agent;
    public bool FermatiAGuardare;
    public FieldOfView fieldOfView;
    float fieldOfViewOriginalViewRadius;   
    Vector3 lastPlayerPos;
    Vector3 myLastPos;   
    public float TimeForLookAround;
    float rotTimer;
    public float GameOverDist;

    //[Range(0, 5)]
    //public List<float> ModCounters = new List<float>();

    public float Counter_Patrol_MaxValue;

    public float Counter_Alert_MaxValue;

    public float Counter_Research_MaxValue;

    public float Counter_Pursue_MaxValue;

    public float PostObjectivePatrolCounterValue;
   
    public float WalkSpeed;
    public float ResearchSpeed;
    public float RunSpeed;

    [HideInInspector]
    public float Counter;
    public float CounterUpdateTime;

    public Vector3 IdlePosition;
    public Vector3 IdleTriggerTrapPosition;
    public Vector3 IdleTriggerTrapDim;

    public float ModCounter(Transform _enemy, Transform _player)
    {
        return DistCounter(_enemy.position, _player.position) * SinCounter(_enemy, _player);
    }

    private float DistCounter(Vector3 _origin, Vector3 _player)
    {
        return 1.1f - (Vector3.Distance(_origin, _player) / fieldOfView.viewRadius);
    }

    private float SinCounter(Transform _origin, Transform _player)
    {
        return Mathf.Sin(Mathf.Deg2Rad * Vector3.Angle(_origin.transform.position - _player.transform.position, _origin.transform.right));
    }

    private void OnEnable()
    {
        IdlePosition = transform.position;
        GameManager.instance.PostObjective += SetPostObjectiveCounter;
    }

    private void OnDisable()
    {
        GameManager.instance.PostObjective -= SetPostObjectiveCounter;
    }

    private void SetPostObjectiveCounter()
    {
        if (Counter < PostObjectivePatrolCounterValue) Counter = PostObjectivePatrolCounterValue;
    }
}