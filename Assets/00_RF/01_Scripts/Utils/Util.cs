using UnityEngine;

/// <summary>
/// 유틸 함수
/// </summary>
public class Util
{
    /// <summary>
    /// 컴포넌트를 찾거나 없다면 추가한다.
    /// </summary>
    public static T GetOrAddComponent<T>(GameObject go) where T : UnityEngine.Component
    {
        T component = go.GetComponent<T>();
        if (component == null)
            component = go.AddComponent<T>();
        return component;
    }

    /// <summary>
    /// 자식 오브젝트 찾기
    /// </summary>
    public static GameObject FindChild(GameObject go, string name = null, bool recursive = false)
    {
        // 모든 GameObject에는 Transform이 존재 하기 때문에 Transform을 사용하면
        // 기존의 "FindChild"를 재사용 할 수 있다.
        Transform transform = FindChild<Transform>(go, name, recursive);
        // null체크 후 돌려주기
        return transform == null ? null : transform.gameObject;
    }

    /// <summary>
    /// 자식 컴포넌트 찾기
    /// </summary>
    public static T FindChild<T>(GameObject go, string name = null, bool recursive = false) where T : UnityEngine.Object
    {
        if (go == null)
            return null;

        // 자식만 찾음
        if (recursive == false)
        {
            // 자식 수 만큼
            for (int i = 0; i < go.transform.childCount; i++)
            {
                // 자식을 찾고
                Transform transform = go.transform.GetChild(i);
                // name은 null일수도 있음, 이름이 같다면
                if (string.IsNullOrEmpty(name) || transform.name == name)
                {
                    // 컴포넌트를 취득 하고 배포
                    T component = transform.GetComponent<T>();
                    if (component != null)
                        return component;
                }
            }
        }
        // 자식의 자식까지 찾음
        else
        {
            // 모든 자식들 만큼
            foreach (T component in go.GetComponentsInChildren<T>())
            {
                // name은 null일수도 있음, 이름이 같다면
                // // 컴포넌트를 취득 하고 배포
                if (string.IsNullOrEmpty(name) || component.name == name)
                    return component;
            }
        }

        return null;
    }
    
    /// <summary>
    /// 기호를 이용한 숫자 축약 ex) 1,000 = 1K
    /// </summary>
    public static string NumAbb(long originNum)
    {
        // 記号(symbol)一覧
        string[] symbol = new string[7] { "K", "M", "G", "T", "P", "E", "Z" };
        // 受信した数字を文字列に変更
        string result = originNum.ToString();

        // 数字 + symbolは最大4桁まで, 数字が4桁以下なら、Symbolなしでそのまま出力
        if (result.Length < 4)
            return result;

        for (int i = 0; i < symbol.Length; i++)
        {
            // 文字列の長崎チェック
            if (4 + 3 * i <= result.Length && result.Length == 4 + 3 * (i + 1))
            {
                // 3の余りの値(n)は、[0, 1, 2]
                int n = result.Length % 3;
                // 残りの値(n)が、0なら、3で
                n = n == 0 ? 3 : n;
                
                // 残り値個数 (n) = 前桁個数
                // 前桁の表現に使用した値の、すぐ後ろの値を小数点以下の数字料表現
                result = $"{result.Substring(0, n)}.{result.Substring(n, 1)}";
                result += symbol[i];
                break;
            }
        }
        return result;
    }
}
