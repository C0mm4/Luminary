using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionTrriger : MonoBehaviour
{
    private float distanceToPlayer;
    private string objectName;
    void Awake()
    {
        objectName = gameObject.name;
        Debug.Log("interactive object: " + objectName);
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.gameState == GameManager.GameState.InPlay)
        {
            distanceToPlayer = Vector3.Distance(transform.position, GameManager.player.transform.position);
        }
    }
}
