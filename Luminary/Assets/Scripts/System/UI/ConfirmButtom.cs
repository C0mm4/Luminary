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
        menu.currentMenu = 99;
        img.sprite = sprites[2];
    }

    public void OnPointerExit(PointerEventData eventData)
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

    public void Confirm()
    {
        if (menu != null)
        {
            menu.ConfirmAction();
        }
    }

    public void setAble()
    {
        isAble = true;
        img.sprite = sprites[1];
    }
}
