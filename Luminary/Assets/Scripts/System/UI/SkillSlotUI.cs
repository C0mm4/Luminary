using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillSlotUI : MonoBehaviour
{
    RectTransform rt;
    SkillSlot[] slots;

    public Image[] img;
    public Image[] fillImg;

    public void init()
    {
        rt = GetComponent<RectTransform>();
        rt.transform.SetParent(GameManager.Instance.canvas.transform);
        rt.transform.localScale = Vector3.one;
        rt.transform.localPosition = Vector3.zero;

        imgInit();

        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            slots = GameObject.FindGameObjectWithTag("Player").GetComponent<Behavior>().skillSlots;
            setImg();
        }

    }

    public void Update()
    {
        if (slots == null)
        {
            if (GameObject.FindGameObjectWithTag("Player") != null)
            {
                slots = GameObject.FindGameObjectWithTag("Player").GetComponent<Behavior>().skillSlots;
                setImg();
            }
        }
        else
        {
            checkCDs();
        }

    }

    public void setImg()
    {
        if (slots != null)
        {
            for (int i = 0; i < 5; i++)
            {
                if (slots[i].isSet())
                {
                    img[i].sprite = slots[i].getSpell().getSpr();
                    img[i].color = new Color(1f, 1f, 1f, 1f);
                }
                else
                {
                    img[i].color = new Color(1f, 1f, 1f, 0f);
                }
            }
        }
    }

    public void imgInit()
    {
        for (int i = 0; i < 5; i++) 
        {
            img[i].color = new Color(1f, 1f, 1f, 0f);
            fillImg[i].color = new Color(0f, 0f, 0f, 0f);
        }
        
    }


    public void checkCDs()
    {
        if (slots != null)
        {
            for (int i = 0; i < 5; i++)
            {
                if (slots[i].isSet())
                {
                    if (slots[i].getSpell().isCool)
                    {
                        fillImg[i].color = new Color(0f, 0f, 0f, 0.8f);
                        fillImg[i].fillAmount = slots[i].getSpell().ct / slots[i].getCD();
                        
                    }
                    else
                    {
                        fillImg[i].color = new Color(0f, 0f, 0f, 0f);
                        fillImg[i].fillAmount = 0;
                    }
                }
            }
        }
    }
}
