using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceableSpot : SpotBase
{
    public Collider ColliderForRayCast;
    public PlaceableSpotGraphicsController Graphics;
    public GameObject PlanObj;
    public PlaceableSpotType SpotType;
     
    public enum PlaceableSpotType
    {
        Null,
        EscapePoint,
        StartinPoint,
        Hiding,
        Cam,
        Multi
    }

    private void OnEnable()
    {
        GameManager.instance.OnExePhaseAction += DisableCollider;
        GameManager.instance.OnExePhaseAction += DisablePlanGraphics;
    }

    private void OnDisable()
    {
        GameManager.instance.OnExePhaseAction -= DisableCollider;
        GameManager.instance.OnExePhaseAction -= DisablePlanGraphics;
    }

    private void DisableCollider()
    {
        if (ColliderForRayCast) ColliderForRayCast.enabled = false;
        else GetComponent<Collider>().enabled = false;
    }
    private void DisablePlanGraphics()
    {
        Graphics.SetExeGraphichs(true);
        PlanObj.SetActive(false);
    }
}
