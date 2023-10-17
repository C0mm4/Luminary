using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ConfirmButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public Menu menu;
    public bool isAble = false;

    [SerializeField]
    public List<Sprite> sprites;
    public Image img;

    public void OnPointerClick(PointerEventData eventData)
    {
        Confirm();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        inHandler();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        outHandler();
    }

    public void Confirm()
    {
        if (menu != null)
        {
            // when click button or select this button and confirm button push, process confirm Action on target menu
            menu.ConfirmAction();
        }
    }

    // select this button handler
    public void inHandler()
    {
        menu.currentMenu = 99;
        img.sprite = sprites[2];
    }

    // deselect this button handler
    public void outHandler()
    {
        if (isAble)
        {
            img.sprite = sprites[1];
        }
        else
        {
            img.sprite = sprites[0];
        }
    }

    // set sprite confirm is able
    public void setAble()
    {
        isAble = true;
        img.sprite = sprites[1];
    }
}
