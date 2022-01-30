using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostItem : MonoBehaviour
{
    public static BoostItem Instance;
    [SerializeField] private List<GrechkaItem> grechkaItems = new List<GrechkaItem>();
    [SerializeField] private List<GasItem> gasItems = new List<GasItem>();
    [SerializeField] private List<SigaItem> sigaItems = new List<SigaItem>();
    [SerializeField] private List<TualetkaItem> tualetkaItems = new List<TualetkaItem>();
    
    private void Awake()
    {
        Instance = this;
    }
    
    public void AddGasItem(GasItem _item)
    {
        gasItems.Add(_item);
    }
    
    public void AddGrechkaItem(GrechkaItem _item)
    {
        grechkaItems.Add(_item);
    }
    
    public void AddSigaItem(SigaItem _item)
    {
        sigaItems.Add(_item);
    }
    
    public void AddTualetkaItem(TualetkaItem _item)
    {
        tualetkaItems.Add(_item);
    }
    
    public void RemoveGasItem(GasItem _item)
    {
        gasItems.Remove(_item);
    }
    
    public void RemoveGrechkaItem(GrechkaItem _item)
    {
        grechkaItems.Remove(_item);
    }
    
    public void RemoveSigaItem(SigaItem _item)
    {
        sigaItems.Remove(_item);
    }
    
    public void RemoveTualetkaItem(TualetkaItem _item)
    {
        tualetkaItems.Remove(_item);
    }

    public void TryMakeBoost(ResourceItem _itme, bool _val)
    {
        switch (_itme.GetResourceID())
        {
            case 0:
                MakeBoostGrechka(_itme, _val);
                break;
            case 1:
                MakeBoostGaz(_itme, _val);
                break;
            case 2:
                MakeBoostSiga(_itme, _val);
                break;
            case 3:
                MakeBoostTualetka(_itme, _val);
                break;
        }
    }

    private void MakeBoostGrechka(ResourceItem _item, bool _val)
    {
        if(grechkaItems.Count  < 2) return;
        foreach (var grechka in grechkaItems )
        {
            if (grechka != _item && !grechka.IsRightSide())
            {
                if(_val)
                    grechka.SetBoostTarget(null);
                else
                    grechka.SetBoostTarget(_item);
            }
        }
    } 
    
    private void MakeBoostGaz(ResourceItem _item, bool _val)
    {
        if(gasItems.Count  < 2) return;
        foreach (var grechka in gasItems)
        {
            if (grechka != _item && !grechka.IsRightSide())
            {
                if(_val)
                    grechka.SetBoostTarget(null);
                else
                    grechka.SetBoostTarget(_item);
            }
        }
    } 
    
    private void MakeBoostSiga(ResourceItem _item, bool _val)
    {
        if(sigaItems.Count  < 2) return;
        foreach (var grechka in sigaItems)
        {
            if (grechka != _item && !grechka.IsRightSide())
            {
                if(_val)
                    grechka.SetBoostTarget(null);
                else
                    grechka.SetBoostTarget(_item);
            }
        }
    } 
    
    private void MakeBoostTualetka(ResourceItem _item, bool _val)
    {
        if(tualetkaItems.Count  < 2) return;
        foreach (var grechka in tualetkaItems)
        {
            if (grechka != _item && !grechka.IsRightSide())
            {
                if(_val)
                    grechka.SetBoostTarget(null);
                else
                    grechka.SetBoostTarget(_item);
            }
        }
    } 
}
