using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Canvas MaiCanvas;
    public TMP_Text PhaseTxt;
    public GameObject GameOverPanel;
    public GameObject Win;
    public GameObject PlanningUI;
    public GameObject PopupEscapeSpwan, PopupHidingCam, PopupUltimate;
    public GameObject ExeUI;

    public GameObject ReloadButtonWin;
    public GameObject MainMenuButtonWin;
    public TMP_Text WinTxt;

    public DirectionSpriteController directionSpriteController;
    public RectTransform KeyDirection, TreasureDirection, EscapeDirection;
    public TMP_Text KeyDistance, TreasureDistance, EscapeDistance;


    public GameObject KeyIcon, TreasureIcon;

    public float TxtFadeSpeed;
        
    bool InWin = false;

    public Action StartWinFade;

    public RawImage SpotCameraScreen;
    public GameObject SpotCameraScreenGObj;

    public GameObject CommandsScreen;

    public Text VersionTxt;
    private void OnEnable()
    {
        VersionTxt.text = "v" + Application.version +"   ";
        StartWinFade += WinInUpdate;
        SceneManager.sceneLoaded += ResetUI;
    }

    private void OnDisable()
    {
       
        SceneManager.sceneLoaded -= ResetUI;
    }
  
    private void WinInUpdate()
    {
        InWin = true;
    }

    private void WinFade()
    {
        if (WinTxt.color.a > 0)
        {
            WinTxt.color = new Color(WinTxt.color.r, WinTxt.color.b, WinTxt.color.g, WinTxt.color.a - (TxtFadeSpeed * Time.deltaTime));
        }
        else
        {
            ReloadButtonWin.SetActive(true);
            MainMenuButtonWin.SetActive(true);
            InWin = false;
        }
    }

    private void Update()
    {
        if (InWin)
        {
            WinFade();
        }

        SetDirection();
        OpenCommandsScreen();
    }


    void ResetUI(Scene scene, LoadSceneMode loadSceneMode)
    {
        if (scene.buildIndex == 1)
        {  
            WinTxt.color = new Color(WinTxt.color.r, WinTxt.color.b, WinTxt.color.g, 1f);
            ReloadButtonWin.SetActive(false);
            MainMenuButtonWin.SetActive(false);
        }
    }

    public void ClosePopup(GameObject _popup)
    {
        GameManager.instance.Drone.SetupPopupsCamera(false);
        GameManager.instance.Drone.enabled = true;
        GameManager.instance.Drone.DroneCamera.enabled = true;
        _popup.SetActive(false);
    }

    public void SetDirection()
    {
        if (GameManager.instance.OnExePhase)
        {
            if (!GameManager.instance.Player.haveTheKey && GameManager.instance.Key != null)
            {
                TreasureDirection.gameObject.SetActive(false);
                EscapeDirection.gameObject.SetActive(false);

                directionSpriteController.DirectionImage = KeyDirection;
                directionSpriteController.DirectionTarget = GameManager.instance.Key.DirectionTarget;
                directionSpriteController.distanceTxt = KeyDistance;
            }
            else if (GameManager.instance.Player.haveTheKey || GameManager.instance.Key == null)
            {
                if (GameManager.instance.Player.GoldenlEgg == null)
                {
                    KeyDirection.gameObject.SetActive(false);
                    EscapeDirection.gameObject.SetActive(false);

                    directionSpriteController.DirectionImage = TreasureDirection;
                    directionSpriteController.DirectionTarget = GameManager.instance.Treasure.DirectionTarget;
                    directionSpriteController.distanceTxt = TreasureDistance;
                }
                else
                {
                    KeyDirection.gameObject.SetActive(false);
                    TreasureDirection.gameObject.SetActive(false);

                    directionSpriteController.DirectionImage = EscapeDirection;
                    directionSpriteController.DirectionTarget = GameManager.instance.CurrentEscapeSpot.DirectionTarget;
                    directionSpriteController.distanceTxt = EscapeDistance;
                }
            }
        }
    }

    private void OpenCommandsScreen()
    {
        if (Input.GetButtonDown("CommandsScreen"))
        {
            if (PopupsOff()) if (CommandsScreen) if (!CommandsScreen.activeSelf) CommandsScreen.SetActive(true);
        }
    }

    private bool PopupsOff()
    {
        return !PopupEscapeSpwan.activeSelf && !PopupHidingCam.activeSelf && !PopupUltimate.activeSelf;
    }
}
