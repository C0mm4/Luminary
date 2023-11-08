using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Windows;

public class NewBehaviourScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        List<int> initialEnergy = new List<int>();
        initialEnergy.Add(5);
        initialEnergy.Add(2);
        initialEnergy.Add(13);
        initialEnergy.Add(10);
        int th = 8;
        int energySum = initialEnergy.Sum();
        initialEnergy.Sort();
        int time = 0;

        foreach(int i in initialEnergy)
        {
            Debug.Log(i);
        }

        while (energySum >= th)
        {
            time++;
            energySum -= initialEnergy.Count;
            for (int i = 0; i < initialEnergy.Count; i++)
            {
                Debug.Log("Count : " + (initialEnergy[i] - time));
                if (initialEnergy[i] <= time)
                {
                    initialEnergy.RemoveAt(i);
                    i--;
                }
            }
        }
        Debug.Log(time);
    }

}
