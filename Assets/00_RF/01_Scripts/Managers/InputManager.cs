using System;
using UnityEngine;

public class InputManager
{
    /// <summary>
    /// 리스너패턴
    /// 입력을 체크 후 입력이 있을 경우 전파한다.
    /// </summary>
    public Action KeyAction = null;
    
    public void OnUpdate()
    {
        // 키입력이 없으면 리턴
        if (Input.anyKey == false)
            return;
        
        // 키입력이 있으면 전파
        if (this.KeyAction != null)
            this.KeyAction?.Invoke();
    }
}
