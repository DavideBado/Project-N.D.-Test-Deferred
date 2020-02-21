using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Drone Movement Data", menuName = "Data/Player/Planning/Movement", order = 0)]
public class DroneMovement_ConfigData : ScriptableObject
{
    public float SpeedMaxValue;
    public float StartTilAt;
    public float DecelerationTime;

    public float Acceleration_Z;
    public float Acceleration_X;

    [HideInInspector]
    public Vector3 LastDirection;
}
