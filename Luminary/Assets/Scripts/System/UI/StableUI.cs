using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StableUI : MonoBehaviour
{
    // HP, Mana UI
    [SerializeField]
    Sprite BlankHP_L;
    [SerializeField]
    Sprite BlankHP_C;
    [SerializeField] 
    Sprite BlankHP_R;
    [SerializeField]
    Sprite BlankMP_L;
    [SerializeField]
    Sprite BlankMP_C;
    [SerializeField]
    Sprite BlankMP_R;

    [SerializeField]
    GameObject HP;
    [SerializeField]
    GameObject Mana;
    
    [SerializeField]
    Sprite FillHP_L;
    [SerializeField]
    Sprite FillHP_C;
    [SerializeField]
    Sprite FillHP_R;
    [SerializeField]
    Sprite FillMana_L;
    [SerializeField]
    Sprite FillMana_C;
    [SerializeField]
    Sprite FillMana_R;

    [SerializeField]
    SpriteRenderer FillHPBar;
    [SerializeField]
    SpriteRenderer FillManaBar;

    public List<GameObject> HPBar;
    public List<GameObject> currentHPBar;
    public List<GameObject> MPBar;
    public List<GameObject> currentMPBar;

    int maxHP, maxMana;
    int currentHP, currentMana;

    Player player;

    RectTransform rt;
    public void Start()
    {
        rt = GetComponent<RectTransform>();
        rt.transform.SetParent(GameManager.Instance.canvas.transform, false);
        rt.transform.localScale = Vector3.one;
        rt.transform.localPosition = Vector3.zero;
    }

    public void init()
    {
        // When Player Gen, Call Init();
        Debug.Log("init");
        player = GameManager.player.GetComponent<Player>();
        FreshMaxHPMP();
        FreshHPMP();
    }


    public void Update()
    {
        if(GameManager.gameState == GameState.InPlay)
            FreshHPMP();
    }

    public void FreshMaxHPMP()
    {
        if(maxHP != GameManager.player.GetComponent<Player>().status.maxHP)
        {
            maxHP = GameManager.player.GetComponent<Player>().status.maxHP;
            foreach (GameObject hp in currentHPBar)
            {
                GameManager.Resource.Destroy(hp);

            }
            currentHPBar.Clear();
            for(int i = 0; i < currentHP; i++)
            {
                GameObject go = new GameObject();
                go.AddComponent<RectTransform>();
                go.transform.SetParent(HP.transform, false);
                go.GetComponent<RectTransform>().localScale = Vector3.one;
                go.GetComponent<RectTransform>().localPosition = new Vector3(i * 0.64f, 0, 0);
                go.AddComponent<SpriteRenderer>();
                if (i == 0)
                {
                    go.GetComponent<SpriteRenderer>().sprite = FillHP_L;
                    Debug.Log("Left");
                }
                else if(i == maxHP - 1)
                {
                    go.GetComponent<SpriteRenderer>().sprite = FillHP_R;
                }
                else
                {
                    go.GetComponent<SpriteRenderer>().sprite = FillHP_C;
                }
                HPBar.Add(go);
            }
        }
        if(maxMana != GameManager.player.GetComponent<Player>().status.maxMana)
        {
            maxMana = GameManager.player.GetComponent<Player>().status.maxMana;
            foreach (GameObject hp in MPBar)
            {
                GameManager.Resource.Destroy(hp);

            }
            MPBar.Clear();
            for (int i = 0; i < maxMana; i++)
            {
                GameObject go = new GameObject();
                go.AddComponent<RectTransform>();
                go.transform.SetParent(Mana.transform, false);
                go.GetComponent<RectTransform>().localScale = Vector3.one;
                go.GetComponent<RectTransform>().localPosition = new Vector3(i * 0.64f, 0, 0);
                go.AddComponent<SpriteRenderer>();
                if (i == 0)
                {
                    go.GetComponent<SpriteRenderer>().sprite = BlankMP_L;
                }
                else if (i == maxMana - 1)
                {
                    go.GetComponent<SpriteRenderer>().sprite = BlankMP_R;
                }
                else
                {
                    go.GetComponent<SpriteRenderer>().sprite = BlankMP_C;
                }
            }
        }
    }

    public void FreshHPMP()
    {
        if (currentHP != GameManager.player.GetComponent<Player>().status.currentHP)
        {
            currentHP = GameManager.player.GetComponent<Player>().status.currentHP;
            foreach (GameObject hp in HPBar)
            {
                GameManager.Resource.Destroy(hp);

            }
            HPBar.Clear();
            for (int i = 0; i < maxHP; i++)
            {
                GameObject go = new GameObject();
                go.AddComponent<RectTransform>();
                go.transform.SetParent(HP.transform, false);
                go.GetComponent<RectTransform>().localScale = Vector3.one;
                go.GetComponent<RectTransform>().localPosition = new Vector3(i * 0.64f, 0, 0);
                go.AddComponent<SpriteRenderer>();
                if (i == 0)
                {
                    go.GetComponent<SpriteRenderer>().sprite = BlankHP_L;
                    Debug.Log("Left");
                }
                else if (i == maxHP - 1)
                {
                    go.GetComponent<SpriteRenderer>().sprite = BlankHP_R;
                }
                else
                {
                    go.GetComponent<SpriteRenderer>().sprite = BlankHP_C;
                }
                currentHPBar.Add(go);
            }
        }
        if (currentMana != GameManager.player.GetComponent<Player>().status.currentMana)
        {
            currentMana = GameManager.player.GetComponent<Player>().status.currentMana;
            foreach (GameObject hp in currentMPBar)
            {
                GameManager.Resource.Destroy(hp);

            }
            currentMPBar.Clear();
            for (int i = 0; i < currentMana; i++)
            {
                GameObject go = new GameObject();
                go.AddComponent<RectTransform>();
                go.transform.SetParent(Mana.transform, false);
                go.GetComponent<RectTransform>().localScale = Vector3.one;
                go.GetComponent<RectTransform>().localPosition = new Vector3(i * 0.64f, 0, 0);
                go.AddComponent<SpriteRenderer>();
                if (i == 0)
                {
                    go.GetComponent<SpriteRenderer>().sprite = FillMana_L;
                }
                else if (i == maxMana - 1)
                {
                    go.GetComponent<SpriteRenderer>().sprite = FillMana_C;
                }
                else
                {
                    go.GetComponent<SpriteRenderer>().sprite = FillMana_R;
                }
                currentMPBar.Add(go);
            }
            
        }
    }
}
