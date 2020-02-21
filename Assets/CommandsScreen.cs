using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CommandsScreen : MonoBehaviour
{
    public Button FirstButton;
    GameObject oldSettingEventSystem;
    EventSystem eventSystem;
    bool PrevCursorV = false;
    CursorLockMode PrevCursorLock;

    private void OnEnable()
    {
        GameManager.instance.InCommandsScreen = true;


        if (GameManager.instance.Drone) GameManager.instance.Drone.DroneCamera.enabled = false;
        if (GameManager.instance.Player) GameManager.instance.Player.freeLookCamera.enabled = false;

        eventSystem = FindObjectOfType<EventSystem>();
        oldSettingEventSystem = eventSystem.firstSelectedGameObject;
        eventSystem.firstSelectedGameObject = FirstButton.gameObject;

        PrevCursorV = Cursor.visible;
        PrevCursorLock = Cursor.lockState;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        Time.timeScale = 0;
    }

    private void Update()
    {
        if (eventSystem.currentSelectedGameObject != FirstButton)
        {
            if (Input.GetButtonDown("Submit") || Input.GetButtonDown("CommandsScreen")) gameObject.SetActive(false);
        }
    }
    private void OnDisable()
    {
        eventSystem.firstSelectedGameObject = oldSettingEventSystem;
        if (GameManager.instance.OnPlanPhase) if (GameManager.instance.Drone) GameManager.instance.Drone.DroneCamera.enabled = true;
        if (GameManager.instance.OnExePhase) if (GameManager.instance.Player) GameManager.instance.Player.freeLookCamera.enabled = true;
        Time.timeScale = 1;

        Cursor.visible = PrevCursorV;
        Cursor.lockState = PrevCursorLock;

        GameManager.instance.InCommandsScreen = false;
    }

    IEnumerator Wait()
    {
        yield return 0;
        eventSystem.gameObject.SetActive(true);
    }
}