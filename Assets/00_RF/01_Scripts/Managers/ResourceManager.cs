using UnityEngine;

public class ResourceManager
{
    // 로드
    public T Load<T>(string path) where T : Object
    {
        // Prefab인 경우에 Pool에서 찾아 본 후 그것을 반환
        if (typeof(T) == typeof(GameObject))
        {
            // 경로가 이름으로 들어 오기 때문에 가공이 필요함
            string name = path;
            // 마지막 / 을 찾은 후
            int index = name.LastIndexOf('/');
            // / 이후의 문자를 모두 저장
            if (index >= 0)
                name = name.Substring(index + 1);

            // 바로 취득
            GameObject go = Managers.Pool.GetOriginal(name);
            if (go != null)
                return go as T;
        }
        
        // 업다면 로드
        return Resources.Load<T>(path);
    }

    // 생성
    public GameObject Instantiate(string path, Transform parent = null)
    {
        // 로드 Prefab인 경우에 Pool에서 찾아 본 후 그것을 반환
        GameObject original = Load<GameObject>($"Prefabs/{path}");

        if (original == null)
        {
            Debug.Log($"Prefab 로드 실패 {path}");
            return null;
        }
        
        // 풀링 된 오브젝트가 있다면 재사용
        if (original.GetComponent<Poolable>() != null)
            return Managers.Pool.Pop(original, parent).gameObject;
        
        // 풀링 대상이 아닌 경우
        GameObject go = Object.Instantiate(original, parent);
        // (Clone) 텍스트 제거
        go.name = original.name;
        return go;
    }

    // 파괴
    public void Destroy(GameObject go)
    {
        if (go == null)
            return;
        // 풀링 대상일 경우 풀으로 돌려준다.
        Poolable poolable = go.GetComponent<Poolable>();
        if (poolable != null)
        {
            Managers.Pool.Push(poolable);
            return;
        }
        
        // 풀링 대상이 아닌 경우
        Object.Destroy(go);
    }
}
