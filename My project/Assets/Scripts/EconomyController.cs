using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/*
 * Main money && res controller
 */

public class EconomyController : MonoBehaviour
{
    #region Instance

    public static EconomyController Instance;
    
    private void Awake()
    {
        Instance = this;
    }

    #endregion
    
    [SerializeField] private float currentMoney;
    public float GetCurrentMoney()
    {
        return currentMoney;
    }
    
    [Serializable]
    public class ResourceSample
    {
        [SerializeField] private string name;
        [SerializeField] private float currentAmount;
        [SerializeField] private float deltaPerTime;
        [SerializeField] private Slider amountSlider;

        public void ReduceAmount()
        {
            currentAmount -= deltaPerTime;
            if (currentAmount < 0) currentAmount = 0;
        }

        public void AddAmount(float _val)
        {
            currentAmount += _val;
            if (currentAmount > 1) currentAmount = 1;
        }

        public void UpdateUI()
        {
            amountSlider.value = currentAmount;
            
            if(amountSlider.value <= 0)
                UiController.Instance.ShowGameOverScreen();
        }
    }
    [SerializeField] private List<ResourceSample> resourceSamples = new List<ResourceSample>();
    [SerializeField] private TextMeshProUGUI moneyText;
    public GameObject moneyPrefab;
    
    private void Start()
    {
        ChangeMoneyCount(0);
    }

    private void FixedUpdate()
    {
        if(UiController.Instance.SomeViewIsActive()) return;
        
        foreach (var VARIABLE in resourceSamples)
        {
            VARIABLE.ReduceAmount();
            VARIABLE.UpdateUI();
        }
    }

    public void AddResourceAmount(int _resId, float _val)
    {
        resourceSamples[_resId].AddAmount(_val);
    }

    public bool CanSpawnItem(float _currency)
    {
        return GetCurrentMoney() - _currency > 0;
    }

    public void ChangeMoneyCount(float _val)
    {
        currentMoney += _val;
        moneyText.text = currentMoney.ToString("F2");
    }
}
