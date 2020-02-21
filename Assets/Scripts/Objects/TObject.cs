using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TObject : MonoBehaviour
{
    public ParabolaController parabolaController;
    public NoiseController NoiseController;
    public SphereCollider MyCollider;
    public float NoiseAreaMod, NoiseDuration;
    public GameObject Player;
    public ParabolaGraphic Graphic;
    bool onUpgrade = false;
    public bool onAir = false;
    public MeshRenderer MyRenderer;
    public bool CanTObj;
    public LayerMask PlayerLayer;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxisRaw("ThrowObject") != 0)
        {
            if (!onAir && CanTObj)
            {
                CanTObj = false;
                onAir = true;
                parabolaController.FollowParabola();
                NoiseController.Reset();
               // MyCollider.enabled = true;
                MyRenderer.enabled = true;
                onUpgrade = false;
                Graphic.lineRenderer.enabled = false;
            }
        }
        if (onAir)
        {
            RaycastHit[] collisions = Physics.SphereCastAll(transform.position, MyCollider.radius, transform.forward, 1f, PlayerLayer);
            for (int i = 0; i < collisions.Length; i++)
            {
                if (collisions[i].transform.tag != "OutMap" && collisions[i].transform.tag != "Player")
                {
                    onUpgrade = true;
                    onAir = false;
                    MyRenderer.enabled = false;
                    NoiseController.MakeNoiseDelegate(NoiseAreaMod, NoiseDuration, NoiseController.NoiseType.Object);
                }
                else
                {
                    //  MyCollider.enabled = false;
                    onAir = false;
                }
            } 
            
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject != Player && other.transform.parent != Player.transform && !onUpgrade)
        {
            if (other.tag != "OutMap")
            {
                onUpgrade = true;
                onAir = false;
                MyRenderer.enabled = false;
                NoiseController.MakeNoiseDelegate(NoiseAreaMod, NoiseDuration, NoiseController.NoiseType.Object);
            }
            else
            {
                MyCollider.enabled = false;
                onAir = false;
            }
        }
    }
}
