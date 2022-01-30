using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasItem : ResourceItem
{
    public override void OnFixedUpdate()
    {
        BoostItem.Instance.TryMakeBoost(this, false);
    }

    public override void OnAwake()
    {
        BoostItem.Instance.AddGasItem(this);
    }
    
    private void OnDestroy()
    {
        BoostItem.Instance.RemoveGasItem(this);
        ItemDataBase.Instance.RemoveSpawnedItem(this);
    }
}
