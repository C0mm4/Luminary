using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Xml;
using Unity.VisualScripting;
using UnityEngine;

public class SpellManager
{
    public Dictionary<int, Spell> spells;

    string spellXMLFile = "SpellsXML";

    XmlNodeList text;

    public void init()
    {
        XmlDocument doc = GameManager.Resource.LoadXML(spellXMLFile);
        spells = new Dictionary<int, Spell>();  

        text = doc.GetElementsByTagName("Spell");

        createSpellObj();
    }


    public void createSpellObj()
    {
        foreach (XmlNode node in text)
        {

            Spell spl = new Spell();
            spl.setData(setSpellData(node));
            spells.Add(int.Parse(node["id"].InnerText), spl);

        }
    }
    
    public SpellData setSpellData(XmlNode node)
    {
        SpellData spellData = new SpellData();
        spellData.name = node["name"].InnerText;
        spellData.cd = float.Parse(node["cd"].InnerText);
        spellData.circle = int.Parse(node["circle"].InnerText);
        spellData.type = int.Parse(node["type"].InnerText);
        spellData.xRange = float.Parse(node["xRange"].InnerText);
        spellData.yRange = float.Parse(node["yRange"].InnerText);
        spellData.damage = int.Parse(node["damage"].InnerText);
        spellData.hits = int.Parse(node["hits"].InnerText);
        spellData.castTime = float.Parse(node["castT"].InnerText);
        spellData.debufP = float.Parse(node["debufP"].InnerText);
        spellData.durateT = float.Parse(node["durateT"].InnerText);
        spellData.path = node["prefabpath"].InnerText;
        spellData.spr = GameManager.Resource.LoadSprite(node["spr"].InnerText);

        return spellData;
    }
}
