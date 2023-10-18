using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEditor;
using UnityEngine;

public class NPCUI : Menu
{
    public NPC npc;
    [SerializeField]
    public GameObject nameUI;
    public GameObject TextUI;
    public GameObject SelectUI;
    public List<GameObject> selects;

    public string text;
    public int textCnt;
    public int currentCnt;

    public bool isDataSet = false;
    public bool isCoroutineActivate = false;

    public bool isActivate = false;

    public int currentSelection = 0;

    // Set Transform
    public override void Start()
    {
        base.Start();
        Func.SetRectTransform(gameObject);
        SelectUI.SetActive(false);
    }


    public override void InputAction()
    {
        if (!isActivate)
        {
            // any input detected, all texts showing
            if (Input.anyKeyDown)
            {
                currentCnt = textCnt;
            }
        }
        else
        {
            // WS and up down arrows move selection menus
            if(npc.selections.Count <= 0)
            {
                if (Input.GetKeyDown(PlayerDataManager.keySetting.InteractionKey))
                {
                    GameManager.Instance.uiManager.endMenu();
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
                {
                    DeSelectHandler(currentSelection);
                    currentSelection--;
                    if (currentSelection < 0)
                    {
                        currentSelection = npc.selections.Count - 1;
                    }
                    SelectHandler(currentSelection);
                    
                }
                if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
                {
                    DeSelectHandler(currentSelection);

                    currentSelection++;
                    if (currentSelection >= npc.selections.Count)
                    {
                        currentSelection = 0;
                    }
                    SelectHandler(currentSelection);

                }

                if (Input.GetKeyDown(PlayerDataManager.keySetting.InteractionKey))
                {
                    SelectionWork();
                }
            }
        }
    }

    public void Update()
    {
        if (!isActivate)
        {
            if (isDataSet && currentCnt <= textCnt)
            {
                // Filling Texts in UI
                if (!isCoroutineActivate)
                {
                    isCoroutineActivate = true;
                    StartCoroutine(TextFilling());
                }
            }
            else
            {
                // After Filling all texts, Selection UI sprite Set
                isActivate = true;
                selects[currentSelection].GetComponent<SpriteRenderer>().sprite = selects[currentSelection].GetComponent<Choice>().select;
            }
        }
    }

    public virtual void SelectionWork()
    {
        selects[currentSelection].GetComponent<Choice>().Work();
    }

    public IEnumerator TextFilling()
    {
        TMP_Text txt = TextUI.GetComponentInChildren<TMP_Text>();
        if (txt != null)
        {
            txt.text = text[0..currentCnt];
            currentCnt++;
        }
        yield return new WaitForSeconds(0.1f);
        isCoroutineActivate = false;
    }

    public void setData()
    {
        int cnt = npc.scripts.Count;
        text = npc.scripts[GameManager.Random.getGeneralNext(0, cnt)];
        textCnt = text.Length;
        currentCnt = 0;

        setSelection();

        isDataSet = true;
    }
    // Set Selection positions
    public void setSelection()
    {
        if(npc.selections.Count > 0)
        {
            for(int i = 0; i < npc.selections.Count; i++) 
            { 
                GameObject go = GameManager.Resource.Instantiate("UI/NPCUI/Selection/" + npc.selections[i], SelectUI.transform);
                go.GetComponent<Choice>().index = i;
                go.GetComponent<RectTransform>().localPosition = new Vector3(0, npc.selections.Count * 0.64f - 0.32f - i * 0.64f, 0);
                go.GetComponent<Choice>().npc = npc;
                selects.Add(go);
            }
        }
    }
    // Highlight select ui
    public void SelectHandler(int index)
    {
        selects[index].GetComponent<SpriteRenderer>().sprite = selects[currentSelection].GetComponent<Choice>().select;
    }
    // Highlight off select ui
    public void DeSelectHandler(int index)
    {
        selects[index].GetComponent<SpriteRenderer>().sprite = selects[currentSelection].GetComponent<Choice>().deSelect;
    }

    public override void hide()
    {
        npc.isActivate = false;
        base.hide();
    }

    public override void ConfirmAction()
    {
    }
}
