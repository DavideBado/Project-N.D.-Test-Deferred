using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceableSpotGraphicsController : MonoBehaviour
{
    public delegate void SetGraphicsActive(bool _active);

    public SetGraphicsActive SetSelectedGraphichs, SetExeGraphichs;

    public GameObject SelectedPlanGraphicsObj, NotSelectedPlanGraphicsObj, ExeGraphicsObj;


    private void OnEnable()
    {
        SetSelectedGraphichs += ActivePlanGraphics;
        SetExeGraphichs += ActiveExeGraphics;
    }

    private void OnDisable()
    {
        SetSelectedGraphichs -= ActivePlanGraphics;
        SetExeGraphichs -= ActiveExeGraphics;
    }

    private void ActivePlanGraphics(bool _active)
    {
        if(NotSelectedPlanGraphicsObj) NotSelectedPlanGraphicsObj.SetActive(!_active);
        if(SelectedPlanGraphicsObj) SelectedPlanGraphicsObj.SetActive(_active);
    }

    private void ActiveExeGraphics(bool _active)
    {
        ExeGraphicsObj.SetActive(_active);
    }
}
