using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Xml;
using Unity.VisualScripting;
using UnityEngine;

public class SpellManager
{
    public List<GameObject> allspells;
    public GameObject Spells;
    string spellXMLFile = "SpellsXML";
    string test = "TestSpell";

    public void init()
    {
        XmlDocument doc = GameManager.Resource.LoadXML(spellXMLFile);
        allspells = new List<GameObject>();
        Spells = new GameObject("SPELLS");

        XmlNodeList text = doc.GetElementsByTagName("name");
        foreach ( XmlNode node in text)
        {
            Debug.Log("Load1");
            GameObject spl = new GameObject("spl");
            allspells.Add(spl);
            spl.transform.parent = Spells.transform;
            spl.AddComponent(Type.GetType(node.InnerText));
            Debug.Log(node.InnerText[3]);
        }
    }

}
