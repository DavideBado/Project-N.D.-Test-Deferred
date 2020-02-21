using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using Cinemachine;
using System.Linq;

public class PlayerMovController : MonoBehaviour
{
    public Capsulo GoldenlEgg = null;
    int currentSpotCameraIndex = 0;
    public List<CamSpot> camSpots = new List<CamSpot>();
    public Camera SpotCamera;
    public RawImage SpotCameraScreen;
    public Rigidbody rb;
    public float currentSpeed;
    public float walkSpeed;
    public float crouchingSpeed;
    public float runningSpeed;
    public float rotationSpeed;

    public CinemachineFreeLook freeLookCamera;
    public CinemachineVirtualCamera ResetCamera;
    //public KeyCode interact;
    //public KeyCode crouch;
    //public KeyCode run;

    //public KeyCode NextSpotCam;
    //public KeyCode PrevSpotCam;

    Collision Wall;
    public LayerMask WallMask;
    public LayerMask DroneMask;
    [HideInInspector]
    public bool isCrouching = false;
    bool isRunning = false;
    [HideInInspector]
    public bool isHiding = false;

    public bool haveTheKey = false;

    //List<Gate> gates = new List<Gate>();
    public string OpenTheGateTrigger;

    Vector3 lastPosition;

    public NoiseController Noise;
    public float walkDimensionMod;
    public float runDimensionMod;
    public float walkDuration;
    public float runDuration;
    public Transform movementTransform;

    [HideInInspector]
    public float GraphSpeed;
    public CameraMovement m_camera;

    public Vector3 ResetPosition;

    public bool InputActive = true;

    public Camera MainCamera;
        
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentSpeed = walkSpeed;
        ChangeCamSpot(0);
        //gates = FindObjectsOfType<Gate>().ToList();
        // if (camSpots.Count == 0) SpotCameraScreen.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.instance.InCommandsScreen)
        {
            CameraReset();
            AxisDownCheck();
            //if (camSpots.Count == 0) SpotCameraScreen.enabled = false;
            if (InputActive)
            {
                DetectHidingPoint();
                if (!isHiding)
                {
                    float translationVertical = Input.GetAxis("Vertical") * currentSpeed;
                    float HorizontalTranslation = Input.GetAxis("Horizontal") * currentSpeed;


                    GraphSpeed = translationVertical;

                    translationVertical *= Time.deltaTime;
                    HorizontalTranslation *= Time.deltaTime;

                    //MoveToCameraForward();
                    movementTransform.position = Camera.main.transform.position;
                    movementTransform.rotation = Camera.main.transform.rotation;
                    movementTransform.eulerAngles = new Vector3(0, movementTransform.eulerAngles.y, movementTransform.eulerAngles.z);

                    if (Input.GetAxis("Vertical") != 0)
                        transform.rotation = movementTransform.rotation;

                    if (translationVertical > 0)
                    {
                        if (!Physics.Raycast(new Vector3(transform.position.x, 0.2f, transform.position.z), transform.forward, 0.5f, WallMask)) transform.Translate(new Vector3(0, 0, translationVertical), movementTransform);
                    }
                    else if (translationVertical < 0) if (!Physics.Raycast(new Vector3(transform.position.x, 0.2f, transform.position.z), -transform.forward, 0.5f, WallMask))
                        {
                            transform.Translate(new Vector3(0, 0, translationVertical), movementTransform);
                            Debug.DrawLine(transform.position, -transform.forward, Color.red, 1);
                        }

                    if (HorizontalTranslation > 0)
                    {
                        if (!Physics.Raycast(new Vector3(transform.position.x, 0.2f, transform.position.z), transform.right, 0.5f, WallMask)) transform.Translate(new Vector3(HorizontalTranslation, 0, 0), movementTransform);
                    }
                    else if (HorizontalTranslation < 0) if (!Physics.Raycast(new Vector3(transform.position.x, 0.2f, transform.position.z), -transform.right, 0.5f, WallMask))
                        {
                            transform.Translate(new Vector3(HorizontalTranslation, 0, 0), movementTransform);
                            Debug.DrawLine(transform.position, -transform.forward, Color.red, 1);
                        }


                    //transform.Rotate(0, rotation, 0);

                    Crouch();
                    Run();

                    if (currentSpeed == walkSpeed && Input.GetAxis("Vertical") != 0)
                    {
                        Noise.MakeNoiseDelegate(walkDimensionMod, walkDuration, NoiseController.NoiseType.Walk);
                    }
                    if (currentSpeed == runningSpeed && Input.GetAxis("Vertical") != 0)
                    {
                        Noise.MakeNoiseDelegate(runDimensionMod, runDuration, NoiseController.NoiseType.Run);
                    }

                    if (Input.GetButtonDown("Interact"))
                        if (haveTheKey)
                        {
                            Debug.DrawLine(transform.position, transform.position + transform.forward, Color.red, 1.5f);
                            m_axisDown = true;
                            RaycastHit hit;
                            if (Physics.Raycast(transform.position, transform.forward, out hit, 1.5f))
                            {
                                if (hit.transform.GetComponent<Gate>() != null)
                                {
                                    //for (int i = 0; i < gates.Count; i++)
                                    //{
                                    //    gates[i].GetComponent<Animator>().SetTrigger(OpenTheGateTrigger);
                                    //}

                                    hit.transform.GetComponent<Gate>().GateAnimator.SetTrigger(OpenTheGateTrigger);

                                    // haveTheKey = false;

                                    GameManager.instance.UI_Manager.KeyIcon.SetActive(false);
                                }
                            }
                        }
                }
            }

            if (Input.GetAxisRaw("NextSpotCam") != 0 && !m_axisDown)
            {
                m_axisDown = true;
                currentSpotCameraIndex++;
                if (currentSpotCameraIndex >= camSpots.Count) currentSpotCameraIndex = 0;

                ChangeCamSpot(currentSpotCameraIndex);
            }
            if (Input.GetAxisRaw("PrevSpotCam") != 0 && !m_axisDown)
            {
                m_axisDown = true;
                currentSpotCameraIndex--;
                if (currentSpotCameraIndex < 0) currentSpotCameraIndex = camSpots.Count - 1;

                ChangeCamSpot(currentSpotCameraIndex);
            }

        }
    }
    bool m_axisDown = false;
    void Crouch()
    {
        if (Input.GetAxisRaw("Crouch") != 0 && isCrouching == false && isHiding == false && !m_axisDown)
        {
            m_axisDown = true;
            currentSpeed = crouchingSpeed;
            isCrouching = true;
        }
        else if (Input.GetAxisRaw("Crouch") != 0 && isCrouching == true && isHiding == false && !m_axisDown)
        {
            m_axisDown = true;
            currentSpeed = walkSpeed;
            isCrouching = false;
        }
    }

    void Run()
    {
        if (Input.GetAxisRaw("Run") != 0 && isRunning == false && isHiding == false && !m_axisDown)
        {
            m_axisDown = true;
            currentSpeed = runningSpeed;
            isCrouching = false;
            isRunning = true;
        }
        else if (Input.GetAxisRaw("Run") != 0 && isRunning == true && isHiding == false && !m_axisDown)
        {
            m_axisDown = true;
            currentSpeed = walkSpeed;
            isCrouching = false;
            isRunning = false;
        }
    }

    float pezzahidingSpeed = 0;
    public MenuSelector MenuSelector;
    public float HidingSpotMaxDistance = 1.5f;
    private void DetectHidingPoint()
    {
        if (Input.GetAxisRaw("Interact") != 0 )
            if ( /*!MenuSelector.InMapView && */!m_axisDown)
        {
            m_axisDown = true;
            if (isHiding == false)
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position, transform.forward, out hit, HidingSpotMaxDistance, DroneMask))
                {
                    if (hit.collider.gameObject.tag == "HidingSpot")
                    {
                        PezzaLampoHidingPoint(false);
                        lastPosition = transform.position;
                        transform.position = hit.transform.position;
                        isHiding = true;
                        pezzahidingSpeed = currentSpeed;
                        currentSpeed = 0;
                    }
                }
            }
            else if (isHiding == true)
            {
                transform.position = lastPosition;
                PezzaLampoHidingPoint(true);
                isHiding = false;
                currentSpeed = pezzahidingSpeed;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == WallMask)
        {
            Wall = collision;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (Wall == collision)
        {
            Wall = null;
        }
    }

    public GameObject Graphics;
    public CapsuleCollider Collider;
    public NavMeshObstacle ObstacleNav;


    public void PezzaLampoHidingPoint(bool x)
    {
        Graphics.SetActive(x);
        Collider.enabled = x;
        rb.useGravity = x;
        if (GameManager.instance.OnExePhase) ObstacleNav.enabled = x;
    }

    public void TurnOnOffThePlayer(bool x)
    {
        SpotCameraScreen.enabled = x;
        Graphics.SetActive(x);
        InputActive = x;
    }

    public GameObject debugSphere;
    public void MoveToCameraForward()
    {
        if (Input.GetAxis("Vertical") != 0)
        {
            transform.LookAt(m_camera.point);
            var rot = transform.rotation;
            rot.x -= rot.x;
            rot.z -= rot.z;
            transform.rotation = rot;
        }
    }

    void ChangeCamSpot(int _index)
    {
        if (camSpots.Count > 0)
        {
            for (int i = 0; i < camSpots.Count; i++)
            {
                camSpots[i].GetComponent<CinemachineVirtualCamera>().Priority = 0;
            }
            if (camSpots.Count > _index) camSpots[_index].GetComponent<CinemachineVirtualCamera>().Priority = 50;

        }
    }

    private void AxisDownCheck()
    {
        if(Input.GetAxisRaw("Interact") == 0 && Input.GetAxisRaw("NextSpotCam") == 0 && Input.GetAxisRaw("PrevSpotCam") == 0 && Input.GetAxisRaw("Crouch") == 0 && Input.GetAxisRaw("Run") == 0)
        m_axisDown = false;
    }
       
    private void CameraReset()
    {
        if(Input.GetButton("ResetCamera"))
        {
            ResetCamera.enabled = true;
            freeLookCamera.enabled = false;
            StartCoroutine("Wait");          
        }
    }

    IEnumerator Wait()
    {
        yield return 0;
        freeLookCamera.enabled = true;
        ResetCamera.enabled = false;
    }
}