using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ItemHoveringUI : MonoBehaviour
{
    public Item item;
    [SerializeField]
    public TMP_Text itemName;
    public Image img;

    public List<KeyValuePair<string, int>> status = new List<KeyValuePair<string, int>>();
    public string effectText;

    [SerializeField]
    public GameObject text;

    // Set showing item datas
    public void setData(Item itm) 
    { 
        item = itm;
        itemName.text = item.data.itemName;
        status = FindStatus();
        setSize();
    }

    // Resizing by item data scrolls
    public void setSize()
    {
        RectTransform rect = img.GetComponent<RectTransform>();
        float newWidth, newHeight;
        GameObject go;
        switch (status.Count)
        {
            case 1:
                newWidth = 400;
                newHeight = 200;
                go = GameManager.Resource.Instantiate(text, transform);
                go.GetComponent<RectTransform>().localPosition = new Vector3(0, -29, 0);
                go.GetComponent<TMP_Text>().text = status[0].Key + " +" + status[0].Value;
                go.GetComponent<TMP_Text>().fontSize = 30f;
                go.GetComponent<TMP_Text>().alignment = TextAlignmentOptions.Center;
                break;
            case 2:
                newWidth = 400;
                newHeight = 200;
                for(int i = 0; i < status.Count; i++)
                {
                    go = GameManager.Resource.Instantiate(text, transform);
                    go.GetComponent<RectTransform>().localPosition = new Vector3(0, -14 - 30 * i, 0);
                    go.GetComponent<TMP_Text>().text = status[i].Key + " +" + status[i].Value; 
                    go.GetComponent<TMP_Text>().fontSize = 30f;
                    go.GetComponent<TMP_Text>().alignment = TextAlignmentOptions.Center;
                }
                break;
            case 3:
                newWidth = 400;
                newHeight = 200; 
                for (int i = 0; i < status.Count; i++)
                {
                    go = GameManager.Resource.Instantiate(text, transform);
                    go.GetComponent<RectTransform>().localPosition = new Vector3(0, 1 - 30 * i, 0);
                    go.GetComponent<TMP_Text>().text = status[i].Key + " +" + status[i].Value;
                    go.GetComponent<TMP_Text>().fontSize = 30f;
                    go.GetComponent<TMP_Text>().alignment = TextAlignmentOptions.Center;
                }
                break;
            case 4:
                newWidth = 400;
                newHeight = 230;
                for (int i = 0; i < status.Count; i++)
                {
                    go = GameManager.Resource.Instantiate(text, transform);
                    go.GetComponent<RectTransform>().localPosition = new Vector3(0, 1 - 30 * i, 0);
                    go.GetComponent<TMP_Text>().text = status[i].Key + " +" + status[i].Value;
                    go.GetComponent<TMP_Text>().fontSize = 30f;
                    go.GetComponent<TMP_Text>().alignment = TextAlignmentOptions.Center;
                }
                break;
            case 5:
                newWidth = 400;
                newHeight = 260;
                for (int i = 0; i < status.Count; i++)
                {
                    go = GameManager.Resource.Instantiate(text, transform);
                    go.GetComponent<RectTransform>().localPosition = new Vector3(0, 1 - 30 * i, 0);
                    go.GetComponent<TMP_Text>().text = status[i].Key + " +" + status[i].Value;
                    go.GetComponent<TMP_Text>().fontSize = 30f;
                    go.GetComponent<TMP_Text>().alignment = TextAlignmentOptions.Center;
                }
                break;
            default:
                newWidth = 400;
                newHeight = 200;
                break;
        }
        if(item.data.effectText != "")
        {
            int index = -1;
            int count = 0;
            while ((index = item.data.effectText.IndexOf("\n", index + 1)) != -1)
            {
                count++;
            }
            newHeight += 50 + count * 20;
            go = GameManager.Resource.Instantiate(text, transform);
            go.GetComponent<RectTransform>().anchorMin = new Vector2(0.5f, 0f);
            go.GetComponent<RectTransform>().anchorMax = new Vector2(0.5f, 0f);
            go.GetComponent<TMP_Text>().text = item.data.effectText;
            go.GetComponent<TMP_Text>().fontSize = 30f;
            go.GetComponent<TMP_Text>().alignment = TextAlignmentOptions.Bottom;
            go.GetComponent<RectTransform>().localPosition = new Vector3(0, -80, 0);

        }
        rect.sizeDelta = new Vector2(newWidth, newHeight);
        transform.SetParent(GameManager.Instance.canvas.transform);
        GetComponent<RectTransform>().sizeDelta = new Vector2(newWidth, newHeight);
        
        GetComponent<RectTransform>().localScale = Vector3.one;


    }

    // Find showing item status data
    public List<KeyValuePair<string, int>> FindStatus()
    {
        List<KeyValuePair<string, int>> keyValuePairs = new List<KeyValuePair<string, int>>();
        if (item.data.status.strength != 0)
        {
            KeyValuePair<string, int> data = new KeyValuePair<string, int>("STR", item.data.status.strength);
            keyValuePairs.Add(data);
        }
        if (item.data.status.dex != 0)
        {
            KeyValuePair<string, int> data = new KeyValuePair<string, int>("DEX", item.data.status.dex);
            keyValuePairs.Add(data);
        }
        if (item.data.status.intellect != 0)
        {
            KeyValuePair<string, int> data = new KeyValuePair<string, int>("INT", item.data.status.intellect);
            keyValuePairs.Add(data);
        }
        if (item.data.status.increaseHP != 0)
        {
            KeyValuePair<string, int> data = new KeyValuePair<string, int>("MAX HP", item.data.status.increaseHP);
            keyValuePairs.Add(data);
        }
        if (item.data.status.increaseMP != 0)
        {
            KeyValuePair<string, int> data = new KeyValuePair<string, int>("MAX MP", item.data.status.increaseMP);
            keyValuePairs.Add(data);
        }
        if(item.data.effectText != "")
        {
            effectText = item.data.effectText;
        }
        return keyValuePairs;
    }
}
