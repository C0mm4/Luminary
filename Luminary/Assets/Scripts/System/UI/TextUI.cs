using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TextUI : MonoBehaviour
{
    public string text;

    [SerializeField]
    TMP_Text txt;
    [SerializeField]
    Image img;
    [SerializeField]
    RectTransform rt;

    public float starttime, currenttime;

    
    void Start()
    {
        starttime = Time.time;
        currenttime = Time.time;
        setTxt();

        rt = gameObject.GetComponent<RectTransform>();
        rt.transform.SetParent(GameManager.Instance.canvas.transform);
        rt.transform.localScale = Vector3.one;
        rt.transform.localPosition = new Vector3(0, 300, 0);
    }

    void setTxt()
    {
        txt.text = text;
    }

    // Update is called once per frame
    void Update()
    {
        currenttime = Time.time;
        float duratetime = currenttime - starttime;
        if (duratetime <= 0.5f)
        {
            rt.localPosition = rt.localPosition + new Vector3(0, duratetime * 0.8f, 0);
        }
        if (duratetime >= 2.5f)
        {
            // visualize 2 sec
            rt.localPosition = rt.localPosition + new Vector3(0, 0.15f * duratetime, 0);
            float a = img.color.a - 0.001f * duratetime;
            img.color = new Color(0, 0, 0, a);
            txt.color -= new Color(0, 0, 0, 0.001f * duratetime);
        }
        if(txt.alpha <= 0)
        {
            // after 2 sec, solowly upper and alpha down
            // almost 1 sec
            GameManager.Resource.Destroy(this.gameObject);
        }
    }
}
