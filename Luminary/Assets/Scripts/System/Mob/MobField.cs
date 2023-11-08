using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobField : MonoBehaviour
{
    public Mob shooter;
    public Charactor player;
    public bool isActive = false;
    public GameObject ActiveObj = null;
    public GameObject Prefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
            // Activate Hitbox Object
            if(ActiveObj == null)
            {
                ActiveObj = GameManager.Resource.Instantiate(Prefab, gameObject.transform);
                ActiveObj.transform.position = transform.position;
            }
        }
    }

    public virtual void setData(Mob mob, Vector3 pos = default(Vector3))
    {
        // Set target, attackers and transform
        shooter = mob;
        player = shooter.player;
        if(pos != default(Vector3))
        {
            transform.position = pos;
        }
        else
        {
            transform.position = new Vector3(player.transform.position.x, player.transform.position.y, 1);
        }
    }

    public virtual void setActive()
    {
        isActive = true;
    }
}
