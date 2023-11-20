using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.PlayerLoop;


// 리소스의 Load, Instantiate, Destroy 를 관리하는 리소스 매니저. 
public class ResourceManager
{

    public int a = 1;
    public int geta() { return a; }
    // path에 있느 파일을 로드하는 함수, 로드되는 조건은 Object 일 때
    public T Load<T>(string path) where T : Object
    {
        return Resources.Load<T>(path);
    }


    // Game Object Loading with prefab paths
    public GameObject Instantiate(string path, Vector3 pos = default, Transform parent = null)
    {
        GameObject prefab = Load<GameObject>($"Prefabs/{path}");
        prefab.transform.position = pos;
        if (prefab == null)
        {
            Debug.Log($"Failed to load prefab : {path}");
            return null;
        }

        return Object.Instantiate(prefab, parent);
    }

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

    public GameObject Instantiate(string path)
    {
        GameObject prefab = Load<GameObject>($"Prefabs/{path}");
        if (prefab == null)
        {
            Debug.Log($"Failed to load prefab : {path}");
            return null;
        }

        return Object.Instantiate(prefab);
    }

    // Game Object Loading with GameObject Prefabs
    public GameObject Instantiate(GameObject obj, Vector3 pos = default, Transform parent = null)
    {
        if(obj == null)
        {
            return null;
        }
        GameObject prefab = Object.Instantiate(obj, parent);
        if (prefab == null)
        {
            Debug.Log($"Failed to laod prefab : {obj.name}");
            return null;
        }
        prefab.transform.position = pos;
        return prefab;
    }

    public GameObject Instantiate(GameObject obj, Transform parent = null)
    {
        if (obj == null)
        {
            return null;
        }
        GameObject prefab = Object.Instantiate(obj, parent);
        if (prefab == null)
        {
            Debug.Log($"Failed to laod prefab : {obj.name}");
            return null;
        }
        return prefab;
    }
    public GameObject Instantiate(GameObject obj)
    {
        if (obj == null)
        {
            return null;
        }
        GameObject prefab = Object.Instantiate(obj);
        if (prefab == null)
        {
            Debug.Log($"Failed to laod prefab : {obj.name}");
            return null;
        }
        return prefab;
    }

    // Loading XML Datas in path
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

    // Loading Sprites in path
    public Sprite LoadSprite(string path)
    {
        Sprite spr;
        spr = Load<Sprite>($"Sprites/{path}");
        if (spr == null)
        {
            Debug.Log($"Failed to load Sprite : {path}");
        }

        return spr;
    }

    // Destroy GameObject
    public void Destroy(GameObject go)
    {
        if (go == null) return;
        Object.Destroy(go);
    }

    // Destroy GameObjects
    public void Destroy(GameObject[] go)
    {
        foreach(GameObject g in go)
        {
            if(g != null)
            {
                Object.Destroy(g);
            }
        }
    }

}