using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiController : MonoBehaviour
{
    #region Singleton

    public static UiController Instance;

    private void Awake()
    {
        Instance = this;
        startView.SetActive(true);
    }

    #endregion
    
    [SerializeField] private GameObject escView;
    [SerializeField] private GameObject startView;
    [SerializeField] private GameObject gameOverView;
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //show esc screen
            escView.SetActive(!escView.activeSelf);
        }
    }

    public bool SomeViewIsActive()
    {
        return escView.activeSelf || startView.activeSelf || gameOverView.activeSelf;
    }

    public void HideStartView()
    {
        startView.SetActive(false);
    }

    public void HideEscView()
    {
        escView.SetActive(false);
    }

    public void ShowGameOverScreen()
    {
        gameOverView.SetActive(true);
    }
}
