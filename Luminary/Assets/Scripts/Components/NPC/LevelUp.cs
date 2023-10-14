using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelUp : Menu
{
    SerializedPlayerStatus playerStatus;
    SerializedPlayerStatus tmpStatus;
    public int totalSelect;
    public int strSelect;
    public int dexSelect;
    public int intSelect;

    public TMP_Text level;
    public TMP_Text reqGold;
    public TMP_Text currGold;
    public TMP_Text str;
    public TMP_Text dex;
    public TMP_Text intellect;
    public TMP_Text hp;
    public TMP_Text mp;
    public TMP_Text spd;
    public TMP_Text dmg;

    public int requireGold;

    public override void ConfirmAction()
    {
        throw new System.NotImplementedException();
    }

    public override void InputAction()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            currentMenu++;
            if(currentMenu >= menusize)
            {
                currentMenu = 0;
            }
        }
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            currentMenu--;
            if (currentMenu < 0)
            {
                currentMenu = menusize - 1;
            }
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            UpHandler();
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            DownHandler();
        }
        if (Input.GetKeyDown(PlayerDataManager.keySetting.InteractionKey))
        {
            levelup();
        }
    }

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        Func.SetRectTransform(gameObject);
        tmpStatus = new SerializedPlayerStatus();
        playerStatus = GameManager.player.GetComponent<Player>().status;
        menusize = 3;
        DataSet();
    }

    public void levelup()
    {
        playerStatus.level += totalSelect;
        playerStatus.strength += strSelect;
        playerStatus.dexterity += dexSelect;
        playerStatus.Intellect += intSelect;
        GameManager.player.GetComponent<Player>().status = playerStatus;
        GameManager.player.GetComponent<Player>().calcStatus();
    }

    public void UpHandler()
    {
        totalSelect++;
        requireGold += totalSelect * 1000;
        switch (currentMenu)
        {
            case 0:
                strSelect++;
                break;
            case 1:
                dexSelect++;
                break;
            case 2:
                intSelect++;
                break;
        }
        DataSet();
    }

    public void DownHandler()
    {
        switch (currentMenu)
        {
            case 0:
                if(strSelect > 0)
                {
                    totalSelect--;
                    requireGold -= totalSelect * 1000;
                    strSelect--;
                }
                break;
            case 1:
                if (dexSelect > 0)
                {
                    totalSelect--;
                    requireGold -= totalSelect * 1000;
                    dexSelect--;
                }
                break;
            case 2:
                if (intSelect > 0)
                {
                    totalSelect--;
                    requireGold -= totalSelect * 1000;
                    intSelect--;
                }
                break;
        }
        DataSet();
    }

    public void DataSet()
    {
        level.text = playerStatus.level.ToString() + " -> " + (playerStatus.level + totalSelect).ToString();
        reqGold.text = requireGold.ToString();
        currGold.text = playerStatus.gold.ToString() + " -> " + (playerStatus.gold - requireGold).ToString();
        str.text = playerStatus.gold.ToString() + " -> " + (playerStatus.strength + strSelect).ToString();
        dex.text = playerStatus.gold.ToString() + " -> " + (playerStatus.dexterity + dexSelect).ToString();
        intellect.text = playerStatus.gold.ToString() + " -> " + (playerStatus.Intellect + intSelect).ToString();
        hp.text = playerStatus.gold.ToString() + " -> " + (playerStatus.strength + strSelect).ToString();
        mp.text = playerStatus.gold.ToString() + " -> " + (playerStatus.strength + strSelect).ToString();
        spd.text = playerStatus.gold.ToString();
        dmg.text = playerStatus.gold.ToString();
    }
}
