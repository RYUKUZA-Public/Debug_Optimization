using System.Collections.Generic;
using UnityEngine;

public class UIManager
{
    // 캔버스의 소트오더 관리 (팝업 관리)
    private int _order = 10;
    
    // 팝업은 가장 마지막의 캔버스 부터 삭제가 되기 때문에 Stack
    private Stack<UI_Popup> _popupStack = new Stack<UI_Popup>();
    private UI_Scene _sceneUI = null;

    public GameObject Root
    {
        get
        {
            GameObject root = GameObject.Find("@UI_Root");
            if (root == null)
                root = new GameObject { name = "@UI_Root" };
            return root;
        }
    }

    /// <summary>
    /// 팝업이 켜질때 캔버스의 sortingOrder를 관리
    /// </summary>
    public void SetCanvas(GameObject go, bool sort = true)
    {
        Canvas canvas = go.GetOrAddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.overrideSorting = true;

        if (sort)
        {
            canvas.sortingOrder = _order;
            _order++;
        }
        // 팝업과 관련 없는 일반 UI
        else
        {
            canvas.sortingOrder = 0;
        }

    }
    
    public T MakeSubItem<T>(Transform parent = null, string name = null) where T : UI_Base
    {
        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;
        
        GameObject go = Managers.Resource.Instantiate($"UI/SubItem/{name}");
        
        if (parent != null)
            go.transform.SetParent(parent);
        
        return go.GetOrAddComponent<T>();
    }

    public T ShowSceneUI<T>(string name = null) where T : UI_Scene
    {
        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;

        GameObject go = Managers.Resource.Instantiate($"UI/Scene/{name}");
        T sceneUI = go.GetOrAddComponent<T>();
        _sceneUI = sceneUI;
        
        go.transform.SetParent(Root.transform);
        
        return sceneUI;
    }

    public T ShowPopupUI<T>(string name = null) where T : UI_Popup
    {
        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;

        GameObject go = Managers.Resource.Instantiate($"UI/Popup/{name}");
        T popup = go.GetOrAddComponent<T>();
        _popupStack.Push(popup);
        
        go.transform.SetParent(Root.transform);
        
        return popup;
    }

    public void ClosePopupUI(UI_Popup popup)
    {
        if (_popupStack.Count == 0)
            return;

        if (_popupStack.Peek() != popup)
        {
            Debug.Log("팝업 닫기 실패");
            return;
        }

        ClosePopupUI();
    }
    
    public void ClosePopupUI(string name = null)
    {
        if (_popupStack.Count == 0)
            return;

        UI_Popup popup = _popupStack.Pop();
        Managers.Resource.Destroy(popup.gameObject);
        popup = null;
        _order--;

    }

    public void CloseAllPopupUI(string name = null)
    {
        while (_popupStack.Count > 0)
            ClosePopupUI();
    }
}
