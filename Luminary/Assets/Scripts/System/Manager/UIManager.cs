using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    Canvas canvas;

    [SerializeField]
    Camera camera;
    bool invenUI = false, skillUI = false, skillSlotUI = false, inGameUI = false, bossUI = false, mapUI = false;

//    SkillSlotUI skillslot;
    Inventory inventory;

    private void Awake()
    {
        GameObject invgo = GameManager.Resource.Instantiate("Inventory");
        invgo.transform.parent = canvas.transform;
        RectTransform rt = invgo.GetComponent<RectTransform>();

        rt.position = new Vector3(0, 0, 0);
        rt.localScale = Vector3.one;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            Debug.Log("Tab INPUT");
        }

        if (Input.GetKeyDown("k"))
        {
            Debug.Log("K Input");
        }

        if (Input.GetKeyDown("i"))
        {
            Debug.Log("\"I\" KEY INPUT");
        }
    }

    
}
