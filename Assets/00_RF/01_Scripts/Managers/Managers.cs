using UnityEngine;

/// <summary>
/// 매니저 통합
/// </summary>
public class Managers : MonoBehaviour
{ 
    static Managers s_instance;
    public static Managers Instance => s_instance;

    private void Start() => Init();

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
