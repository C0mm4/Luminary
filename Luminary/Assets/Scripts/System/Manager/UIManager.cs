using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;

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
    public GameObject stableUI;
    public GameObject menuUI;
    public GameObject pauseUI;
    public GameObject loadUI;

    public bool isInit = false;


    Queue<string> textUIqueue = new Queue<string>();
    private float textUItime = -3f;

    public Menu currentMenu = null;
    public Stack<Menu> menuStack = new Stack<Menu>();

    Stack<UIState> uistack = new Stack<UIState>();


    public void addMenu(Menu menu)
    {
        Debug.Log("addMenu");
        if(currentMenu != null)
        {
            currentMenu.hide();
            menuStack.Push(currentMenu);
        }
        currentMenu = menu;
        ChangeStateOnStack(UIState.Menu);
        currentMenu.show();
        Debug.Log(uistack.Peek().ToString());
    }

    public void endMenu()
    {
        currentMenu = null;
        if (menuStack.Count > 0)
        {
            currentMenu = menuStack.Pop();
            currentMenu.show();
        }
        PopStateStack();
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

        if (GameManager.uiState != UIState.Title)
        {
            UIClear();
        }
        invUI = GameManager.Resource.Instantiate("UI/Inventory2");
        invUI.GetComponent<Inventory>().init();
        

        skillSlotUI = GameManager.Resource.Instantiate("UI/SkillSlots");
        skillSlotUI.GetComponent<SkillSlotUI>().init();
        skillSlotUI.GetComponent<HPUI>().init();
        skillSlotUI.SetActive(false);
        textUItime = -3f;
        isInit = true;


        stableUI = GameManager.Resource.Instantiate("UI/StableUI");
        skillSlotUI.SetActive(false);
    }


    public void UIClear()
    {
        foreach(Transform child in transform)
        {
            GameManager.Resource.Destroy(child.gameObject);
        }
    }

    public void invenFresh()
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
        if(Input.GetKeyDown(PlayerDataManager.keySetting.inventoryKey))
        {
            if(GameManager.uiState != UIState.Menu)
            {
                addMenu(invUI.GetComponent<Menu>());
            }
        }
    }


    public void InventoryToggleInput()
    {
        if (Input.GetKeyDown(PlayerDataManager.keySetting.inventoryKey))
        {
            if (GameManager.uiState != UIState.Menu) 
            {
                addMenu(invUI.GetComponent<Menu>());
            }
        }
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

    }
    
    public void ChangeStateOnStack(UIState state)
    {
        uistack.Push(GameManager.uiState);
        Debug.Log("UIStack Push");
        ChangeState(state);
    }

    public void PopStateStack()
    {
        Debug.Log(uistack.Count);
        ChangeState(uistack.Pop());
    }
}
