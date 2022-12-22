using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager
{
    /// <summary>
    /// 리스너패턴
    /// 입력을 체크 후 입력이 있을 경우 전파한다.
    /// </summary>
    public Action KeyAction = null;
    /// <summary>
    /// 마우스 이벤트
    /// </summary>
    public Action<Define.MouseEvent> MouseAction = null;

    private bool _pressed = false;

    public void OnUpdate()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        // 키입력이 있으면 전파
        if (Input.anyKey && KeyAction != null)
            KeyAction?.Invoke();

        if (MouseAction != null)
        {
            // 왼쪽 마우스 클릭
            if (Input.GetMouseButton(0))
            {
                MouseAction.Invoke(Define.MouseEvent.Press);
                _pressed = true;
            }
            else
            {
                if (_pressed)
                    MouseAction.Invoke(Define.MouseEvent.Click);
                
                _pressed = false;
            }
        }
    }
}