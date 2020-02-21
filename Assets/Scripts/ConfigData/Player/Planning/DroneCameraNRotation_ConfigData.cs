using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Drone Camera&Rotation Data", menuName = "Data/Player/Planning/Camera&Rotation", order = 1)]

public class DroneCameraNRotation_ConfigData : ScriptableObject
{
    public float TitlMaxDeg;
    [HideInInspector]
    public float TitlCurrentDeg;
    public float TitlSpeed;

    public float RotationHorSpeed;

    public float RotationVertSpeed;
    public float RotationVertMinValue;
    public float RotationVertMaxValue;

    public float[] ZoomValues;
    [HideInInspector]
    public int currentZoomIndex;
    public float ZoomTime;
}
