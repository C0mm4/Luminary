using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractionTrriger : MonoBehaviour
{
    private float distanceToPlayer;


    // Update is called once per frame
    void Update()
    {
        if (GameObject.FindWithTag("Player")) 
        {
            distanceToPlayer = Vector3.Distance(transform.position, GameManager.player.transform.position);
            if (distanceToPlayer <= 5.0f && distanceToPlayer < PlayerDataManager.interactionDistance)
            {
                PlayerDataManager.interactionObject = gameObject;
                PlayerDataManager.interactionDistance = distanceToPlayer;
                Debug.Log("Now interactionObject is " + gameObject.name);
                // ac
            }
            else if (PlayerDataManager.interactionObject == gameObject)
            {
                PlayerDataManager.interactionObject = null;
                PlayerDataManager.interactionDistance = 5.5f;
                Debug.Log("Now interactionObject is not" + gameObject.name);
                //ac
            }
        }
    }
    public abstract void isInteraction();
}
