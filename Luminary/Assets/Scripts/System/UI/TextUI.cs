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
        rt.transform.localPosition = new Vector3(0, 350, 0);
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
        if (duratetime >= 1)
        {
            rt.localPosition = rt.localPosition + new Vector3(0, 1, 0);
            img.color = new Color(1, 1, 1);
            txt.alpha -= 0.001f;
        }
        if(txt.alpha <= 0)
        {
            GameManager.Resource.Destroy(this.gameObject);
        }
    }
}
