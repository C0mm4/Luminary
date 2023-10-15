using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UICloseButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public Menu menu;
    public List<Sprite> sprites = new List<Sprite>();
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
            GameManager.Instance.uiManager.endMenu();
        }
    }
    public void inHandler()
    {
        menu.currentMenu = -1;
        img.sprite = sprites[1];
    }

    public void outHandler()
    {
        img.sprite = sprites[0];
    }
}
