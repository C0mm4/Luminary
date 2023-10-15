using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class ShopInven : BarInven
{
    public GameObject hoveringUI;
    public int tmp2index;

    public override void Start()
    {
        base.Start();

        if(menusize == 0)
        {
            if(npc.takeALook.All(b => b == false))
            {
                closeButton.GetComponent<UICloseButton>().inHandler();
            }
            else
            {
                currentMenu = 18;
            }
        }
        tmpIndex = 0;
        tmp2index = 18;
    }
    public override void InputAction()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            if(currentMenu == -1 || currentMenu == 99)
            {
                if(currentMenu == -1)
                {
                    closeButton.GetComponent<UICloseButton>().outHandler();
                    currentMenu = tmpIndex;
                }
                else
                {
                    currentMenu = tmp2index;
                    confirmButton.GetComponent<ConfirmButton>().outHandler();
                    
                }
                hoverHandler(currentMenu);
            }
            else
            {
                if(currentMenu < 18)
                {
                    outHoverHandler(currentMenu);
                    currentMenu++;
                    if(currentMenu >= 18)
                    {
                        currentMenu = 0;
                    }
                    hoverHandler(currentMenu);
                }
                else if(currentMenu >= 18 && currentMenu < 24)
                {
                    outHoverHandler(currentMenu);
                    currentMenu++;
                    if(currentMenu >= 24)
                    {
                        currentMenu = 18;
                    }
                    hoverHandler(currentMenu);
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            if(currentMenu == -1 || currentMenu == 99)
            {
                if (currentMenu == -1)
                {
                    closeButton.GetComponent<UICloseButton>().outHandler();
                    currentMenu = tmpIndex;
                }
                else
                {
                    currentMenu = tmp2index;
                    confirmButton.GetComponent<ConfirmButton>().outHandler();
                }
                hoverHandler(currentMenu);
            }
            else
            {
                if(currentMenu < 18)
                {
                    outHoverHandler(currentMenu);
                    currentMenu--;
                    if(currentMenu < 0)
                    {
                        currentMenu = 17;
                    }
                    hoverHandler(currentMenu);
                }
                else if(currentMenu >= 18 && currentMenu < 24)
                {
                    outHoverHandler(currentMenu);
                    currentMenu--;
                    if(currentMenu < 18)
                    {
                        currentMenu = 23;
                    }
                    hoverHandler(currentMenu);
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            if(currentMenu == 99)
            {
                confirmButton.GetComponent<ConfirmButton>().outHandler();
                if (npc.takeALook.All(b => b == false))
                {
                    if(menusize != 0)
                    {
                        currentMenu = tmpIndex;
                        hoverHandler(currentMenu);
                    }
                    else
                    {
                        closeButton.GetComponent<UICloseButton>().inHandler();
                    }
                }
                else
                {
                    currentMenu = tmp2index;
                    hoverHandler(currentMenu);
                }
            }
            else if(currentMenu >= 18 && currentMenu < 24)
            {
                outHoverHandler(currentMenu);
                tmp2index = currentMenu;
                if(menusize != 0)
                {
                    currentMenu = tmpIndex;
                    hoverHandler(currentMenu);
                }
                else
                {
                    closeButton.GetComponent<UICloseButton>().inHandler();
                }
            }
            else if(currentMenu != -1)
            {
                outHoverHandler(currentMenu);
                tmpIndex = currentMenu;
                closeButton.GetComponent<UICloseButton>().inHandler();
            }
        }

        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            if(currentMenu == -1)
            {
                closeButton.GetComponent<UICloseButton>().outHandler();
                if (menusize != 0)
                {
                    currentMenu = tmpIndex;
                    hoverHandler(currentMenu);
                }
                else
                {
                    if (npc.takeALook.All(b => b == false))
                    {
                        confirmButton.GetComponent<ConfirmButton>().inHandler();
                    }
                    else
                    {
                        currentMenu = tmp2index;
                    }
                }
            }
            else if (currentMenu < 18)
            {
                outHoverHandler(currentMenu);
                tmpIndex = currentMenu;
                if (npc.takeALook.All(b => b == false))
                {
                    confirmButton.GetComponent<ConfirmButton>().inHandler();
                }
                else
                {
                    currentMenu = tmp2index;
                    hoverHandler(currentMenu);
                }
            }
            else if(currentMenu != 99)
            {
                outHoverHandler(currentMenu);
                tmp2index = currentMenu;
                confirmButton.GetComponent<ConfirmButton>().inHandler();
            }
        }

        if (Input.GetKeyDown(PlayerDataManager.keySetting.InteractionKey))
        {
            if(currentMenu == -1)
            {
                GameManager.Instance.uiManager.endMenu();
            }
            else if(currentMenu == 99)
            {
                ConfirmAction();
            }
            else
            {
                if (currentMenu != selectIndex)
                {
                    clickHandler(currentMenu);
                }
                else
                {
                    ConfirmAction();
                }
            }
        }
    }

    public override void hide()
    {
        base.hide();

        GameManager.Resource.Destroy(hoveringUI);
    }

    public override void clickHandler(int index)
    {
        int tmp = selectIndex;
        selectIndex = index;
        if (tmp >= 0 && tmp < slots.Count)
        {
            slots[tmp].GetComponent<ItemSlotBar>().outCursor();

        }
        slots[selectIndex].GetComponent<ItemSlotBar>().Select();

        // Set ConfirmButton Able
        confirmButton.GetComponent<ConfirmButton>().setAble();
        if(selectIndex >= 0 && selectIndex < 18)
        {
            confirmButton.GetComponentInChildren<TMP_Text>().text = "Sell";
        }
        else if(selectIndex >= 18 && selectIndex < 24)
        {
            confirmButton.GetComponentInChildren<TMP_Text>().text = "Buy";
        }
    }

    public override void hoverHandler(int index)
    {
        slots[index].GetComponent<ItemSlotBar>().onCursor();
        if (slots[index].GetComponent<ItemSlotBar>().Item != null)
        {
            hoveringUI = GameManager.Resource.Instantiate("UI/ItemHoveringUI");
            hoveringUI.GetComponent<ItemHoveringUI>().setData(slots[index].GetComponent<ItemSlotBar>().Item);
            hoveringUI.GetComponent<RectTransform>().localPosition = GameManager.cameraManager.camera.WorldToScreenPoint(slots[index].transform.position) - new Vector3(Screen.width / 2, Screen.height / 2, 0) + new Vector3(260, 0, 0);

        }
    }


    public override void outHoverHandler(int index)
    {
        slots[index].GetComponent<ItemSlotBar>().outCursor();
        GameManager.Resource.Destroy(hoveringUI);
    }

    public override void ConfirmAction()
    {
        if(selectIndex >= 0 && selectIndex < 18)
        {
            if (slots[selectIndex].GetComponent<ItemSlotBar>().originSlot <= 5)
            {
                GameManager.Instance.uiManager.textUI("Items in equipment cannot be sold.");
            }
            else
            {
                tmpIndex = 0;
                if (slots[selectIndex].GetComponent<ItemSlotBar>().Item != null)
                {
                    player.status.gold += slots[selectIndex].GetComponent<ItemSlotBar>().Item.data.sellGold;
                    player.ItemDelete(slots[selectIndex].GetComponent<ItemSlotBar>().originSlot - 6);
                    int tmp = selectIndex;
                    selectIndex = -1;
                    outHoverHandler(tmp);
                }
            }
            invenFresh();
        }
        else if(selectIndex >= 18 && selectIndex < 24)
        {
            if(player.status.gold >= slots[selectIndex].GetComponent<ItemSlotBar>().Item.data.purchaseGold)
            {
                int npcindex = selectIndex - 18;
                if (npc.takeALook[npcindex])
                {
                    player.status.gold -= slots[selectIndex].GetComponent<ItemSlotBar>().Item.data.purchaseGold;
                    player.ItemAdd(slots[selectIndex].GetComponent<ItemSlotBar>().Item);
                    npc.takeALook[npcindex] = false;
                    slots[selectIndex].GetComponent<ItemSlotBar>().outCursor();
                }
                else
                {
                    GameManager.Instance.uiManager.textUI("This item has already been sold.");
                }
            }
            else
            {
                GameManager.Instance.uiManager.textUI("Not Enough Golds.");
            }
            invenFresh();
        }
    }
}
