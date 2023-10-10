using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCClose : Choice
{
    // Start is called before the first frame update
    public override void Work()
    {
        GameManager.Instance.uiManager.endMenu();
    }
}
