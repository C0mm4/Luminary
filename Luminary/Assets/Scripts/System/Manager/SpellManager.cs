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

    XmlNodeList text;

    public void init()
    {
        XmlDocument doc = GameManager.Resource.LoadXML(spellXMLFile);
        allspells = new List<GameObject>();

        text = doc.GetElementsByTagName("Spell");

        createSpellObj();
    }


    public void createSpellObj()
    {
        allspells.Clear();
        Spells = new GameObject("SPELLS");
        foreach (XmlNode node in text)
        {
            GameObject spl = new GameObject("spl");
            allspells.Add(spl);
            spl.transform.parent = Spells.transform;
            Debug.Log(node["name"].InnerText);
            spl.AddComponent(Type.GetType(node["name"].InnerText));
            spl.GetComponent<Spell>().spr = GameManager.Resource.LoadSprite(node["spr"].InnerText);
        }
    }

}
