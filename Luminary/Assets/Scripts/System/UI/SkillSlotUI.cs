using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillSlotUI : MonoBehaviour
{
    RectTransform rt;
    

    public void init()
    {
        rt = GetComponent<RectTransform>();
        rt.transform.SetParent(GameManager.Instance.canvas.transform);
        rt.transform.localScale = Vector3.one;
        rt.transform.localPosition = Vector3.zero;
    }

}
