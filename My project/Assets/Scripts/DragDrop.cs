using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/*
 * Drag items logic
 */

public class DragDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IPointerUpHandler
{
    private RectTransform rectTr;
    private Canvas canvas;
    private CanvasGroup canvasGroup;
    private ResourceItem item;
    private bool isTouched;
    
    public void Init(Canvas _canvas)
    {
        rectTr = GetComponent<RectTransform>();
        canvas = _canvas;
        canvasGroup = GetComponent<CanvasGroup>();
        item = GetComponent<ResourceItem>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = false;
        item.SetMoveTarget(null);
        item.ResetToMinSpeed();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;
        item.ResetToDefaultTarget();
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTr.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public bool IsTouched()
    {
        return isTouched;
    }
    
    public void OnPointerDown(PointerEventData eventData)
    {
        isTouched = true;
        ItemDataBase.Instance.SetDynamicItemsRaycastState(false, item);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isTouched = false;
        ItemDataBase.Instance.SetDynamicItemsRaycastState(true, item);
    }
}
