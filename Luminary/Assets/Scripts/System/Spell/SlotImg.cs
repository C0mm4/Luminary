using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotImg : MonoBehaviour
{
    [SerializeField]
    public Image img;
    public bool isRes = false;
    

    public void setImg(Sprite spr)
    {
        isRes = true;
        img.sprite = spr;
        img.color = new Color(1, 1, 1, 1);

    }

    public void deSetImg()
    {
        isRes = false;
        img.color = new Color(1, 1, 1, 0);
    }

    public void updateImg()
    {
        if(isRes)
        {
            img.color = new Color(1, 1, 1, 1);
        }
        else
        {
            img.color = new Color(1, 1, 1, 0);
        }
    }

}
