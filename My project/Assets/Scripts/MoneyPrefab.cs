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
    private int money;
    private Image image;
    private int boost = 1;
    
    private void Awake()
    {
       
        image = GetComponent<Image>();
        
        ItemDataBase.Instance.AddMoneyPref(this);
    }

    public void AddMoney()
    {
        EconomyController.Instance.ChangeMoneyCount(money);
        
        ItemDataBase.Instance.RemoveMoneyPrefab(this);
        Destroy(gameObject);
    }

    public void AddBoost(int _boost)
    {
        boost = _boost;
        money = Random.Range((int)moneyBounds.x, (int)moneyBounds.y) * boost;
    }
    
    public void SetRaycastingState(bool _val)
    {
        image.raycastTarget = _val;
    }
}
