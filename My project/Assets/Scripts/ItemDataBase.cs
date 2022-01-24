using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDataBase : MonoBehaviour
{
    public static ItemDataBase Instance;
   [SerializeField] private List<ResourceItem> resourceItems = new List<ResourceItem>();

    private void Awake()
    {
        Instance = this;
    }

    public Color32 GetColorByItemID(int ID)
    {
        Color32 _color = Color.white;
        foreach (var item in resourceItems)
        {
            if (item.GetResourceID() == ID)
            {
                _color = item.GetImage().color;
                break;
            }
        }
        return _color;
    }
}
