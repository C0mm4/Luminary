using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractionTrriger : MonoBehaviour
{
    private float distanceToPlayer;
    [SerializeField]
    public float interactDist;

    protected GameObject popupUI;
    float width;

    // Update is called once per frame
    void Update()
    {
        width = GetComponent<SpriteRenderer>().bounds.size.x;
        if (GameObject.FindWithTag("Player")) 
        {
            distanceToPlayer = Vector3.Distance(transform.position, GameManager.player.transform.position);
            if(PlayerDataManager.interactionObject != gameObject)
            {
                if (distanceToPlayer <= interactDist && distanceToPlayer <= PlayerDataManager.interactionDistance)
                {
                    PlayerDataManager.interactionObject = gameObject;
                    PlayerDataManager.interactionDistance = distanceToPlayer;
                    Debug.Log("Now interactionObject is " + gameObject.name);
                    // ac
                }
            }
            else
            {
                if(distanceToPlayer > interactDist)
                {
                    PlayerDataManager.interactionObject = null;
                    PlayerDataManager.interactionDistance = interactDist + 1f;
                    Debug.Log("Now interactionObject is not" + gameObject.name);
                    //ac
                }
            }
        }
        if (PlayerDataManager.interactionObject == gameObject)
        {
            if (popupUI == null)
            {
                popupUI = GameManager.Resource.Instantiate("UI/InteractionUI", GameManager.Instance.canvas.transform);

            }
            PopUpMenu();
        }
        else
        {
            GameManager.Resource.Destroy(popupUI);
            popupUI = null;
        }
    }
    public virtual void isInteraction()
    {
        PlayerDataManager.interactionObject = null;
        PlayerDataManager.interactionDistance = 5.5f;
    }

    public void PopUpMenu()
    {
        Func.SetRectTransform(popupUI, GameManager.cameraManager.camera.WorldToScreenPoint(transform.position) - new Vector3(Screen.width / 2, Screen.height / 2, 0) + new Vector3(width + 250, 50, 0));
    }
}
