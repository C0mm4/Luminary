using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionTrriger : MonoBehaviour
{
    private float distanceToPlayer;


    // Update is called once per frame
    void Update()
    {
        if(GameManager.gameState == GameState.InPlay)
        {
            distanceToPlayer = Vector3.Distance(transform.position, GameManager.player.transform.position);
            if(distanceToPlayer <= 5.0f && distanceToPlayer < PlayerDataManager.interactionDistance)
            {
                PlayerDataManager.interactionObject = gameObject;
                distanceToPlayer = PlayerDataManager.interactionDistance;
//                Debug.Log("Now interactionObject is " + objectName);
                // ac
            }
            else if(PlayerDataManager.interactionObject == gameObject)
            {
                PlayerDataManager.interactionObject = null;
                PlayerDataManager.interactionDistance = 5.5f;
//                Debug.Log("Now interactionObject is not" + objectName);
                //ac
            }
        }
    }
    public void isInteraction()
    {
        switch (gameObject.name)
        {
            case "InitPlayObject":
                
                GameManager.Instance.sceneControl("StageScene");
                break;
            default:
                break;
        }
    }
}