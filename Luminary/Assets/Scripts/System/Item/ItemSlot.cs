using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour, IDragHandler, IEndDragHandler, IPointerClickHandler
{
    [SerializeField]
    private Image image;

    [SerializeField]
    public int index;

    [SerializeField]
    private Item _item;
    public Item item
    {
        get { return _item; }
        set
        {
            _item = value;
            if (_item != null)
            {
                image.sprite = item.data.itemImage;
                image.color = new Color(1, 1, 1, 1);
            }
            else
            {
                image.color = new Color(1, 1, 1, 0);
            }
        }
    }

    public void Awake()
    {
        image.color = new Color(1, 1, 1, 0);
    }


    public void OnDrag(PointerEventData eventData)
    {
        if(item != null)
        {
            if (!GameManager.inputManager.isDragging)
            {

                GameManager.Instance.uiManager.invUI.GetComponent<Inventory>().clickIndex = index;
                GameManager.Instance.uiManager.invUI.GetComponent<Inventory>().tmpitem = GameManager.Resource.Instantiate("UI/TmpItem");
                GameObject tmpobj = GameManager.Instance.uiManager.invUI.GetComponent<Inventory>().tmpitem;
                tmpobj.GetComponent<SpriteRenderer>().sprite = image.sprite;
                tmpobj.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.7f);
                tmpobj.transform.position = new Vector3(GameManager.inputManager.mouseWorldPos.x, GameManager.inputManager.mouseWorldPos.y, -1);
                tmpobj.transform.localScale = new Vector2(2.5f, 2.5f);

                GameManager.inputManager.isDragging = true;
            }
            
        }
    }

    public virtual void OnEndDrag(PointerEventData eventData)
    {
        // 드래그를 끝낼 때 호출되는 함수
        if (GameManager.Instance.uiManager.invUI.GetComponent<Inventory>().clickIndex != -1)
        {
            if (eventData.pointerEnter != null)
            {
                Equip equip = eventData.pointerEnter.GetComponent<Equip>();
                if(equip != null)
                {
                    if(equip != null && equip != this)
                    {
                        GameManager.player.GetComponent<Player>().Equip(index, GameManager.player.GetComponent<Player>().status.inventory[index].item, equip.index);

                    }
                }
                else
                {
                    ItemSlot targetSlot = eventData.pointerEnter.GetComponent<ItemSlot>();

                    if (targetSlot != null && targetSlot != this)
                    {
                        GameManager.player.GetComponent<Player>().ItemSwap(index, targetSlot.index);
                    }
                }

            }
        }
        GameManager.Resource.Destroy(GameManager.Instance.uiManager.invUI.GetComponent<Inventory>().tmpitem);
        GameManager.Instance.uiManager.invenFrest();
        GameManager.Instance.uiManager.invUI.GetComponent<Inventory>().clickIndex = -1;
        GameManager.inputManager.isDragging = false;
    }


    public virtual void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            GameManager.player.GetComponent<Player>().Equip(index, item);
        }

    }
}
