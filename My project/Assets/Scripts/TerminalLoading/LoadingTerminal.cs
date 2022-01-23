using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/**
 * terminal logic - get cargo and send  
 */
public class LoadingTerminal : MonoBehaviour, IDropHandler
{
    [Header("Set item in this slot")] [SerializeField]
    private RectTransform itemSlot;

    [Header("Transport speed in seconds")] [SerializeField]
    private float transportSpeed = 3f; // speed in seconds 

    [Header("List of item in this terminal")] [SerializeField]
    private List<ResourceItem> itemInTerminal = new List<ResourceItem>(); // all item it this terminal

    [SerializeField] private int terminalType;

    [SerializeField] private Transform loadingBar;

    private bool isSendLoadIsStarted = false;

    // detect cargo
    public void OnDrop(PointerEventData eventData)
    {
        Debug.LogError("On drop " + eventData.pointerDrag.gameObject.name);
        if (eventData.pointerDrag != null)
        {
            var resourceItem = eventData.pointerDrag.GetComponent<ResourceItem>();
            AddNewItem(resourceItem);
        }
    }

    // add new load in terminal
    private void AddNewItem(ResourceItem _resourceItem)
    {
        if (itemInTerminal.Contains(_resourceItem)) return;
        _resourceItem.SetInTerminal(true);
        _resourceItem.SetMoveTarget(null);
        _resourceItem.SetRectPosition(itemSlot.position);
        StartCoroutine(DisableItemAndTryToSend(_resourceItem));
    }

    // try send load to next planet    
    IEnumerator DisableItemAndTryToSend(ResourceItem _resourceItem)
    {
        yield return new WaitForSeconds(0.3f);
        itemInTerminal.Add(_resourceItem);
        _resourceItem.SetStateGameobject(false);
    }

    private float currentCreationTime = 0;
    private void FixedUpdate()
    {
        if (itemInTerminal.Count == 0) // empty item list
        {
            loadingBar.localScale = new Vector3(0, loadingBar.localScale.y) ;
            currentCreationTime = 0;
        }
        else
        {
            currentCreationTime += Time.fixedDeltaTime;
            if (currentCreationTime >= itemInTerminal[0].GetSendingTime())
            {
                NewPlanetLoadSpawner.Instance.SpawnItem(itemInTerminal[0]);
                itemInTerminal.RemoveAt(0);
                currentCreationTime = 0;
            }
            if(itemInTerminal.Count > 0)
                loadingBar.localScale = new Vector3(currentCreationTime / itemInTerminal[0].GetSendingTime(), loadingBar.localScale.y) ;
        }
    }
}
