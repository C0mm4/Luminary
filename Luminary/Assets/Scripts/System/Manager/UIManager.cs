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
    bool invenUI = false, skillUI = false, skillSlotUI = false, inGameUI = false, bossUI = false, mapUI = false;

    SkillSlotUI skillslot;
    Inventory inventory;

    private void Awake()
    { /*
        GameObject invgo = GameManager.Resource.Instantiate("Inventory");
        invgo.transform.parent = canvas.transform;
        RectTransform rt = invgo.GetComponent<RectTransform>();

        rt.position = new Vector3(0, 0, 0);
        rt.localScale = Vector3.one;
        */
    }

    // Manager 객체의 영향을 받기에 Manager 객체들의 Initializion 이후 호출해야 함.
    public void init()
    {
        inventory = GameManager.Instance._inventory.GetComponent<Inventory>();
        skillslot = GameManager.SkillSlot.skillslotUI;

        inventory.init();
        skillslot.init();

        inventory.gameObject.SetActive(false);
        skillslot.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            mapUI = !mapUI;
        }

        if (Input.GetKeyDown("k"))
        {
            skillSlotUI = !skillSlotUI;
            skillslot.gameObject.SetActive(skillSlotUI);
        }

        if (Input.GetKeyDown("i"))
        {
            invenUI = !invenUI;
            inventory.gameObject.SetActive(invenUI);
        }
    }

    
}
