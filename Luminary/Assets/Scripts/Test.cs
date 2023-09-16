using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : InteractionTrriger
{
    public override void isInteraction()
    {
        Debug.Log("Test Code Running");
        GameManager.Instance.ItemDrop(0, transform);
        GameManager.Resource.Destroy(gameObject);
    }
}
