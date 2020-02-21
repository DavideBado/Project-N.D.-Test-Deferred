using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeySpot : PlaceableSpot
{
    public GameObject DirectionTarget;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.GetComponent<PlayerMovController>() != null)
        {
            other.transform.GetComponent<PlayerMovController>().haveTheKey = true;
            GameManager.instance.UI_Manager.KeyIcon.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
