using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/*
 * Drag items logic
 */

public class DragDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    private RectTransform rectTr;
    private Canvas canvas;
    //private CanvasGroup canvasGroup;
    private ResourceItem item;
    
    public void Init(Canvas _canvas)
    {
        rectTr = GetComponent<RectTransform>();
        canvas = _canvas;
        //canvasGroup = GetComponent<CanvasGroup>();
        item = GetComponent<ResourceItem>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        //canvasGroup.blocksRaycasts = false;
        item.SetMoveTarget(null);
        item.ResetSpeed();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //canvasGroup.blocksRaycasts = true;
        item.ResetToDefaultTarget();
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTr.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }
    
    public void OnPointerDown(PointerEventData eventData)
    {
        //throw new System.NotImplementedException();
    }
}
