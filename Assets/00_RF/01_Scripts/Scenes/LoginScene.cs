using UnityEngine;

public class LoginScene : BaseScene
{
    protected override void Init()
    {
        base.Init();
        SceneType = Define.Scene.Login;

        for (int i = 0; i < 10; i++)
        {
            Managers.Resource.Instantiate("Player");
        }
        
    }

    public override void Clear()
    {
        Debug.Log("로그인씬 Clear");
    }

    /// <summary>
    /// TODO.
    /// </summary>
    public void Update()
    {
        // 씬이동
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Managers.Scene.LoadScene(Define.Scene.InGame);
            //SceneManager.LoadSceneAsync("InGame");
        }
    }
}
