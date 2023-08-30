using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    Canvas canvas;

    [SerializeField]
    Camera camera;
    bool binvenUI = false, bskillUI = false, bskillSlotUI = false, binGameUI = false, bbossUI = false, bmapUI = false;

    [SerializeField]
    public GameObject invUI;
    public GameObject skillSlotUI;
    public GameObject menuUI;
    public GameObject pauseUI;
    public GameObject loadUI;

    public bool isInit = false;


    Queue<string> textUIqueue = new Queue<string>();
    private float textUItime = -3f;

    public Menu currentMenu = null;
    public Stack<Menu> menuStack = new Stack<Menu>();

    Stack<UIState> uistack = new Stack<UIState>();

    private void Start()
    {
        if (GameManager.Instance.uiManager != null)
        {
            init();
        }
    }

    public void addMenu(Menu menu)
    {
        if(currentMenu != null)
        {
            currentMenu.hide();
            menuStack.Push(currentMenu);
        }
        currentMenu = menu;
        switch (menu.GetType().Name)
        {
            case "PauseMenu":
                ChangeState(UIState.Pause);
                break;
            case "SettingMenu":
                ChangeState(UIState.Setting);
                break;
            case "":
                break;
        }
    }

    public void endMenu()
    {
        currentMenu = null;
        if (menuStack.Count > 0)
        {
            currentMenu = menuStack.Pop();
            currentMenu.show();
            switch (currentMenu.GetType().Name)
            {
                case "PauseMenu":
                    ChangeState(UIState.Pause);
                    break;
                case "SettingMenu":
                    ChangeState(UIState.Setting);
                    break;
                case "":
                    break;
            }
        }
        else
        {
            Debug.Log("In Play State");
            if(SceneManager.GetActiveScene().name == "LobbyScene")
            {
                ChangeState(UIState.Lobby);
            }
            else
            {
                ChangeState(UIState.InPlay);
            }
        }
    }

    // Manager ��ü�� ������ �ޱ⿡ Manager ��ü���� Initializion ���� ȣ���ؾ� ��.
    public void init()
    {
        if(GameManager.uiState == UIState.Title)
        {
            ChangeState(UIState.Title);
        }
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
        skillSlotUI.GetComponent<HPUI>().init();
        skillSlotUI.SetActive(false);
        textUItime = -3f;
        isInit = true;
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

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            // Tap Interaction
        }
    }

    public void MenuInput()
    {

    }

    public void InventoryToggleInput()
    {
        if (Input.GetKeyDown(PlayerDataManager.keySetting.inventoryKey))
        {
            if (GameManager.uiState != UIState.Inventory) 
            {
                ChangeStateOnStack(UIState.Inventory);
                invUI.SetActive(true);
            }
            else
            {
                PopStateStack();
                invUI.SetActive(false);
            }

        }
    }

    public void InventoryInInput()
    {

    }

    public void PauseInput()
    {

    }


    // Update is called once per frame
    void Update()
    {
        if (!isInit)
        {
            if(GameManager.Instance.uiManager != null)
            {
                init();
            }
        }
    // Draw TEXT UI
        if(textUIqueue.Count > 0)
        {
            if(Time.time - textUItime > 2.5f)
            {
                textUItime = Time.time;
                GenTextUI();
            }
        }

        if(invUI == null)
        {
            invUI = canvas.transform.Find("Inventory2(Clone)").gameObject;
        }
        if (skillSlotUI == null)
        {
            skillSlotUI = canvas.transform.Find("SkillSlots(Clone)").gameObject;
        }


    }

    public void ChangeState(UIState state)
    {
        
        GameManager.uiState = state;

        GameManager.inputManager.changeInputState();

        switch (state)
        {
            case UIState.Inventory:
                skillSlotUI.SetActive(false);
                break;
            case UIState.InPlay:
                skillSlotUI.SetActive(true);
                break;
            case UIState.Loading:
                break;
            case UIState.Lobby:
                skillSlotUI.SetActive(false);
                break;
            case UIState.CutScene:
                break;
            case UIState.Title:
                break;
        }
    }
    
    public void ChangeStateOnStack(UIState state)
    {
        uistack.Push(GameManager.uiState);
        ChangeState(state);
    }

    public void PopStateStack()
    {
        ChangeState(uistack.Pop());
    }
}
