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
        Debug.Log(status.maxHP);
        GameManager.Instance.SceneChangeAction += DieObject;
        sMachine.changeState(new PlayerIdleState());
        isInit = true;
        currentSpell = skillslots[1];
        Debug.Log(status.level);
    }
    private void setSkillSlots()
    {
        skillslots[0] = new SkillSlot();
        skillslots[1] = new SkillSlot();
        skillslots[2] = new SkillSlot();
        skillslots[3] = new SkillSlot();
        skillslots[4] = new SkillSlot();
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

            interactionTrriger = PlayerDataManager.interactionObject.GetComponent<InteractionTrriger>();
            interactionTrriger.isInteraction();
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
    public IEnumerator Q()
    {
        float cd = 0f;
        if (GameManager.FSM.getList(sMachine.getStateStr()).Contains("PlayerCastingState"))
        {
            if (skillslots[1].isSet())
            {
                skillslots[1].useSkill();
                cd = skillslots[1].getCD();
            }

            yield return new WaitForSeconds(cd);
            if (skillslots[1].isSet())
            {
                skillslots[1].getSpell().isCool = false;
            }
        }

    }

    public IEnumerator W()
    {
        float cd = 0f;
        if (GameManager.FSM.getList(sMachine.getStateStr()).Contains("PlayerCastingState"))
        {
            if (skillslots[2].isSet())
            {
                skillslots[2].useSkill();
                cd = skillslots[2].getCD();
            }

            yield return new WaitForSeconds(cd);
            if (skillslots[2].isSet())
            {
                skillslots[2].getSpell().isCool = false;
            }
        }

    }

    public IEnumerator E()
    {
        float cd = 0f;
        if (GameManager.FSM.getList(sMachine.getStateStr()).Contains("PlayerCastingState"))
        {
            if (skillslots[3].isSet())
            {
                skillslots[3].useSkill();
                cd = skillslots[3].getCD();
            }

            yield return new WaitForSeconds(cd);
            if (skillslots[3].isSet())
            {
                skillslots[3].getSpell().isCool = false;
            }
        }

    }

    public IEnumerator R()
    {
        float cd = 0f;
        if (GameManager.FSM.getList(sMachine.getStateStr()).Contains("PlayerCastingState"))
        {
            if (skillslots[4].isSet())
            {
                skillslots[4].useSkill();
                cd = skillslots[4].getCD();
            }

            yield return new WaitForSeconds(cd);
            if (skillslots[4].isSet())
            {
                skillslots[4].getSpell().isCool = false;
            }
        }

    }
}
