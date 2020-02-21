using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Character Movement Data", menuName = "Data/Player/Exe/Movement", order = 0)]
public class CharacterMovement_ConfigData : ScriptableObject
{
    public float WalkSpeed;
    public float RunSpeed;
    public float CrouchSpeed;
}
