using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DirectionSpriteController : MonoBehaviour
{
    public RectTransform DirectionImage;
    public GameObject DirectionTarget;
    public TMP_Text distanceTxt;

    // Update is called once per frame
    void Update()
    {
        if (DirectionImage != null && DirectionTarget != null)
        {

            float dot = Vector3.Dot(Camera.main.transform.forward, (DirectionTarget.transform.position - Camera.main.transform.position).normalized);
            {
                if (dot > 0)
                {
                    distanceTxt.text = Mathf.Round(Vector3.Distance(GameManager.instance.Player.transform.position, DirectionTarget.transform.parent.position)).ToString();
                    DirectionImage.gameObject.SetActive(true);
                    Vector2 position = GameManager.instance.Player.MainCamera.WorldToScreenPoint(DirectionTarget.transform.position);

                    Vector3 _origin = new Vector3(0, GameManager.instance.Player.MainCamera.transform.position.y, 0);
                    Vector3 _destination = new Vector3(0, DirectionTarget.transform.position.y, 0);                                      

                    Vector3 angle = GameManager.instance.Player.MainCamera.transform.eulerAngles;
                    float x = angle.x;
                    float y = angle.y;
                    float z = angle.z;

                    if (Vector3.Dot(transform.up, Vector3.up) >= 0f)
                    {
                        if (angle.x >= 0f && angle.x <= 90f)
                        {
                            x = angle.x;
                        }
                        if (angle.x >= 270f && angle.x <= 360f)
                        {
                            x = angle.x - 360f;
                        }
                    }
                    if (Vector3.Dot(transform.up, Vector3.up) < 0f)
                    {
                        if (angle.x >= 0f && angle.x <= 90f)
                        {
                            x = 180 - angle.x;
                        }
                        if (angle.x >= 270f && angle.x <= 360f)
                        {
                            x = 180 - angle.x;
                        }
                    }

                    if (angle.y > 180)
                    {
                        y = angle.y - 360f;
                    }

                    if (angle.z > 180)
                    {
                        z = angle.z - 360f;
                    }

                    //Debug.Log(angle + " :::: " + Mathf.Round(x) + " , " + Mathf.Round(y) + " , " + Mathf.Round(z));

                    position.y -= Screen.currentResolution.height / 180 * x;

                    DirectionImage.position = position;
                }
                else DirectionImage.gameObject.SetActive(false);
            } 
        }
    }
}
