using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TualetkaItem : ResourceItem
{
    public override void OnFixedUpdate()
    {
        BoostItem.Instance.TryMakeBoost(this);
    }

    public override void OnAwake()
    {
        BoostItem.Instance.AddTualetkaItem(this);
    }

    private void OnDestroy()
    {
        BoostItem.Instance.RemoveTualetkaItem(this);
    }
}
