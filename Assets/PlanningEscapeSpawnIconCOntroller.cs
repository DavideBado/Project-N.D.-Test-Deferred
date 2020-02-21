using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanningEscapeSpawnIconCOntroller : MonoBehaviour
{
    DroneMoveController _drone;
    // Start is called before the first frame update
    void Start()
    {
        _drone = GameManager.instance.Drone;
    }

    // Update is called once per frame
    void Update()
    {
        if(_drone) transform.LookAt(new Vector3(_drone.transform.position.x, transform.position.y, _drone.transform.position.z));
        else _drone = GameManager.instance.Drone;
    }
}
