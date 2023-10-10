using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class ScriptManager
{
    public Dictionary<int, List<string>> scripts;

    string scriptXMLFile = "ScriptXML";

    XmlDocument text;

    public void init()
    {
        text = GameManager.Resource.LoadXML(scriptXMLFile);
        scripts = new Dictionary<int, List<string>>();
        Debug.Log("ScriptManager Init");
    }

    public List<string> getTxtData(int index)
    {
        List<string> ret = new List<string>();

        XmlNode node = text.SelectSingleNode($"/ScriptData/Script[id='{index}']");
        if(node != null)
        {
            XmlNodeList textNode = node.SelectNodes("Text");
            foreach (XmlNode text in textNode)
            {
                ret.Add(text.InnerText);
            }

            return ret;
        }
        else
        {
            Debug.Log($"Can't Find Script about : {index}");
            return null;
        }
    }
}
