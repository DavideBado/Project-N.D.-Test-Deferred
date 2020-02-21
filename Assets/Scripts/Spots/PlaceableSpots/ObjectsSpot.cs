using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsSpot : PlaceableSpot
{
    public List<PlaceableSpotType> SpotTypesForMulti = new List<PlaceableSpotType>();
    public List<GameObject> SpotsForMultiExe = new List<GameObject>();
    public List<GameObject> SpotsForMultiPlan = new List<GameObject>();
}
