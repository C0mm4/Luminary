using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    Canvas canvas;

    [SerializeField]
    Camera camera;
    bool binvenUI = false, bskillUI = false, bskillSlotUI = false, binGameUI = false, bbossUI = false, bmapUI = false;

    SkillSlotUI skillslot;
    [SerializeField]
    GameObject invUI;
    GameObject skillSlotUI;

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
        skillslot = GameManager.SkillSlot.skillslotUI;
        skillslot.init();
        skillslot.gameObject.SetActive(false);

        invUI = GameManager.Resource.Instantiate("UI/Inventory2");
        invUI.GetComponent<Inventory>().init();
        invUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
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
        if (skillslot == null)
        {
            if (GameManager.SkillSlot.skillslotUI != null)
            {
                skillslot = GameManager.SkillSlot.skillslotUI;
            }
        }


        if (Input.GetKeyDown(KeyCode.Tab))
        {
            bmapUI = !bmapUI;
        }

        if (Input.GetKeyDown("k"))
        {
            bskillSlotUI = !bskillSlotUI;
            skillslot.gameObject.SetActive(bskillSlotUI);
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
