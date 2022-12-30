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

    private DataManager _data = new DataManager();
    private InputManager _input = new InputManager();
    private PoolManager _pool = new PoolManager();
    private ResourceManager _resource = new ResourceManager();
    private SceneManagerEx _scene = new SceneManagerEx();
    private SoundManager _sound = new SoundManager();
    private UIManager _ui = new UIManager();

    public static DataManager Data { get { return Instance._data; } }
    public static InputManager Input { get { return Instance._input; } }
    public static PoolManager Pool { get { return Instance._pool; } }
    public static ResourceManager Resource { get { return Instance._resource; } }
    public static SceneManagerEx Scene { get { return Instance._scene; } }
    public static SoundManager Sound { get { return Instance._sound; } }
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
            
            // 데이터 매니저 초기화
            s_instance._data.Init();
            // 풀 매니저 초기화
            s_instance._pool.Init();
            // 사운드 매니저 초기화
            s_instance._sound.Init();
        }
    }

    /// <summary>
    /// 씬 이동시 처리해야 할것을 이곳에서
    /// 일괄 처리 한다.
    /// </summary>
    public static void Clear()
    {
        Sound.Clear();
        Input.Clear();
        Scene.Clear();
        UI.Clear();
        Pool.Clear();
    }
}
