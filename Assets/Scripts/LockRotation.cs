using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockRotation : MonoBehaviour
{
    public Camera PlayerCamera;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerCamera) transform.rotation = PlayerCamera.transform.rotation;
        else PlayerCamera = Camera.main;
    }
}
