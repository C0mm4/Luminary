using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class spellFill : MonoBehaviour
{
    public Image img;

    private float cooltime, timeStart, timeCurrent;
    private bool isRun = false;

    private void Start()
    {
        img.color = new Color(0, 0, 0, 0);
        img.fillAmount = 0;
        Debug.Log(img.type);
        img.type = Image.Type.Filled;
        img.fillMethod = Image.FillMethod.Radial360;
        img.fillOrigin = 0;
        img.fillClockwise = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (isRun)
        {
            checkCoolTime();
        }
    }

    public void setCooltime(float cd)
    {
        cooltime = cd;
        startSpell();
        Debug.Log(img.type);
    }

    public void startSpell()
    {
        timeStart = Time.time;
        img.color = new Color(0, 0, 0, 0.8f);
        isRun = true;
    }

    void checkCoolTime()
    {
        timeCurrent = Time.time - timeStart;
        if(timeCurrent < cooltime)
        {
            img.fillAmount = 0;
        }
        else
        {
            isRun = true;
            img.fillAmount = 0;
            img.color = new Color(0, 0, 0, 0);
        }
    }
}
