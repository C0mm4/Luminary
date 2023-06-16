using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPUI : MonoBehaviour
{
    RectTransform rt;
    Player player;

    public Image HPBar;
    public Image currentHP;

    public bool isInit = false;

    public void init()
    {
        if(GameObject.FindGameObjectWithTag("Player") != null)
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
            if (player.isInit)
            {
                currentHP.fillAmount = 1;
                isInit = true;
            }
            
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isInit)
        {
            if (GameObject.FindGameObjectWithTag("Player") != null)
            {
                player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
                if (player.isInit)
                {
                    currentHP.fillAmount = 1;
                    isInit = true;
                }
            }
        }
        else
        {
            currentHP.fillAmount = player.status.currentHP/player.status.maxHP;
        }
    }
}
