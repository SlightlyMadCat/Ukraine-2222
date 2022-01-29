using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDataBase : MonoBehaviour
{
    public static ItemDataBase Instance;
    [SerializeField] private List<ResourceItem> resourceItems = new List<ResourceItem>();
    private List<ResourceItem> spawnedItems = new List<ResourceItem>();
    private List<MoneyPrefab> spawnedMoneyPrefabs = new List<MoneyPrefab>();
    
    private void Awake()
    {
        Instance = this;
    }

    public Sprite GetSpriteByItemID(int ID)
    {
        Sprite _sprite =null;
        foreach (var item in resourceItems)
        {
            if (item.GetResourceID() == ID)
            {
                _sprite = item.GetImage().sprite;
                break;
            }
        }
        return _sprite;
    }

    public void AddSpawnedItem(ResourceItem _item)
    {
        spawnedItems.Add(_item);
    }

    public void RemoveSpawnedItem(ResourceItem _item)
    {
        spawnedItems.Remove(_item);
    }

    //ignore other items when some item is dragging
    public void SetDynamicItemsRaycastState(bool _val, ResourceItem _selectedItem)
    {
        foreach (var VARIABLE in spawnedItems)
        {
            if(VARIABLE == _selectedItem) continue;
            VARIABLE.SetRaycastingState(_val);
        }

        foreach (var VARIABLE in spawnedMoneyPrefabs)
        {
            VARIABLE.SetRaycastingState(_val);
        }
    }
    
    public void AddMoneyPref(MoneyPrefab _pref)
    {
        spawnedMoneyPrefabs.Add(_pref);
    }

    public void RemoveMoneyPrefab(MoneyPrefab _pref)
    {
        spawnedMoneyPrefabs.Remove(_pref);
    }
}
