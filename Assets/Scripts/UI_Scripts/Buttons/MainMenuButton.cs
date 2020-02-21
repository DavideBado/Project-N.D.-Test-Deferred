using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(MainMenuButtons_Data))]
public class MainMenuButton : Button
{
    MainMenuButtons_Data _data;

    protected override void Awake()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        _data = GetComponent<MainMenuButtons_Data>();    
    }

    public override void OnSelect(BaseEventData eventData)
    {
        image.sprite = _data.ImageSelected.sprite;
        image.rectTransform.sizeDelta = _data.ImageSelected.rectTransform.sizeDelta;
        _data.Text.fontSize = _data.SelectedTxtSize;
        base.OnSelect(eventData);
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        image.sprite = _data.ImageSelected.sprite;
        image.rectTransform.sizeDelta = _data.ImageSelected.rectTransform.sizeDelta;
        _data.Text.fontSize = _data.SelectedTxtSize;
        base.OnPointerEnter(eventData);
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        image.sprite = _data.ImageBase.sprite;
        image.rectTransform.sizeDelta = _data.ImageBase.rectTransform.sizeDelta;
        _data.Text.fontSize = _data.BaseTxtSize;
        base.OnPointerExit(eventData);
    }

    public override void OnDeselect(BaseEventData eventData)
    {
        image.sprite = _data.ImageBase.sprite;
        image.rectTransform.sizeDelta = _data.ImageBase.rectTransform.sizeDelta;
        _data.Text.fontSize = _data.BaseTxtSize;
        base.OnDeselect(eventData);
    }
}
