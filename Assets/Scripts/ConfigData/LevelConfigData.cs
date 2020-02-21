using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Level Data", menuName = "LevelData/Level", order = 0)]
public class LevelConfigData : ScriptableObject
{
    public List<CheckpointConfigData> Checkpoints = new List<CheckpointConfigData>();
    public List<Collectable> CollectableItems = new List<Collectable>();
}
