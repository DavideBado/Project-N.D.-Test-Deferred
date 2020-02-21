using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class CameraReset : MonoBehaviour
{
    public CinemachineFreeLook CM_ThirdPerson;

    private float y_Value = 0, x_Value = 0;
    // Start is called before the first frame update
    void Start()
    {
        y_Value = CM_ThirdPerson.m_YAxis.Value;
        x_Value = CM_ThirdPerson.m_XAxis.Value;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("ResetCamera"))
        {
            CM_ThirdPerson.m_YAxis.Value = y_Value;
          CM_ThirdPerson.m_XAxis.Value = x_Value;
        }
    }
}
