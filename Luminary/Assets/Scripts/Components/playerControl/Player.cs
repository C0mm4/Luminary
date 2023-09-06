using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using UnityEngine;

public class Player : Charactor
{
    // Start is called before the first frame update

    public SkillSlot[] skillslots;
    public List<SkillSlot> spells;
    public SkillSlot currentSpell;

    [SerializeField]
    InteractionTrriger interactionTrriger;

    public bool isInit = false;
    public bool ismove = false;
    public Vector2 playerSpeed = new Vector2();

    public override void Awake()
    {
        base.Awake();
        player = this;
        GameManager.player = player.gameObject;

        skillslots = new SkillSlot[5];
        spells = new List<SkillSlot>();
        setSkillSlots();


        skillslots[0].setCommand(GameManager.Spells.spells[1]);
        skillslots[1].setCommand(GameManager.Spells.spells[1003000]);
        skillslots[2].setCommand(GameManager.Spells.spells[1003001]);

        status = PlayerDataManager.playerStatus;

        calcStatus();
        GameManager.Instance.SceneChangeAction += DieObject;
        sMachine.changeState(new PlayerIdleState());
        isInit = true;
        currentSpell = skillslots[1];

        Debug.Log(status.inventory.Count);
    }
    private void setSkillSlots()
    {
        skillslots[0] = new SkillSlot();
        skillslots[1] = new SkillSlot();
        skillslots[2] = new SkillSlot();
    }


    public override void DieObject()
    {
        GameManager.inputManager.KeyAction -= moveKey;
        GameManager.inputManager.KeyAction -= spellKey;
        GameManager.Instance.SceneChangeAction -= DieObject;
        PlayerDataManager.playerStatus = status;
        base.DieObject();
    }

    public override void FixedUpdate()
    {
        if (getState() == null)
        {
            changeState(new PlayerIdleState());
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (skillslots[0].isSet())
            {
                if (!skillslots[0].getSpell().isCool)
                {
                    if(ismove)
                        StartCoroutine("roll");
                }
            }
        }

        playerSpeed = Vector2.zero;
        if (Input.GetKey(KeyCode.W))
        {
            playerSpeed.y = status.speed;
        }

        if (Input.GetKey(KeyCode.S))
        {
            playerSpeed.y = -status.speed;
        }

        if (Input.GetKey(KeyCode.A))
        {
            playerSpeed.x = -status.speed;
        }

        if (Input.GetKey(KeyCode.D))
        {
            playerSpeed.x = status.speed;
        }

        if (Input.GetKeyDown(PlayerDataManager.keySetting.InteractionKey))
        {
            if(PlayerDataManager.interactionObject != null)
            {
                interactionTrriger = PlayerDataManager.interactionObject.GetComponent<InteractionTrriger>();
                interactionTrriger.isInteraction();
            }
                
        }

        if (!ismove && playerSpeed != Vector2.zero)
        {
            ismove = true;
            changeState(new PlayerMoveState());
        }
        base.FixedUpdate();
    }

    public void moveKey()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            status.level++;
            Debug.Log(status.level);
        }
    }

    public void spellKey()
    {
        if (Input.GetMouseButtonDown(0))
        {
            currentSpell.useSkill();
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if(currentSpell == skillslots[1])
            {
                Debug.Log("SpellSlot Change 2");
                currentSpell = skillslots[2];
            }
            else
            {
                Debug.Log("SpellSlot Change 1");
                currentSpell = skillslots[1];
            }
        }
    }


    public IEnumerator roll()
    {
        float cd = 0f;
        if (GameManager.FSM.getList(sMachine.getStateStr()).Contains("PlayerCastingState")) 
        {
            if (skillslots[0].isSet())
            {
                changeState(new PlayerCastingState(skillslots[0].getSpell(), playerSpeed));
                skillslots[0].getSpell().isCool = true;
                skillslots[0].useSkill();
                cd = skillslots[0].getCD();
            }

            yield return new WaitForSeconds(cd);
            if (skillslots[0].isSet())
            {
                skillslots[0].getSpell().isCool = false;
            }
        }

    }

    public void ItemEquip(int index, Item item)
    {
        if (currentequipSize < 4)
        {
            for (int i = 0; i < 4; i++)
            {
                if (status.equips[i].item == null)
                {
                    status.equips[i].AddItem(item);
                    break;
                }
            }
            status.inventory[index].RemoveItem();
            GameManager.Instance.uiManager.invenFrest();
        }
        else
        {
            Debug.Log("Full Equiped");
        }
    }

    public void WeaponEquip(int index, Item item)
    {
        if (currentweaponSize < 2)
        {
            for (int i = 0; i < 2; i++)
            {
                if (status.weapons[i].item == null)
                {
                    status.weapons[i].AddItem(item);
                    skillslots[i + 1].setCommand(GameManager.Spells.spells[item.data.spellnum]);
                    break;
                }
            }
            status.inventory[index].RemoveItem();
            GameManager.Instance.uiManager.invenFrest();
        }
        else
        {
            Debug.Log("Full Equiped");
        }
    }

    public void ItemUnequip(int n)
    {
        if (ItemAdd(status.equips[n].item))
        {
            status.equips[n].RemoveItem();
            GameManager.Instance.uiManager.invenFrest();
        }
        else
        {
            Debug.Log("Inventory is Full");
        }
    }

    public void WeaponUnequip(int n)
    {
        if (ItemAdd(status.weapons[n].item))
        {
            status.weapons[n].RemoveItem();
            GameManager.Instance.uiManager.invenFrest();    
        }
    }

    public void Equip(int index, Item item)
    {
        if(item.data.type == 0)
        {
            WeaponEquip(index, item);
        }
        else
        {
            ItemEquip(index, item);
        }

        GameManager.Instance.uiManager.invenFrest();
    }

    public void Unequip(int index, Item item)
    {
        if(item.data.type == 0)
        {
            WeaponUnequip(index);
        }
        else
        {
            ItemUnequip(index);
        }
        GameManager.Instance.uiManager.invenFrest();
    }
}
