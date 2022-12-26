using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerEx
{
    /// <summary>
    /// 현재 씬을 외부에서 BaseScene으로 취득
    /// </summary>
    public BaseScene CurrentScene => GameObject.FindObjectOfType<BaseScene>();

    public void LoadScene(Define.Scene type)
    {
        // 현재 씬을 청소
        CurrentScene.Clear();
        // 이동
        SceneManager.LoadScene(GetSceneName(type));
    }

    /// <summary>
    /// Enum의 Name취득
    /// </summary>
    private string GetSceneName(Define.Scene type)
    {
        return System.Enum.GetName(typeof(Define.Scene), type);
    }
}
