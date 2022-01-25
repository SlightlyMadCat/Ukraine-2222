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
    private List<ItemInTerminalSample> itemInTerminal = new List<ItemInTerminalSample>(); // all item it this terminal

    [SerializeField] private int terminalType;
    [SerializeField] private float terminalBonusTime = 1f;

    [SerializeField] private Transform loadingBar;

    private bool isSendLoadIsStarted = false;

    [SerializeField] private Image image;
    [Serializable]
    public class ItemInTerminalSample
    {
       [SerializeField] private int currentItemSlot; // current type of item for this slot
       [SerializeField] private ResourceItem resourceItem;

       public ItemInTerminalSample(int currentItemSlot, ResourceItem resourceItem)
       {
           this.currentItemSlot = currentItemSlot;
           this.resourceItem = resourceItem;
       }

       public int GetItemSlot()
       {
           return currentItemSlot;
       }
       
       public ResourceItem GetResourceItem()
       {
           return resourceItem;
       }
    }
    
    // detect cargo
    public void OnDrop(PointerEventData eventData)
    {
        //Debug.LogError("On drop " + eventData.pointerDrag.gameObject.name);
        if (eventData.pointerDrag != null)
        {
            var resourceItem = eventData.pointerDrag.GetComponent<ResourceItem>();
            AddNewItem(resourceItem);
        }
    }

    // add new load in terminal
    private void AddNewItem(ResourceItem _resourceItem)
    {
        var itemTerminalSample = new ItemInTerminalSample(terminalType, _resourceItem);
        _resourceItem.SetInTerminal(true);
        _resourceItem.SetMoveTarget(null);
        _resourceItem.SetRectPosition(itemSlot.position);
        StartCoroutine(DisableItemAndTryToSend(itemTerminalSample));
        
        SoundsManager.Instance.PlayCustomSoundByID(0);
    }

    // try send load to next planet    
    IEnumerator DisableItemAndTryToSend(ItemInTerminalSample _terminalItem)
    {
        yield return new WaitForSeconds(0.3f);
        itemInTerminal.Add(_terminalItem);
        _terminalItem.GetResourceItem().SetStateGameobject(false);
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
            var sendingTime = itemInTerminal[0].GetResourceItem().GetSendingTime();
            if (itemInTerminal[0].GetResourceItem().GetResourceID() == itemInTerminal[0].GetItemSlot()) // apply sending boost 
                sendingTime -= terminalBonusTime;
            
            if (currentCreationTime >= sendingTime) // timer
            {
                NewPlanetLoadSpawner.Instance.SpawnItem(itemInTerminal[0].GetResourceItem()); // spawn item on right planet
                itemInTerminal.RemoveAt(0);
                currentCreationTime = 0;
                
                SoundsManager.Instance.PlayCustomSoundByID(1);
            }
            if(itemInTerminal.Count > 0)
                loadingBar.localScale = new Vector3(currentCreationTime / sendingTime, loadingBar.localScale.y) ;
        }
    }

    public void SetTerminalType(int type)
    {
        terminalType = type;
        image.sprite = ItemDataBase.Instance.GetSpriteByItemID(type);
    }
}
