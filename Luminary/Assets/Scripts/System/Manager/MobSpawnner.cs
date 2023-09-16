using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobSpawnner : MonoBehaviour
{
    public List<MobData> mobLists = new List<MobData>();

    public Dictionary<int, MobData> mobDict = new Dictionary<int, MobData>();

    public void init()
    {
        foreach(MobData mob in mobLists)
        {
            mobDict[mob.index] = mob;
        }
    }

    public GameObject spawnMob(int index, Transform transform, Transform parent = null)
    {
        GameObject go;
        if(transform == null)
        {
            go = GameManager.Resource.Instantiate("Mobs/" + index);
            go.transform.position = transform.position;
        }
        else
        {
            go = GameManager.Resource.Instantiate("Mobs/" + index, parent);
            go.transform.position = transform.position;
        }
        if (go != null) 
        {
            return go;
        }
        else
        {
            return null;
        }
    }
}
