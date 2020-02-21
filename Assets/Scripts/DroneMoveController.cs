using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System.Linq;

public class DroneMoveController : MonoBehaviour
{
    //public KeyCode FirstAllSpotPosTypes, SecondAllSpotPosTypes;
    public float speed;
    public LayerMask WallMask;
    public GameObject Pointer;
    public CinemachineFreeLook DroneCamera;
    public List<CellTypeBase> AllSpotPosTypes = new List<CellTypeBase>();
    [HideInInspector]
    public CellTypeBase CurrentIspotType;
    public CellTypeBase CamCelLType;
    public Transform ZminBorder, ZmaxBorder, XminBorder, XmaxBorder;

    public List<CamSpot> camSpots = new List<CamSpot>();
    public List<CamSpotPlan> planCamSpots = new List<CamSpotPlan>();
    float mouseX;
    public float rotationSpeed;

    public PlaceableSpot.PlaceableSpotType CurrentSpotType;
    public List<PlaceableSpot.PlaceableSpotType> MyPossibleSpotTypes = new List<PlaceableSpot.PlaceableSpotType>();
    public int RemHiding, RemCams, RemEsc, RemStart;

    public CinemachineVirtualCamera PopupsCamera;
    private void Start()
    {
        CurrentSpotType = PlaceableSpot.PlaceableSpotType.Hiding;
        if (AllSpotPosTypes.Count > 0) CurrentIspotType = AllSpotPosTypes[0];
    }

    private void Update()
    {
        if (!GameManager.instance.InCommandsScreen)
        {
            CheckInput();
            //CheckCells();
            //SelectCurrentSpotType();
            //ActiveSpot();
            CheckSpots();
        }
    }
    void CheckInput()
    {

        SelectSpotTypeKeyBoard();
        Vector3 VerticalTranslation = Input.GetAxis("Vertical") * speed * Time.deltaTime * transform.forward;
        Vector3 HorizontalTranslation = Input.GetAxis("Horizontal") * speed * Time.deltaTime * transform.right;

        mouseX += Input.GetAxis("Mouse X") * rotationSpeed;
        transform.rotation = Quaternion.Euler(0, mouseX, 0);
        transform.position += VerticalTranslation + HorizontalTranslation;

        if (transform.position.x < XminBorder.position.x) transform.position = new Vector3(XminBorder.position.x, transform.position.y, transform.position.z);
        if (transform.position.x > XmaxBorder.position.x) transform.position = new Vector3(XmaxBorder.position.x, transform.position.y, transform.position.z);
        if (transform.position.z < ZminBorder.position.z) transform.position = new Vector3(transform.position.x, transform.position.y, ZminBorder.position.z);
        if (transform.position.z > ZmaxBorder.position.z) transform.position = new Vector3(transform.position.x, transform.position.y, ZmaxBorder.position.z);
        //VerticalTranslation *= Time.deltaTime;
        //HorizontalTranslation *= Time.deltaTime;

        //if (VerticalTranslation > 0)
        //{
        //    if (!Physics.Raycast(new Vector3(transform.position.x, 0.2f, transform.position.z), transform.forward, 0.5f, WallMask)) transform.Translate(0, 0, VerticalTranslation);
        //}
        //else if (VerticalTranslation < 0) if (!Physics.Raycast(new Vector3(transform.position.x, 0.2f, transform.position.z), -transform.forward, 0.5f, WallMask))
        //    {
        //        transform.Translate(0, 0, VerticalTranslation);
        //        Debug.DrawLine(transform.position, -transform.forward, Color.red, 1);
        //    }

        //if (HorizontalTranslation > 0)
        //{
        //    if (!Physics.Raycast(new Vector3(transform.position.x, 0.2f, transform.position.z), transform.right, 0.5f, WallMask)) transform.Translate(HorizontalTranslation, 0, 0);
        //}
        //else if (HorizontalTranslation < 0) if (!Physics.Raycast(new Vector3(transform.position.x, 0.2f, transform.position.z), -transform.right, 0.5f, WallMask))
        //    {
        //        transform.Translate(HorizontalTranslation, 0, 0);
        //        Debug.DrawLine(transform.position, -transform.forward, Color.red, 1);
        //    }

    }
    RaycastHit Currenthit;
    RaycastHit Oldhit;
    //void CheckCells()
    //{

    //    LayerMask layerMask = ~WallMask;
    //    if (Physics.Raycast(DroneCamera.transform.position, Pointer.transform.position - DroneCamera.transform.position, out Currenthit, 500000f, layerMask))
    //    {
    //        Debug.DrawLine(Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, 10)), Currenthit.point, Color.red, 100f);
    //        if (Currenthit.transform.GetComponent<Cell3D>() != null)
    //        {
    //            Currenthit.transform.GetComponent<MeshRenderer>().material.color = Currenthit.transform.GetComponent<MeshRenderer>().material.color + Color.red;
    //            Oldhit = Currenthit;
    //        }
    //        else if (Oldhit.transform) Oldhit.transform.GetComponent<MeshRenderer>().material.color = Oldhit.transform.GetComponent<MeshRenderer>().material.color - Color.red;
    //    }
    //    else if(Oldhit.transform) Oldhit.transform.GetComponent<MeshRenderer>().material.color = Oldhit.transform.GetComponent<MeshRenderer>().material.color - Color.red;
    //}

    //void ActiveSpot()
    //{
    //    if(Input.GetMouseButtonDown(0))
    //    {
    //        List<CellTypeBase> cellTypeBases = Currenthit.transform.GetComponents<CellTypeBase>().ToList();

    //        for (int i = 0; i < cellTypeBases.Count; i++)
    //        {
    //            cellTypeBases[i].enabled = false;
    //        }

    //        for (int i = 0; i < cellTypeBases.Count; i++)
    //        {
    //            if (cellTypeBases[i].GetType() == CurrentIspotType.GetType())
    //            {
    //                cellTypeBases[i].enabled = true;
    //                if (cellTypeBases[i].GetType() == CamCelLType.GetType())
    //                    Currenthit.transform.GetComponent<MeshRenderer>().material.color = Color.blue;

    //                break;
    //            }
    //        }
    //    }
    //}

    //void SelectCurrentSpotType()
    //{
    //    if (Input.GetKeyDown(FirstAllSpotPosTypes)) CurrentIspotType = AllSpotPosTypes[0];
    //    if (Input.GetKeyDown(SecondAllSpotPosTypes)) CurrentIspotType = AllSpotPosTypes[1];
    //}

    private bool m_isPlacingAxisInUse = false, m_isSelectionAxisInUse = false;
    void CheckSpots()
    {
        LayerMask layerMask = ~WallMask;
        if (Physics.Raycast(DroneCamera.transform.position, Pointer.transform.position - DroneCamera.transform.position, out Currenthit, 500000f, layerMask))
        {
            Debug.DrawLine(Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, 10)), Currenthit.point, Color.red, 100f);
            if (Currenthit.transform.GetComponent<SpotBase>() != null)
            {
                Oldhit = Currenthit;

                if (Currenthit.transform.GetComponent<PlaceableSpot>() != null)
                {
                    if (CheckDronePlacingselection())
                    {
                        SwitchSpotTypes(Currenthit.transform.GetComponent<PlaceableSpot>().SpotType);
                        CurrentSpotType = 0;
                    }
                    else
                    if (Input.GetAxis("DroneSelect") != 0)
                    {
                        SwitchSpotTypes(Currenthit.transform.GetComponent<PlaceableSpot>().SpotType);

                    }
                    else if (Input.GetAxis("DroneRemove") != 0)
                    {
                        switch (Currenthit.transform.GetComponent<PlaceableSpot>().SpotType)
                        {
                            case PlaceableSpot.PlaceableSpotType.EscapePoint:
                                //EscapeSpot _EscapeSpot = Currenthit.transform.GetComponent<EscapeSpot>();
                                //if (GameManager.instance.CurrentEscapeSpot == _EscapeSpot)
                                //{
                                //    GameManager.instance.CurrentEscapeSpot = null;
                                //    _EscapeSpot.Graphics.SetSelectedGraphichs(false);
                                //    RemEsc++;
                                //}
                                break;
                            case PlaceableSpot.PlaceableSpotType.StartinPoint:
                                //SpawnSpot _SpawnSpot = Currenthit.transform.GetComponent<SpawnSpot>();
                                //if (GameManager.instance.CurrentStartSpot == _SpawnSpot)
                                //{
                                //    GameManager.instance.CurrentStartSpot = null;
                                //    _SpawnSpot.Graphics.SetSelectedGraphichs(false);
                                //    RemStart++;
                                //}
                                break;
                            case PlaceableSpot.PlaceableSpotType.Hiding:
                                break;
                            case PlaceableSpot.PlaceableSpotType.Cam:
                                break;
                            case PlaceableSpot.PlaceableSpotType.Multi:
                                ObjectsSpot _objectsSpot = Currenthit.transform.GetComponent<ObjectsSpot>();
                                for (int i = 0; i < _objectsSpot.SpotTypesForMulti.Count; i++)
                                {
                                    //if (_objectsSpot.SpotTypesForMulti[i] == CurrentSpotType)
                                    //{
                                    _objectsSpot.SpotsForMultiExe[i].SetActive(false);
                                    _objectsSpot.Graphics.SetSelectedGraphichs(false);
                                    //}
                                }
                                break;
                            default:
                                break;
                        }
                    }
                }
                else if (Currenthit.transform.GetComponent<InterestSpot>() != null)
                {

                }
            }
            //else if (Oldhit.transform) Oldhit.transform.GetComponent<MeshRenderer>().material.color = Oldhit.transform.GetComponent<MeshRenderer>().material.color - Color.red;
        }
        //else if (Oldhit.transform) Oldhit.transform.GetComponent<MeshRenderer>().material.color = Oldhit.transform.GetComponent<MeshRenderer>().material.color - Color.red;
    }

    private void SwitchSpotTypes(PlaceableSpot.PlaceableSpotType _type)
    {
        switch (_type)
        {
            case PlaceableSpot.PlaceableSpotType.Null:
                break;
            case PlaceableSpot.PlaceableSpotType.EscapePoint:
                if (!m_isSelectionAxisInUse)
                {
                    if (GameManager.instance.CurrentEscapeSpot != null)
                    {
                        GameManager.instance.CurrentEscapeSpot.Graphics.SetSelectedGraphichs(false);
                        GameManager.instance.CurrentEscapeSpot = null;
                        RemEsc++;
                    }
                    if (RemEsc > 0)
                    {
                        EscapeSpot _EscapeSpot = Currenthit.transform.GetComponent<EscapeSpot>();
                        GameManager.instance.CurrentEscapeSpot = _EscapeSpot;
                        _EscapeSpot.Graphics.SetSelectedGraphichs(true);
                        RemEsc--;
                    }
                }
                break;
            case PlaceableSpot.PlaceableSpotType.StartinPoint:
                if (!m_isSelectionAxisInUse)
                {
                    if (GameManager.instance.CurrentStartSpot != null)
                    {
                        GameManager.instance.CurrentStartSpot.Graphics.SetSelectedGraphichs(false);
                        GameManager.instance.CurrentStartSpot = null;
                        RemStart++;
                    }
                    if (RemStart > 0)
                    {
                        SpawnSpot _SpawnSpot = Currenthit.transform.GetComponent<SpawnSpot>();
                        GameManager.instance.CurrentStartSpot = _SpawnSpot;
                        _SpawnSpot.Graphics.SetSelectedGraphichs(true);
                        RemStart--;
                    }
                }
                break;
            case PlaceableSpot.PlaceableSpotType.Hiding:
                //if (RemHiding > 0)
                //{
                //    HidingSpot _HidingSpot = Currenthit.transform.GetComponent<HidingSpot>();
                //    _HidingSpot.Graphics.SetSelectedGraphichs(true);
                //    RemHiding--;
                //}
                break;
            case PlaceableSpot.PlaceableSpotType.Cam:
                //if (RemCams > 0)
                //{
                //    CamSpot _CamSpot = Currenthit.transform.GetComponent<CamSpot>();
                //    _CamSpot.Graphics.SetSelectedGraphichs(true);
                //    RemCams--;
                //}
                break;
            case PlaceableSpot.PlaceableSpotType.Multi:
                if (CurrentSpotType != 0)
                {
                    ObjectsSpot _objectsSpot = Currenthit.transform.GetComponent<ObjectsSpot>();
                    _objectsSpot.Graphics.SetSelectedGraphichs(true);
                    for (int i = 0; i < _objectsSpot.SpotTypesForMulti.Count; i++)
                    {
                        if (_objectsSpot.SpotTypesForMulti[i] == CurrentSpotType)
                        {
                            _objectsSpot.SpotsForMultiExe[i].SetActive(true);
                            _objectsSpot.SpotsForMultiPlan[i].SetActive(true);
                        }
                        else
                        {
                            _objectsSpot.SpotsForMultiExe[i].SetActive(false);
                            _objectsSpot.SpotsForMultiPlan[i].SetActive(false);
                        }
                    }
                }
                break;
            default:
                break;
        }
    }

    private bool CheckDronePlacingselection()
    {
        bool _canPlace = false;
        if (!m_isSelectionAxisInUse)
        {
            if (Input.GetAxisRaw("DronePlaceHiding") != 0)
            {
                m_isSelectionAxisInUse = true;
                CurrentSpotType = PlaceableSpot.PlaceableSpotType.Hiding;
                _canPlace = true;
            }
            else if (Input.GetAxisRaw("DronePlaceCamera") != 0)
            {
                m_isSelectionAxisInUse = true;
                CurrentSpotType = PlaceableSpot.PlaceableSpotType.Cam;
                _canPlace = true;
            }
        }
        else if (Input.GetAxisRaw("DronePlaceHiding") == 0 && Input.GetAxisRaw("DronePlaceCamera") == 0)
        {
            m_isSelectionAxisInUse = false;
        }
        return _canPlace;
    }

    private bool CheckDronePlacingInput()
    {
        return Input.GetAxisRaw("DronePlaceHiding") != 0 || Input.GetAxisRaw("DronePlaceCamera") != 0;
    }

    public void SelectSpotTypeKeyBoard()
    {
        if (Input.GetButtonDown("SelectHiding")) CurrentSpotType = PlaceableSpot.PlaceableSpotType.Hiding;
        else if (Input.GetButtonDown("SelectCam")) CurrentSpotType = PlaceableSpot.PlaceableSpotType.Cam;
    }

    public void SetupPopupsCamera(bool _enabled)
    {
        PopupsCamera.transform.position = Camera.main.transform.position;
        PopupsCamera.transform.rotation = Camera.main.transform.rotation;
        PopupsCamera.enabled = _enabled;
    }
}