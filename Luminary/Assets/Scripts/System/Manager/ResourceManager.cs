using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;


// ���ҽ��� Load, Instantiate, Destroy �� �����ϴ� ���ҽ� �Ŵ���. 
public class ResourceManager
{

    public int a = 1;
    public int geta() { return a; }
    // path�� �ִ� ������ �ε��ϴ� �Լ�, �ε�Ǵ� ������ Object �� ��
    public T Load<T>(string path) where T : Object
    {
        return Resources.Load<T>(path);
    }


    // ���� 
    // parent�� �������� �����ؼ� ���� �� 
    public GameObject Instantiate(string path, Transform parent = null)
    {
        GameObject prefab = Load<GameObject>($"Prefabs/{path}");

        if (prefab == null)
        {
            Debug.Log($"Failed to load prefab : {path}");
            return null;
        }

        return Object.Instantiate(prefab, parent);
    }

    public XmlDocument LoadXML(string path)
    {
        XmlDocument xml = new XmlDocument();
        TextAsset txtAsset = Load<TextAsset>($"XML/{path}");
        xml.LoadXml(txtAsset.text);

        if (xml == null)
        {
            Debug.Log($"Failed to load XML : {path}");
            return null;
        }

        return xml;
    }

    public void Destroy(GameObject go)
    {
        if (go == null) return;
        Object.Destroy(go);
    }

}