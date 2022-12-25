using System;
using UnityEngine;

/// <summary>
/// 매니저 통합
/// </summary>
public class Managers : MonoBehaviour
{
    // 유일성 보장
    private static Managers s_instance;
    // 유일한 Managers
    // 산하의 각 Manager를 돌려주는 형태이기 때문에 private
    private static Managers Instance { get { Init(); return s_instance; } }

    #region [Core]
    private InputManager _input = new InputManager();
    private ResourceManager _resource = new ResourceManager();
    private UIManager _ui = new UIManager();
    
    public static InputManager Input { get { return Instance._input; } }
    public static ResourceManager Resource { get { return Instance._resource; } }
    public static UIManager UI { get { return Instance._ui; } }
    #endregion

    private void Start() => Init();

    /// <summary>
    /// // Managers에서 대표로 Update
    /// </summary>
    private void Update() => _input.OnUpdate();

    // 초기화
    static void Init()
    {
        if (s_instance == null)
        {
            GameObject go = GameObject.Find("@Managers");
        
            if (go == null)
            {
                go = new GameObject { name = "@Managers" };
                go.AddComponent<Managers>();
            }
    
            DontDestroyOnLoad(go);
            s_instance = go.GetComponent<Managers>();
        }
    }
}
