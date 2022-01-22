using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FinishFactory : MonoBehaviour, IDropHandler
{
    [SerializeField] private int itemIDForFactory; // get this type of item
    
    // detect cargo
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            var resourceItem = eventData.pointerDrag.GetComponent<ResourceItem>();
            if (resourceItem.GetResourceID() == itemIDForFactory)
            {
                resourceItem.DestroyObject();
                Debug.LogError("money++");
            }
               
        }
    }
}
