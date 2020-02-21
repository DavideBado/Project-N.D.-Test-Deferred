using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Capsulo : MonoBehaviour
{
    public GameObject Graphics;
    public GameObject DirectionTarget;

    private void OnCollisionEnter(Collision collision)
    {
        if (GameManager.instance.OnExePhase) if (collision.transform.GetComponent<PlayerMovController>())
            {
                collision.transform.GetComponent<PlayerMovController>().GoldenlEgg = this;
                GameManager.instance.UI_Manager.TreasureIcon.SetActive(true);
                GameManager.instance.PostObjective?.Invoke();
                Graphics.SetActive(false);
            }
    }
}
