using System.Collections.Generic;
using UnityEngine;

public class UIManager
{
    // 캔버스의 소트오더 관리 (팝업 관리)
    private int _order = 0;
    
    // 팝업은 가장 마지막의 캔버스 부터 삭제가 되기 때문에 Stack
    private Stack<UI_Popup> _popupStack = new Stack<UI_Popup>();

    public T ShowPopupUI<T>(string name = null) where T : UI_Popup
    {
        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;

        GameObject go = Managers.Resource.Instantiate($"UI/Popup/{name}");
        T popup = Util.GetOrAddComponent<T>(go);
        _popupStack.Push(popup);
        
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
