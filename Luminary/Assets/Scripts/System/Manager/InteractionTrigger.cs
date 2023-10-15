using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public abstract class InteractionTrriger : MonoBehaviour
{
    private float distanceToPlayer;
    [SerializeField]
    public float interactDist;

    protected GameObject popupUI;

    [SerializeField]
    protected string text;
    float width;

    

    // Update is called once per frame
    public virtual void  Update()
    {
        if(GameManager.uiState == UIState.InPlay || GameManager.uiState == UIState.Lobby)
        {
            width = GetComponent<SpriteRenderer>().bounds.size.x;
            if (GameObject.FindWithTag("Player"))
            {
                distanceToPlayer = Vector3.Distance(transform.position, GameManager.player.transform.position);
                if (PlayerDataManager.interactionObject != gameObject)
                {
                    if (distanceToPlayer <= interactDist && distanceToPlayer <= PlayerDataManager.interactionDistance)
                    {
                        PlayerDataManager.interactionObject = gameObject;
                        PlayerDataManager.interactionDistance = distanceToPlayer;
                        // ac
                    }
                }
                else
                {
                    if (distanceToPlayer > interactDist)
                    {
                        PlayerDataManager.interactionObject = null;
                        PlayerDataManager.interactionDistance = interactDist + 1f;
                        //ac
                    }
                }
            }
            if (PlayerDataManager.interactionObject == gameObject)
            {
                if (popupUI == null)
                {
                    popupUI = GameManager.Resource.Instantiate("UI/InteractionUI", GameManager.Instance.canvas.transform);
                    popupUI.GetComponent<InteractHover>().text.text = PlayerDataManager.keySetting.InteractionKey + " - " + text;

                }
                PopUpMenu();
            }
            else
            {
                GameManager.Resource.Destroy(popupUI);
                popupUI = null;
            }
        }
        else
        {
            if(popupUI != null) 
            {
                GameManager.Resource.Destroy(popupUI.gameObject);
            }
        }
    }
    public virtual void isInteraction()
    {
        PlayerDataManager.interactionObject = null;
        PlayerDataManager.interactionDistance = 5.5f;
        GameManager.Resource.Destroy(popupUI.gameObject);
    }

    public void PopUpMenu()
    {
        Func.SetRectTransform(popupUI, GameManager.cameraManager.camera.WorldToScreenPoint(transform.position) - new Vector3(Screen.width / 2, Screen.height / 2, 0) + new Vector3(width + 250, 50, 0));
    }
}
