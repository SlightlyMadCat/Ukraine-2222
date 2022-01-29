using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SigaItem : ResourceItem
{
    public override void OnFixedUpdate()
    {
        BoostItem.Instance.TryMakeBoost(this);
    }

    public override void OnAwake()
    {
        BoostItem.Instance.AddSigaItem(this);
    }
    
    private void OnDestroy()
    {
        BoostItem.Instance.RemoveSigaItem(this);
    }
}
