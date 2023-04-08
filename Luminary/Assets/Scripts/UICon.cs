using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UICon : MonoBehaviour
{
    public string seedText;
    [SerializeField] private TMP_InputField inputfield;
    public void gameStart()
    {
        GameManagers.clear();
        GameManagers.StageC.init();
    }

    public void nextStage()
    {
        GameManagers.clear();
        GameManagers.StageC.nextStage();
    }

    public void seedChange()
    {
        seedText = inputfield.text;
        
        Debug.Log(seedText);
        GameManagers.Random.setSeed(seedText);
    }
}
