using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemFunc
{
    public abstract void EquipEffect();
    public abstract void UnEquipEffect();

    public abstract void OnFrameEffect();
    public abstract void OnHitEffect();

    public abstract void OnDamagedEffect();
    
}
