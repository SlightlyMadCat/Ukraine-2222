using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class MoneyPrefab : MonoBehaviour
{
    [SerializeField] private Vector2 moneyBounds;
    private float money;
    private Image image;
    
    private void Awake()
    {
        money = Random.Range(moneyBounds.x, moneyBounds.y);
        image = GetComponent<Image>();
        
        ItemDataBase.Instance.AddMoneyPref(this);
    }

    public void AddMoney()
    {
        EconomyController.Instance.ChangeMoneyCount(money);
        
        ItemDataBase.Instance.RemoveMoneyPrefab(this);
        Destroy(gameObject);
    }
    
    public void SetRaycastingState(bool _val)
    {
        image.raycastTarget = _val;
    }
}
