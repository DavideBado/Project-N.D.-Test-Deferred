using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Checkpoint Data", menuName = "LevelData/Checkpoint", order = 1)]
public class CheckpointConfigData : ScriptableObject
{
    public Vector3 Position;
    public Quaternion Rotation;
    public List<Collectable> PlayerItems = new List<Collectable>();
}
