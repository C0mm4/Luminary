using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class ItemDropManager : MonoBehaviour
{
    public static ItemDropManager Instance;

    [SerializeField]
    //private GameObject itemPrefab;
    private GameObject[] itemPrefab;

    public List<Item> itemList;
    int rand;

    private void Awake()
    {
        //테스트 아이템 10개 기준
        for (int i = 0; i < 10; i++)
        {
            ItemInit(itemPrefab[i]);
        }
    }

    public void ItemInit(GameObject itemPrefabs)
    {
        //GameObject itm = Instantiate(itemPrefabs);
        itemList.Add(itemPrefabs.GetComponent<Item>());
    }

    public void ItemGen()
    {
        //확률 동일한 경우로 가정했음.
        int n = Random.Range(0, 9);
        Debug.Log(n);
        ItemDrop(itemPrefab[n], new Vector2(0, 0));
    }

    public void ItemDrop(GameObject itemPrefabs, Vector2 _position)
    {
        GameObject item = Instantiate(itemPrefabs);
        Debug.Log(item);
        item.transform.position = _position;

        Inventory iv = new Inventory();
        item.transform.parent = iv.itemin.transform;
        iv.addItem(item);
    }
}

