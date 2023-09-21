using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    SpriteRenderer sprite;

    public void Activate()
    {
        Debug.Log("Door Activate");
    }

    public void DeActivate()
    {
        StartCoroutine(DeActivateCallBack());
    }


    public IEnumerator DeActivateCallBack()
    {
        yield return new WaitForSeconds(1);
        GameManager.Resource.Destroy(this.gameObject);
    }
}
