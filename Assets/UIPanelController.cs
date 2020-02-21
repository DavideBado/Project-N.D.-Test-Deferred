using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIPanelController : MonoBehaviour
{
    public Button FirstButton;
    GameObject oldSettingEventSystem;
    EventSystem eventSystem;
    public bool CursorEnableIn;
    public CursorLockMode CursorLockModeIn;
    public bool CursorEnableOut;
    public CursorLockMode CursorLockModeOut;
    private void OnEnable()
    {
        eventSystem = FindObjectOfType<EventSystem>();
        oldSettingEventSystem = eventSystem.firstSelectedGameObject;
        eventSystem.firstSelectedGameObject = FirstButton.gameObject;
        eventSystem.enabled = false;
        eventSystem.enabled = true;
        Cursor.visible = CursorEnableIn;
        Cursor.lockState = CursorLockModeIn;
    }
    private void OnDisable()
    {
        eventSystem.firstSelectedGameObject = oldSettingEventSystem;
        Cursor.visible = CursorEnableOut;
        Cursor.lockState = CursorLockModeOut;
    }
}
