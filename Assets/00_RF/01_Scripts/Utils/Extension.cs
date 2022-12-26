using System;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Extension기능
/// </summary>
public static class Extension
{
    /// <summary>
    /// 컴포넌트를 찾거나 없다면 추가한다.
    /// </summary>
    public static T GetOrAddComponent<T>(this GameObject go) where T : UnityEngine.Component
    {
        return Util.GetOrAddComponent<T>(go);
    }
    
    /// <summary>
    /// UI이벤트를 추가 (클리그, 드래그, ...)
    /// </summary>
    public static void BindEvent(this GameObject go, Action<PointerEventData> action, Define.UIEvent type = Define.UIEvent.Click)
    {
        UI_Base.BindEvent(go, action, type);
    }
}
