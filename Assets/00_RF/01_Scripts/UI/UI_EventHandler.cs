using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_EventHandler : MonoBehaviour, IPointerClickHandler, IDragHandler
{
    /// <summary>
    /// 클릭 콜백
    /// </summary>
    public Action<PointerEventData> OnPointerClickHandler = null;
    /// <summary>
    /// 드래그 콜백
    /// </summary>
    public Action<PointerEventData> OnDragHandler = null;
    
    public void OnPointerClick(PointerEventData eventData) 
        => OnPointerClickHandler?.Invoke(eventData);

    public void OnDrag(PointerEventData eventData) 
        => OnDragHandler?.Invoke(eventData);
}
