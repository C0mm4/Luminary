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
        GameManager.clear();
        GameManager.StageC.init();
    }

    
    public void nextStage()
    {
        GameManager.clear();
        GameManager.StageC.nextStage();
    }

    public void seedChange()
    {
        seedText = inputfield.text;
        
        Debug.Log(seedText);
        GameManager.Random.setSeed(seedText);
    }
}
