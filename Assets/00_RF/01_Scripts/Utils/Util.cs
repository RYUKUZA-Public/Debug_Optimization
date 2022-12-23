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
}
