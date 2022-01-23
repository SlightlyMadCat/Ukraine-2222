using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class MoneyPrefab : MonoBehaviour
{
    [SerializeField] private Vector2 moneyBounds;
    private float money;

    private void Awake()
    {
        money = Random.Range(moneyBounds.x, moneyBounds.y);
    }

    public void AddMoney()
    {
        EconomyController.Instance.ChangeMoneyCount(money);
        Destroy(gameObject);
    }
}
