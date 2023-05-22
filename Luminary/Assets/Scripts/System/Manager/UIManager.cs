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


    private void Awake()
    {
    }


    // Manager ��ü�� ������ �ޱ⿡ Manager ��ü���� Initializion ���� ȣ���ؾ� ��.
    public void init()
    {
        // Find Camera and Canvas Objects
        if (camera == null)
        {
            camera = GameObject.Find("Main Camera").GetComponent<Camera>();

        }
        if (canvas == null)
        {
            if (GameManager.Instance.canvas != null)
            {
                canvas = GameManager.Instance.canvas;
            }
        }
        if (canvas != null)
        {
            canvas.renderMode = RenderMode.ScreenSpaceCamera;
            canvas.worldCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
            
        }

        invUI = GameManager.Resource.Instantiate("UI/Inventory2");
        invUI.GetComponent<Inventory>().init();
        invUI.SetActive(false);

        skillSlotUI = GameManager.Resource.Instantiate("UI/SkillSlots");
        skillSlotUI.GetComponent<SkillSlotUI>().init();
        skillSlotUI.SetActive(false);
        textUItime = -3f;
        textUI("TEST TEXT UI");
        textUI("new Text UI");
    }

    public void Start()
    {
        init();
    }

    private void UIClear()
    {
        Transform[] trm = GetComponentsInChildren<Transform>();
        if(trm != null)
        {
            for(int i = 0; i < trm.Length; i++)
            {
                GameManager.Resource.Destroy(trm[i].gameObject);
            }
        }
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

    public void InPlayInput()
    {
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
                ChangeState(UIState.Inventory);
            }
            else
            {
                Debug.Log("NOT");
            }
        }
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            bmapUI = !bmapUI;
        }
    }

    public void MenuInput()
    {

    }

    public void InventoryInput()
    {
        if (Input.GetKeyDown(PlayerDataManager.keySetting.inventoryKey))
        {
            binvenUI = !binvenUI;
            if (invUI != null)
            {
                invUI.SetActive(binvenUI);
                ChangeState(UIState.InPlay);
            }
            else
            {
                Debug.Log("NOT");
            }
        }
    }

    public void PauseInput()
    {

    }


    // Update is called once per frame
    void Update()
    {
    // Draw TEXT UI
        if(textUIqueue.Count > 0)
        {
            Debug.Log("TextUI Queue Exists UI Time is : " + textUItime);
            if(Time.time - textUItime > 2.5f)
            {
                textUItime = Time.time;
                GenTextUI();
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



        if (Input.GetKeyDown("a"))
        {
            
        }
    }

    public void ChangeState(UIState state)
    {
        switch (GameManager.Instance.uiState)
        {
            case UIState.Loading:
                break;
            case UIState.InPlay:
                GameManager.inputManager.KeyAction -= InPlayInput;
                break;
            case UIState.Title:
                break;
            case UIState.CutScene:
                break;
            case UIState.Menu:
                GameManager.inputManager.KeyAction -= MenuInput;
                break;
            case UIState.Inventory:
                GameManager.inputManager.KeyAction -= InventoryInput;
                break;
            case UIState.Pause:
                GameManager.inputManager.KeyAction -= PauseInput;
                break;
        }

        GameManager.Instance.uiState = state;

        switch (GameManager.Instance.uiState)
        {
            case UIState.Loading:
                break;
            case UIState.InPlay:
                GameManager.inputManager.KeyAction += InPlayInput;
                break;
            case UIState.Title:
                break;
            case UIState.CutScene:
                break;
            case UIState.Menu:
                GameManager.inputManager.KeyAction += MenuInput;
                break;
            case UIState.Inventory:
                GameManager.inputManager.KeyAction += InventoryInput;
                break;
            case UIState.Pause:
                GameManager.inputManager.KeyAction += PauseInput;
                break;
        }
    }
    
}
