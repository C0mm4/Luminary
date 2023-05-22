using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    Canvas canvas;

    [SerializeField]
    Camera camera;
    bool binvenUI = false, bskillUI = false, bskillSlotUI = false, binGameUI = false, bbossUI = false, bmapUI = false;

    [SerializeField]
    GameObject invUI;
    public GameObject skillSlotUI;

    Queue<string> textUIqueue = new Queue<string>();
    private float textUItime = -3f;

    public UIState uistate;

    private void Awake()
    { /*
        GameObject invgo = GameManager.Resource.Instantiate("Inventory");
        invgo.transform.parent = canvas.transform;
        RectTransform rt = invgo.GetComponent<RectTransform>();

        rt.position = new Vector3(0, 0, 0);
        rt.localScale = Vector3.one;
        */
    }

    // Manager ��ü�� ������ �ޱ⿡ Manager ��ü���� Initializion ���� ȣ���ؾ� ��.
    public void init()
    {

        invUI = GameManager.Resource.Instantiate("UI/Inventory2");
        invUI.GetComponent<Inventory>().init();
        invUI.SetActive(false);

        skillSlotUI = GameManager.Resource.Instantiate("UI/SkillSlots");
        skillSlotUI.GetComponent<SkillSlotUI>().init();
        skillSlotUI.SetActive(false);

        textUI("TEST TEXT UI");
        textUI("new Text UI");
    }

    public void invenFrest()
    {
        invUI.GetComponent<Inventory>().freshSlot();
    }


    public void textUI(string txt)
    {
        textUIqueue.Enqueue(txt);
    }

    private void GenTextUI()
    {

        var obj = GameManager.Resource.Instantiate("UI/TextUI");
        obj.GetComponent<TextUI>().text = textUIqueue.Dequeue();
    }

    public void inPlayInput()
    {

    }


    // Update is called once per frame
    void Update()
    {
    // Draw TEXT UI
        if(textUIqueue.Count > 0)
        {
            if(Time.time - textUItime > 2.5f)
            {
                textUItime = Time.time;
                GenTextUI();
            }
        }

        if(camera == null)
        {
            camera = GameObject.Find("Main Camera").GetComponent<Camera>();
        }
        if(canvas == null)
        {
            if(GameManager.Instance.canvas != null)
            {
                canvas = GameManager.Instance.canvas;
            }
        }
        if(invUI == null)
        {
            Debug.Log("INVNULL");
            invUI = canvas.transform.Find("Inventory2(Clone)").gameObject;
        }
        if (skillSlotUI == null)
        {
            skillSlotUI = canvas.transform.Find("SkillSlots(Clone)").gameObject;
        }


        if (Input.GetKeyDown(KeyCode.Tab))
        {
            bmapUI = !bmapUI;
        }

        if (Input.GetKeyDown("k"))
        {
            bskillSlotUI = !bskillSlotUI;
            skillSlotUI.gameObject.SetActive(bskillSlotUI);
        }

        if (Input.GetKeyDown(PlayerDataManager.keySetting.inventoryKey))
        {
            binvenUI = !binvenUI;
            if (invUI != null)
            {
                invUI.SetActive(binvenUI);
            }
            else
            {
                Debug.Log("NOT");
            }
        }
        if (Input.GetKeyDown("a"))
        {
            
        }
    }

    
}
