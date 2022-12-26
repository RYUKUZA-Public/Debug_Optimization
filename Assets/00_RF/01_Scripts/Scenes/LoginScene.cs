using UnityEngine;
using UnityEngine.SceneManagement;

public class LoginScene : BaseScene
{
    protected override void Init()
    {
        base.Init();
        SceneType = Define.Scene.Login;
        
    }

    public override void Clear()
    {
        
    }

    /// <summary>
    /// TODO.
    /// </summary>
    public void Update()
    {
        // 씬이동
        if (Input.GetKeyDown(KeyCode.Q))
        {
            SceneManager.LoadSceneAsync("InGame");
        }
    }
}
