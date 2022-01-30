using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrechkaItem : ResourceItem
{
    public override void OnFixedUpdate()
    {
        BoostItem.Instance.TryMakeBoost(this, false);
    }

    public override void OnAwake()
    {
        BoostItem.Instance.AddGrechkaItem(this);
    }
    
    private void OnDestroy()
    {
        BoostItem.Instance.RemoveGrechkaItem(this);
        ItemDataBase.Instance.RemoveSpawnedItem(this);
    }
}
